using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NInsight.Core.Domain
{
    public class ClassType : BaseEntity
    {
        public ClassType()
        {
            this.ClassId = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ClassId { get; set; }

        public Guid RunId { get; set; }

        public string TypeFullName { get; set; }

        public string FriendlyName { get; set; }
    }
}