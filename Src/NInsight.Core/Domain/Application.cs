using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NInsight.Core.Domain
{
    public class Application : BaseEntity
    {
        public Application()
        {
            this.Runs = new List<Run>();
        }

        [Key]
        public string Id { get; set; }

        public virtual List<Run> Runs { get; set; }
    }
}