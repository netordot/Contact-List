using ContactList.API.Contracts;
using ContactList.API.Contracts.Requests;
using ContactList.API.Contracts.Response;
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
            // добавить расширение для ошибки

            return new ObjectResult(result.Value) { StatusCode = 200 };

        }

        [HttpPatch("{id:guid}/update")]
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
            // обработка ошибок

            return new ObjectResult(result.Value) { StatusCode = 200 };
        }

        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetAll(
            CancellationToken cancellation,
            [FromServices] IGetAllContactsHandler handler)
        {
            var result = await handler.Handle(cancellation);

            return result.Value;
        }

        [HttpGet("{Name:alpha}/get")]
        public async Task<ActionResult<ContactDto>> GetByName(
            [FromBody] string Name,
            [FromServices] IGetByNameHandler handler,
            CancellationToken cancellation)
        {
            var contact = await handler.Handle(Name, cancellation) ;
            var result = new ContactDto(
                contact.Value.Id.Value,
                contact.Value.Name, 
                contact.Value.PhoneNumber.Number, 
                contact.Value.Description);

            return result;
        }

        [HttpDelete("{id:guid}/delete")]
        public async Task<ActionResult> Delete
            ([FromServices] IDeleteContactHandler handler,
            [FromRoute] Guid id,
            CancellationToken cancellation)
        {
            var result = await handler.Handle(id, cancellation);

            return new ObjectResult(result.Value) { StatusCode= 200 };
        }
    }
}
