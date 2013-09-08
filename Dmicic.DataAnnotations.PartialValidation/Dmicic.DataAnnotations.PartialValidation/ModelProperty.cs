using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

using Dmicic.DataAnnotations.PartialValidation.Specification;

namespace Dmicic.DataAnnotations.PartialValidation
{
    public class ModelProperty<TModelType>
    {
        internal PropertySpecification Specification { get; set; }

        internal ModelType<TModelType> CurrentModel { get; set; }

        internal Environment Environment { get; set; }

        internal ModelProperty(ModelType<TModelType> currentModelType, Environment environment)
        {
            this.CurrentModel = currentModelType;
            this.Environment = environment;
        }

        public ModelType<TOtherModelType> Configure<TOtherModelType>()
        {
            return this.Environment.Configure<TOtherModelType>();
        }

        public ModelProperty<TModelType> Property(Expression<Func<TModelType, object>> nextProperty)
        {
            return this.CurrentModel.Property(nextProperty);
        }

        public ModelProperty<TModelType> Include(ValidationAttribute inclusion)
        {
            if (!this.Specification.Inclusion.Any(x => x.GetType() == inclusion.GetType()))
                this.Specification.Inclusion.Add(inclusion);

            return this;
        }

        public ModelProperty<TModelType> Exclude<TAnnotationType>() where TAnnotationType : ValidationAttribute
        {
            if (!this.Specification.Exclution.Any(x => x == typeof(TAnnotationType)))
                this.Specification.Exclution.Add(typeof(TAnnotationType));

            return this;
        }

        public ModelProperty<TModelType> ExcludeAll()
        {
            this.Specification.ExcludeAll = true;
            return this;
        }

        public ModelProperty<TModelType> ExcludeAllBut<TAnnotationType>() where TAnnotationType : ValidationAttribute
        {
            this.Specification.ExcludeAllExcept = typeof(TAnnotationType);
            return this;
        }
    }
}
