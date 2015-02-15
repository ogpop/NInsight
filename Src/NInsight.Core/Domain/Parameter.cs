using System;
using System.ComponentModel.DataAnnotations;

namespace NInsight.Core.Domain
{
    public class Parameter : BaseEntity
    {
        public Parameter()
        {
            this.ParameterId = Guid.NewGuid();
        }

        [Key]
        //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ParameterId { get; set; }

        public Guid PointId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public string TypeFullName { get; set; }

        public string Value { get; set; }

        public bool IsReturn { get; set; }

        public Type GetType()
        {
            return Type.GetType(this.TypeFullName);
        }

        public string KeyString()
        {
            return string.Format("{0},{1},{2}", this.TypeFullName, this.Name, this.Position);
        }
    }
}