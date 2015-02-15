using System;

namespace NInsight.Core.Config
{
    internal class Conventions
    {
        internal Func<Type, bool> IsEndpointType = t => false;

        internal Func<Type, bool> IsStartpointType = t => false;

        internal Func<Type, bool> IsTracepointType = t => false;
    }
}