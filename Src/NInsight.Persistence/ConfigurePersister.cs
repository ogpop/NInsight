using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Castle.MicroKernel.Registration;

using Neo4jClient;

using NInsight.Core.Config;
using NInsight.Core.Domain;
using NInsight.Core.Repositories;

using BeyondTestContext = NInsight.Persistence.EF.BeyondTestContext;

namespace NInsight.Persistence
{
    public static class ConfigurePersister
    {
        public static void Do(this Configure config)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BeyondTestContext>());
     
            Configuration.Configure.Container.Register(
                Component.For<IGenericRepository<Application>>()
                    .Instance(new EF.GenericRepository<Application>(new BeyondTestContext())));
            Configuration.Configure.Container.Register(
                Component.For<IGenericRepository<Point>>()
                    .Instance(new EF.GenericRepository<Point>(new BeyondTestContext())));


            if (NInsightSettings.Settings.Neo4j.Use)
            {
                var graphClient = new GraphClient(new Uri(NInsightSettings.Settings.Neo4j.Url));
                graphClient.Connect();
                Configuration.Configure.Container.Register(Component.For<GraphClient>().Instance(graphClient));

            }

        }
    }
}