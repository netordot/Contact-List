using ContactList.API.Contracts.Requests;
using ContactList.Application.Contact.CreateContact;
using ContactList.Application.Contact.UpdateContact;
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
            [FromServices] UpdateContactHandler handler,
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
    }
}
