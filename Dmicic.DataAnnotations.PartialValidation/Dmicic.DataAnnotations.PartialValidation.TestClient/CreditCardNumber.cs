using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Dmicic.DataAnnotations.PartialValidation.TestClient
{
    public class CreditCardNumberAttribute : ValidationAttribute
    {
        public CreditCardNumberAttribute()
            : base("Credit card number is invalid")
        { }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object value)
        {
            return ((string)value == "123456");
        }
    }
}
