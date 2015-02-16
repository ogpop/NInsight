using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neo4jClient;

using NInsight.Core.Domain;
using NInsight.Core.Repositories;

namespace NInsight.Persistence.Neo4j
{
    internal class SystemRepository : IPersistState
    {
        public GraphClient graphClient { get; set; }
        public void Create(Core.Domain.Point point)
        {
            var classType = point.Class;
            point.ToNode();
            this.graphClient.Cypher.Match("(ct:ClassType)")
                .Where((ClassType ct) => ct.TypeFullName == point.TypeFullName)
                .Create("ct-[:HAS]->(point:Point {newPoint})")
                .WithParam("newPoint", point)
                .ExecuteWithoutResults();

            

           
            point.Class = null;
            this.graphClient.Cypher.Match("(point1:Point)", "(point2:Point)")
                .Where((Point point1) => point1.PointId == point.ParentPointId)
                .AndWhere((Point point2) => point2.PointId == point.PointId)
                .Create("point1-[:CALLS]->point2")
                .ExecuteWithoutResults();

            this.AddClass(classType);
        }

        private void AddClass(ClassType classType)
        {
            //this.graphClient.Cypher.Merge("(app:Application { Id: {newApp}.Id })")
            //    .OnCreate()
            //    .Set("app = {newApp}")
            //    .WithParams(new { newApp })
            //    .ExecuteWithoutResults();


            this.graphClient.Cypher.Merge("(run:Run)")
                .Where((Run run) => run.Id == classType.RunId)
                .Create("run-[:Has]->(classType:ClassType {newClassType})")
                .WithParam("newClassType", classType)
                .ExecuteWithoutResults();

            //     runs.AddOrUpdate(run.Key, run, (k, existingVal) => { return existingVal; });
        }
        public void CreateIfNotExists(Core.Domain.Application application)
        {
            application.Runs = null;
            this.graphClient.Cypher.Merge("(app:Application { Id: {application}.Id })")
                .OnCreate()
                .Set("app = {application}")
                .WithParams(new { application })
                .ExecuteWithoutResults();
        }

        public void Create(Core.Domain.Run run)
        {
            this.graphClient.Cypher.Match("(app:Application)")
                .Where((Application app) => app.Id == run.ApplicationId)
                .Create("app-[:Has]->(run:Run {newRun})")
                .WithParam("newRun", run)
                .ExecuteWithoutResults();
        }

        public Core.Domain.Application GetApplicationById(string applicationId)
        {
            return
                     this.graphClient.Cypher.Match("(application:Application)")
                         .Where((Application application) => application.Id == applicationId)
                         .Return(application => application.As<Application>())
                         .Results.FirstOrDefault();
        }
    }
}
