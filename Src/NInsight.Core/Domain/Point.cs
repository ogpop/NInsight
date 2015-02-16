using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Newtonsoft.Json;

namespace NInsight.Core.Domain
{
    public class Point : BaseEntity
    {
        public Point()
        {
            this.PointId = Guid.NewGuid();
        }

        [Key]
        public Guid PointId { get; set; }

        public Guid ParentPointId { get; set; }
       // [ForeignKey("RunId")]
        public Guid Run1Id { get; set; }

        //[InverseProperty("Id")]
       // [ForeignKey("RunId")]
       // public Run Run { get; set; }

        public Guid Class1Id { get; set; }

        public string TypeFullName { get; set; }

        public string MethodName { get; set; }

        public string FriendlyName { get; set; }

        public string HashKey { get; set; }

        public bool IsStartPoint { get; set; }

        public virtual List<Parameter> Parameters { get; set; }

        public virtual string[] Arguments { get; set; }

        public virtual Parameter ReturnValue { get; set; }

        public virtual string ReturnArgument { get; set; }

        public virtual ClassType Class { get; set; }

        public Type GetType()
        {
            return Type.GetType(this.TypeFullName);
        }

        public Point ToNode()
        {
            if (this.Parameters != null)
            {
                this.Arguments = new string[this.Parameters.Count];
                this.Arguments = this.Parameters.Select(p => JsonConvert.SerializeObject(p)).ToArray();
                this.Parameters = null;
            }

            this.ReturnArgument = JsonConvert.SerializeObject(this.ReturnValue);
            this.ReturnValue = null;
            this.Class = null;
            return this;
        }
    }
}