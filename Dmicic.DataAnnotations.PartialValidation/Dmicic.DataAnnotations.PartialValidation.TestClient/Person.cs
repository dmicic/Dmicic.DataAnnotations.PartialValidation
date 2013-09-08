using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Dmicic.DataAnnotations.PartialValidation.TestClient
{
    public class Person : EntityBase
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string EMail { get; set; }

        [Required]
        [CreditCardNumber]
        public string CreditCardNumber { get; set; }
    }
}
