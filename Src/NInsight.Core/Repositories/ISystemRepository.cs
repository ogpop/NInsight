using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NInsight.Core.Domain;

namespace NInsight.Core.Repositories
{
    internal interface ISystemRepository      {
        void Create(Point point);
        void CreateIfNotExists(Application application);
        void Create(Run run);
        Application GetApplicationById(string applicationId);
    }
}
