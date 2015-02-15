using System;
 

using Castle.Windsor;

namespace NInsight.Core.Config
{
    public class Configure
    {
        internal Conventions Conventions = new Conventions();

        public Configure()
        {
            new Logger();

         }

        public IWindsorContainer Container { get; set; }

        public Configure DefiningStartpointAs(Func<Type, bool> definesStartpointType)
        {
            this.Conventions.IsStartpointType = definesStartpointType;
            return this;
        }

        public Configure DefiningEndpointAs(Func<Type, bool> definesEndpointType)
        {
            this.Conventions.IsEndpointType = definesEndpointType;
            return this;
        }

        public Configure DefiningTracepointAs(Func<Type, bool> definingTracepointAs)
        {
            this.Conventions.IsTracepointType = definingTracepointAs;
            return this;
        }
    }
}