using System;
using System.Collections.Generic;
using System.Linq;

using NInsight.Core.Config;
using NInsight.Core.Domain;
using NInsight.Core.Mappers;
using NInsight.Core.Repositories.EF;
using NInsight.Core.Repositories.Neo4j;

namespace NInsight.Core.Context
{
   internal class RecordContext
    {
        [ThreadStatic]
        private static RecordContext _current;

        internal Point CurrentPoint;

        internal Stack<Point> PointStack = new Stack<Point>();

        internal Run Run;

        public IGenericRepository<Application> ApplicationRepository { get; set; }
        public NodeRepository NodeRepository { get; set; }

        internal static RecordContext Current
        {
            get
            {
                if (_current == null)
                {
                    _current = Configuration.Configure.Container.Resolve<RecordContext>();
                    _current.Init();
                }
                return _current;
            }
        }

        internal void Init()
        {
            
            var applicationId = AppDomain.CurrentDomain.FriendlyName;
            var application = this.ApplicationRepository.FindBy(a => a.Id == applicationId).FirstOrDefault();
            if (application == null)
            {
                application = new Application { Id = applicationId };
                this.ApplicationRepository.Add(application);
            }
            // this.runRepository.Create(application.Id, run);
            if (this.Run == null)
            {
                var run = new RunMapper().Do(application);
                this.Run = run;
                application.Runs.Add(run);

                this.ApplicationRepository.Edit(application);
                this.NodeRepository.AddRun(application, run);
            }
        }

        internal void BeginInvocation(Point point)
        {
            this.Run.Points.Add(point);
            this.CurrentPoint = point;
            if (this.PointStack.Count > 0)
            {
                point.ParentPointId = this.PointStack.Peek().PointId;
            }
            this.PointStack.Push(point);
        }

       internal void EndInvocation()
       {
           this.PointStack.Pop();
       }
    }
}