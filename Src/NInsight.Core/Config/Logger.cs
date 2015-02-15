using Common.Logging;
using Common.Logging.Simple;

namespace NInsight.Core.Config
{
    public class Logger
    {
        public Logger()
        {
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter();

            var log = LogManager.GetCurrentClassLogger();

            // log something
            log.Debug("Some Debug Log Output");
        }
    }
}