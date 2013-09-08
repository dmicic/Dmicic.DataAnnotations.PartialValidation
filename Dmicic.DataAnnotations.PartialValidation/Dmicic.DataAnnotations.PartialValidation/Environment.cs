using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Dmicic.DataAnnotations.PartialValidation.Specification;

namespace Dmicic.DataAnnotations.PartialValidation
{
    public class Environment
    {
        internal EnvironmentSpecification Specification { get; set; }

        internal Environment()
        { }

        public Environment Exclude<TAnnotationType>() where TAnnotationType : ValidationAttribute
        {
            if (!this.Specification.GlobalExclution.Any(x => x == typeof(TAnnotationType)))
                this.Specification.GlobalExclution.Add(typeof(TAnnotationType));

            return this;
        }

        public ModelType<TModelType> Configure<TModelType>()
        {
            if (!this.Specification.Models.Any(x => x.ModelType == typeof(TModelType)))
                this.Specification.Models.Add(new ModelSpecification(typeof(TModelType)));

            var fluent = new ModelType<TModelType>(this);
            fluent.Specification = this.Specification.Models.First(x => x.ModelType == typeof(TModelType));

            return fluent;
        }
    }
}
