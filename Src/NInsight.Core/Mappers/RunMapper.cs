using System.Threading;

using NInsight.Core.Domain;

namespace NInsight.Core.Mappers
{
    class RunMapper
    {
        public Run Do(Application application)
        {
            return new Run
            {
                Name = Thread.CurrentThread.GetHashCode().ToString(),
                ApplicationId = application.Id
            };
        }
    }
}
