using System.Collections.Generic;

namespace Dmicic.DataAnnotations.PartialValidation.Specification
{
    internal class GlobalSpecification
    {
        internal IList<EnvironmentSpecification> Environments { get; private set; }

        public GlobalSpecification()
        {
            this.Environments = new List<EnvironmentSpecification>();
        }
    }
}
