using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using Dmicic.DataAnnotations.PartialValidation.Specification;


namespace Dmicic.DataAnnotations.PartialValidation
{
    public static class PartialValidator
    {
        public static IEnumerable<ValidationResult> Validate(object modelToValidate, string environment = null)
        {
            if (modelToValidate == null)
                throw new ArgumentNullException("modelToValidate");

            EnvironmentSpecification environmentSpec = null;
            var globalSpec = Configuration.Instance;
            
            if (!string.IsNullOrWhiteSpace(environment))
            {
                // Get environment
                environmentSpec = globalSpec.Environments.FirstOrDefault(x => x.Name.ToLower() == environment.ToLower());

                if (environmentSpec == null)
                    throw new ArgumentException("Validation Environment not found.", "environment");
            }

            // Get model specification
            var modelSpec = environmentSpec.Models.FirstOrDefault(x => x.ModelType == modelToValidate.GetType()) ?? new ModelSpecification(null);
            
            // Get properties to validate
            var propertiesToValidate = TypeDescriptor.GetProperties(modelToValidate);

            // Contains property description and related validation attributes
            var validationPlan = new Dictionary<PropertyDescriptor, List<ValidationAttribute>>();

            PropertySpecification propertySpec;
            List<ValidationAttribute> exectuableValidations;
            bool exclude;
            bool includeOnly;

            foreach (PropertyDescriptor property in propertiesToValidate)
            {
                exectuableValidations = new List<ValidationAttribute>();

                // Get property spec out of model spec.
                propertySpec = modelSpec.Properties.FirstOrDefault(x => x.Name == property.Name);

                // If exclude all is set to true, continue with next property
                if (propertySpec != null && propertySpec.ExcludeAll)
                    continue;

                // Iterate through all property attributes and get the validation attributes
                foreach (var attr in property.Attributes)
                {
                    if (attr is ValidationAttribute)
                    {
                        // Check for global exclution
                        if (!environmentSpec.GlobalExclution.Any(x => attr.GetType() == x))
                        {
                            exclude = false;
                            includeOnly = false;

                            if (propertySpec != null)
                            {
                                includeOnly = (propertySpec.ExcludeAllExcept == attr.GetType());
                                exclude = propertySpec.Exclution.Any(y => y == attr.GetType());
                            }

                            // If no specification exists or no exclution found or all except this attribute are excluded -> add to validation execution list.
                            if (propertySpec == null || (!exclude && includeOnly) || includeOnly)
                                exectuableValidations.Add((ValidationAttribute)attr);
                            
                        }
                    }
                }

                if (propertySpec != null)
                    foreach (var validationAttribute in propertySpec.Inclusion)
                        exectuableValidations.Add(validationAttribute);

                if (exectuableValidations.Any())
                    validationPlan.Add(property, exectuableValidations);
            }

            var validationResult = new List<ValidationResult>();
            foreach (var plan in validationPlan)
            {
                foreach (var validationAttribute in plan.Value)
                {
                    // Check if property is valid. If not, add error to result list
                    if (!validationAttribute.IsValid(plan.Key.GetValue(modelToValidate)))
                    {
                        validationResult.Add(new ValidationResult(validationAttribute.FormatErrorMessage(plan.Key.DisplayName), new List<string> { plan.Key.Name }));
                    }
                }
            }

            return validationResult;
        }
    }
}
