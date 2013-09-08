using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Dmicic.DataAnnotations.PartialValidation.TestClient
{
    public abstract class EntityBase
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
