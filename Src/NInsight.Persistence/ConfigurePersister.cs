using System;
using System.Data.Entity;
using Castle.MicroKernel.Registration;

using Neo4jClient;

using NInsight.Core.Config;
using NInsight.Core.Domain;
using NInsight.Core.Repositories;
using NInsight.Persistence.EF;
using NInsight.Persistence.Neo4j;

namespace NInsight.Persistence
{
    public static class ConfigurePersister
    {
        public static void AddEntityFrameworkPersiting(this Configure config)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NInsightContext>());
     
            Configuration.Configure.Container.Register(
                Component.For<IGenericRepository<Application>>()
                    .Instance(new EF.GenericRepository<Application>(new NInsightContext())));
            Configuration.Configure.Container.Register(
                Component.For<IGenericRepository<Point>>()
                    .Instance(new EF.GenericRepository<Point>(new NInsightContext())));

            Configuration.Configure.Container.Register(
              Component.For<IGenericRepository<Run>>()
                  .Instance(new EF.GenericRepository<Run>(new NInsightContext())));


            Configuration.Configure.Container.Register(
                 Component.For<ISystemRepository>()
                     .ImplementedBy<SystemRepository>());

        }

        public static void AddNeo4jPersiting(this Configure config)
        {
            Configuration.Configure.Container.Register(
               Component.For<INodeRepository>()
                   .ImplementedBy<NodeRepository>());

            if (NInsightSettings.Settings.Neo4j.Use)
            {
                var graphClient = new GraphClient(new Uri(NInsightSettings.Settings.Neo4j.Url));
                graphClient.Connect();
                Configuration.Configure.Container.Register(Component.For<GraphClient>().Instance(graphClient));

            }

        }
    }
}