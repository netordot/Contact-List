using ContactList.Application.Contact.CreateContact;
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

            //services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }

    }
}
