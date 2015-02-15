using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NInsight.Core.Domain
{
    public class Run : BaseEntity
    {
        public Run()
        {
            this.Created = DateTime.Now.ToUniversalTime();
            this.RunId = Guid.NewGuid();
            this.Points = new List<Point>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid RunId { get; set; }

        public string Name { get; set; }

        public virtual List<Point> Points { get; set; }

        public DateTime Created { get; set; }

        public string ApplicationId { get; set; }
    }
}