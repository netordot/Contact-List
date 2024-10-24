using ContactList.Application.Contact.CreateContact;
using ContactList.Application.Contact.Delete;
using ContactList.Application.Contact.GetAll;
using ContactList.Application.Contact.UpdateContact;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContactList.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICreateContactHandler, CreateContactHandler>();
            services.AddScoped<IUpdateContactHandler, UpdateContactHandler>();
            services.AddScoped<IGetAllContactsHandler, GetAllContactsHandler>();
            services.AddScoped<IGetAllContactsHandler, GetAllContactsHandler>();
            services.AddScoped<IDeleteContactHandler, DeleteContactHandler>();  

            //services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }

    }
}
