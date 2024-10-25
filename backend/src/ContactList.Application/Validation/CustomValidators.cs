using ContactList.Domain.Contact.Shared;
using CSharpFunctionalExtensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Validation
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>
            (this IRuleBuilder<T, TElement> ruleBuilder, Func<TElement, Result<TValueObject, Error>> factoryMethod)
        {
            return ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject, Error> result = factoryMethod(value);

                if (result.IsSuccess)
                    return;

                context.AddFailure(result.Error.Serialize());
            });
        }

        public static IRuleBuilderOptionsConditions<T, TProperty> WithError<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, Error error)
        {
            return (IRuleBuilderOptionsConditions<T, TProperty>)rule.WithMessage(error.Serialize());
        }
    }
}
