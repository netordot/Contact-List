﻿using ContactList.API.Contracts;
using ContactList.API.Contracts.Requests;
using ContactList.API.Contracts.Response;
using ContactList.API.Extensions;
using ContactList.Application.Contact.CreateContact;
using ContactList.Application.Contact.Delete;
using ContactList.Application.Contact.GetAll;
using ContactList.Application.Contact.GetByName;
using ContactList.Application.Contact.UpdateContact;
using ContactList.Domain.Contact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactList.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CreateContactRequest request,
            [FromServices] ICreateContactHandler handler,
            CancellationToken cancellation)
        {
            var command = new CreateContactCommand(
                request.PhoneNumber,
                request.Name,
                request.Email,
                request.Description);

            var result = await handler.Handle(command, cancellation);
            if(result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(result.Value) { StatusCode = 200 };

        }

        [HttpPut("{id:guid}/update")]
        public async Task<ActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateContactRequest request,
            [FromServices] IUpdateContactHandler handler,
            CancellationToken cancellation)
        {
            var command = new UpdateContactCommad(
                id,
                request.PhoneNumber,
                request.Name,
                request.Email,
                request.Description
                );

            var result = await handler.Handle(command, cancellation);
            if(result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(result.Value) { StatusCode = 200 };
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactDto>>> GetAll(
            CancellationToken cancellation,
            [FromServices] IGetAllContactsHandler handler)
        {
            var contacts = await handler.Handle(cancellation);
            var result = contacts.Value.Select(c => new ContactDto(c.Name, c.PhoneNumber.Number, c.Description, c.Email.Mail)).ToList();

            return result;
        }

        [HttpGet("{id:guid}/get")]
        public async Task<ActionResult<ContactDto>> GetById(
             [FromRoute] Guid id,
            [FromServices] IGetByIdHandler handler,
            CancellationToken cancellation)
        {
            var contact = await handler.Handle(id, cancellation);
            if(contact.IsFailure)
            {
                return contact.Error.ToResponse();
            }
            var result = new ContactDto(
                contact.Value.Name,
                contact.Value.PhoneNumber.Number,
                contact.Value.Description,
                contact.Value.Email.Mail);

            return result;
        }

        [HttpDelete("{id:guid}/delete")]
        public async Task<ActionResult> Delete
            ([FromServices] IDeleteContactHandler handler,
            [FromRoute] Guid id,
            CancellationToken cancellation)
        {
            {
                var result = await handler.Handle(id, cancellation);

                if (result.IsFailure)
                {
                    return Ok(new { success = false, error = result.Error });
                }

                return Ok(new { success = true, id = result.Value });
            }
        }
    }

}
