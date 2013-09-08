using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Dmicic.DataAnnotations.PartialValidation.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Person();
            p1.CreditCardNumber = "23434";
            p1.EMail = "wrong mail address";

            Configuration.Configure()
                .For("UI")
                        .Configure<Person>()
                            .Property(p => p.CreatedBy).ExcludeAll() // Exclude all validations for CreatedBy
                            .Property(p => p.CreatedDate).ExcludeAll() // Exclude all validations for CreatedDate
                            .Property(p => p.EMail).Include(new EmailAttribute()) // Include the Email validation for EMail
                            .Property(p => p.CreditCardNumber).Exclude<CreditCardNumberAttribute>(); // Exclude the credit card validation for CreditCardNumber

            Configuration.Configure()
                .For("Business")
                    .Configure<Person>()
                            .Property(p => p.CreatedBy).ExcludeAll() // Exclude all validations for CreatedBy
                            .Property(p => p.CreatedDate).ExcludeAll(); // Exclude all validations fro CreatedDate


            Configuration.Configure()
                .For("DataAccess")
                    .Configure<Person>()
                        .Property(p => p.CreditCardNumber).ExcludeAllBut<RequiredAttribute>(); // Exclude all except the required validation

            var UiResult = PartialValidator.Validate(p1, "UI");
            var BusinessResult = PartialValidator.Validate(p1, "Business");
            var DataAccessResult = PartialValidator.Validate(p1, "DataAccess");
        }
    }
}
