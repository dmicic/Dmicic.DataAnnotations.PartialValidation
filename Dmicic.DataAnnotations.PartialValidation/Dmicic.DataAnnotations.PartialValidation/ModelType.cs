using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Dmicic.DataAnnotations.PartialValidation.Specification;

namespace Dmicic.DataAnnotations.PartialValidation
{
    public class ModelType<TModelType>
    {
        internal ModelSpecification Specification { get; set; }

        internal Environment Environment { get; set; }

        internal ModelType(Environment environment)
        {
            this.Environment = environment;
        }

        public ModelProperty<TModelType> Property(Expression<Func<TModelType, object>> property)
        {
            var propName = Helper.GetPropertyName(property.Body);

            if (!this.Specification.Properties.Any(x => x.Name == propName))
                this.Specification.Properties.Add(new PropertySpecification(propName));

            var fluent = new ModelProperty<TModelType>(this, this.Environment);
            fluent.Specification = this.Specification.Properties.First(x => x.Name == propName);

            return fluent;
        }
    }
}
