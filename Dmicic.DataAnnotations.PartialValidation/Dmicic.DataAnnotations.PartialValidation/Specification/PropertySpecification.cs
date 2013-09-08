using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dmicic.DataAnnotations.PartialValidation.Specification
{
    internal class PropertySpecification
    {
        internal string Name { get; private set; }
        internal Type ExcludeAllExcept { get; set; }
        internal bool ExcludeAll { get; set; }
        internal IList<Type> Exclution { get; private set; }
        internal IList<ValidationAttribute> Inclusion { get; private set; }

        internal PropertySpecification(string name)
        {
            this.Name = name;
            this.Exclution = new List<Type>();
            this.Inclusion = new List<ValidationAttribute>();
        }
    }
}
