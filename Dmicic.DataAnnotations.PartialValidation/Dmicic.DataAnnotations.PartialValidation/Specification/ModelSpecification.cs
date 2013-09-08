using System;
using System.Collections.Generic;

namespace Dmicic.DataAnnotations.PartialValidation.Specification
{
    internal class ModelSpecification
    {
        internal Type ModelType { get; private set; }
        internal IList<PropertySpecification> Properties { get; private set; }

        internal ModelSpecification(Type modelType)
        {
            this.ModelType = modelType;
            this.Properties = new List<PropertySpecification>();
        }
    }
}
