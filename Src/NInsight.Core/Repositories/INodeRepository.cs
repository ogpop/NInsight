using System;

using NInsight.Core.Domain;

namespace NInsight.Core.Repositories
{
    internal interface INodeRepository
    {
        void Create(Application newApp);

        void AddRun(Application application, Run run);

        void AddClass(ClassType classType);

        void AddPoint(Point point);

        Point GetPoint(Guid pointId);

        void AddParameter(Point point1, Parameter parameter);

        void AddReturnParameter(Point point1, Parameter parameter);
    }
}