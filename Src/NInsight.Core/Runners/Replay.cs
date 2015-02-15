using System;
using System.Diagnostics;
using System.Linq;

using KellermanSoftware.CompareNetObjects;

using Newtonsoft.Json;

using NInsight.Core.Config;
using NInsight.Core.Context;
using NInsight.Core.Domain;
using NInsight.Core.Repositories;

namespace NInsight.Core.Runners
{
    public class Replay
    {
        public IGenericRepository<Application> ApplicationRepository { get; set; }

        #region Public Methods and Operators

        public void Run(string applicationId, string runName)
        {
            var app = this.ApplicationRepository.FindBy(a => a.Id == applicationId).FirstOrDefault();
            var run = app.Runs.FirstOrDefault(r => r.Name == runName);
            //foreach (var run in runs)
            {
                ReplayContext.Init(run);

                foreach (var point in run.Points.Where(p => p.IsStartPoint))
                {
                    var startpoints = Configuration.Configure.Container.Resolve(point.GetType());
                    Invoke(startpoints, point);
                }
            }
        }

        private static void Invoke(object sp, Point point)
        {
            var method = sp.GetType().GetMethod(point.MethodName);
            var parameters =
                point.Parameters.Where(p => !p.IsReturn)
                    .Select(p => JsonConvert.DeserializeObject(p.Value, p.GetType()))
                    .ToArray();
            var result = method.Invoke(sp, parameters);
            var compareLogic = new CompareLogic();
            var savedReturnValue =
                Convert.ChangeType(
                    JsonConvert.DeserializeObject(point.ReturnValue.Value, point.ReturnValue.GetType()),
                    point.ReturnValue.GetType());

            var coparisonResult = compareLogic.Compare(savedReturnValue, result);
            Trace.Assert(coparisonResult.AreEqual, coparisonResult.DifferencesString);
        }

        #endregion
    }
}