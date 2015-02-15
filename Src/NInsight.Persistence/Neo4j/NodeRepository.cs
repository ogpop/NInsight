using System;
using System.Linq;

using Neo4jClient;

using NInsight.Core.Domain;
using NInsight.Core.Repositories;

namespace NInsight.Persistence.Neo4j
{
    internal class NodeRepository : INodeRepository
    {
        #region Static Fields

        public GraphClient graphClient { get; set; }
         
        #endregion

        #region Public Methods and Operators

        public void Create(Application newApp)
        {
            this.graphClient.Cypher.Merge("(app:Application { Id: {newApp}.Id })")
                .OnCreate()
                .Set("app = {newApp}")
                .WithParams(new { newApp })
                .ExecuteWithoutResults();
        }

        public void AddRun(Application application, Run run)
        {
            this.graphClient.Cypher.Match("(app:Application)")
                .Where((Application app) => app.Id == run.ApplicationId)
                .Create("app-[:Has]->(run:Run {newRun})")
                .WithParam("newRun", run)
                .ExecuteWithoutResults();
        }

        public void AddClass(ClassType classType)
        {
            this.graphClient.Cypher.Match("(run:Run)")
                .Where((Run run) => run.RunId == classType.RunId)
                .Create("run-[:Has]->(classType:ClassType {newClassType})")
                .WithParam("newClassType", classType)
                .ExecuteWithoutResults();

            //     runs.AddOrUpdate(run.Key, run, (k, existingVal) => { return existingVal; });
        }

        public void AddPoint(Point point)
        {
            point.ToNode();
            this.graphClient.Cypher.Match("(ct:ClassType)")
                .Where((ClassType ct) => ct.TypeFullName == point.TypeFullName)
                .Create("ct-[:HAS]->(point:Point {newPoint})")
                .WithParam("newPoint", point)
                .ExecuteWithoutResults();

            var parentPoint = this.GetPoint(point.ParentPointId);
            var pointExisting = this.GetPoint(point.PointId);

            this.graphClient.Cypher.Match("(point1:Point)", "(point2:Point)")
                .Where((Point point1) => point1.PointId == point.ParentPointId)
                .AndWhere((Point point2) => point2.PointId == point.PointId)
                .Create("point1-[:CALLS]->point2")
                .ExecuteWithoutResults();

            this.AddClass(point.Class);
        }

        public Point GetPoint(Guid pointId)
        {
            return
                this.graphClient.Cypher.Match("(point:Point)")
                    .Where((Point point) => point.PointId == pointId)
                    .Return(point => point.As<Point>())
                    .Results.FirstOrDefault();
        }

        public void AddParameter(Point point1, Parameter parameter)
        {
            this.graphClient.Cypher.Match("(point:Point)")
                .Where((Point point) => point.PointId == parameter.PointId)
                .Create("point-[:Has]->(param:Parameter {newParameter})")
                .WithParam("newParameter", parameter)
                .ExecuteWithoutResults();

            //     runs.AddOrUpdate(run.Key, run, (k, existingVal) => { return existingVal; });
        }

        internal void UpdatePointReturnParameter(Point point1)
        {
            this.graphClient.Cypher.Match("(point:Point)")
                .Where((Point point) => point.PointId == point1.PointId)
                .Set("point.ReturnArgument = {param}")
                .WithParam("param", point1.ToNode().ReturnArgument)
                .ExecuteWithoutResults();
        }

        public void AddReturnParameter(Point point1, Parameter parameter)
        {
            this.graphClient.Cypher.Match("(point:Point)")
                .Where((Point point) => point.PointId == parameter.PointId)
                .Create("point-[:RETURNS]->(param:Parameter {newParameter})")
                .WithParam("newParameter", parameter)
                .ExecuteWithoutResults();
        }

        #endregion
    }
}