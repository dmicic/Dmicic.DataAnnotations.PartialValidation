using System;
using System.Collections.Generic;
using System.Linq;

using Dmicic.DataAnnotations.PartialValidation.Specification;

namespace Dmicic.DataAnnotations.PartialValidation
{
    public class Configuration
    {
        private static Lazy<Configuration> _instance = new Lazy<Configuration>(() => new Configuration());

        internal IList<EnvironmentSpecification> Environments { get; private set; }

        internal static Configuration Instance
        {
            get { return Configuration._instance.Value; }
        }

        private Environment FluentEnvironment { get; set; }

        private Configuration()
        {
            this.Environments = new List<EnvironmentSpecification>();
            this.FluentEnvironment = new Environment();
        }

        public static Configuration Configure()
        {
            return Configuration.Instance;
        }

        public Environment For(string environment)
        {
            if (!this.Environments.Any(x => x.Name == environment))
                this.Environments.Add(new EnvironmentSpecification(environment));

            var fluent = new Environment();
            fluent.Specification = this.Environments.First(x => x.Name == environment);
            return fluent;
        }
    }
}
