using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NInsight.Core.Domain
{
    public class ClassType : BaseEntity
    {
        public ClassType()
        {
             
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
         

        public Guid RunId { get; set; }


        public string FriendlyName { get; set; }
    }
}