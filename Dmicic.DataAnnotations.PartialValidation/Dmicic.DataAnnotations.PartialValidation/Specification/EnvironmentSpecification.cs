using System;
using System.Collections.Generic;

namespace Dmicic.DataAnnotations.PartialValidation.Specification
{
    internal class EnvironmentSpecification
    {
        internal string Name { get; private set; }
        internal IList<Type> GlobalExclution { get; private set; }
        internal IList<ModelSpecification> Models { get; private set; }

        internal EnvironmentSpecification(string name)
        {
            this.Name = name;
            this.GlobalExclution = new List<Type>();
            this.Models = new List<ModelSpecification>();
        }
    }
}
