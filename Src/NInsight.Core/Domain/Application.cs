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
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //   public int Id { get; set; }
        public string Id { get; set; }

        public virtual List<Run> Runs { get; set; }
    }
}