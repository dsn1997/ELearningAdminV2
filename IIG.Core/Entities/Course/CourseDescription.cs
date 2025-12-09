#nullable disable
using IIG.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IIG.Core.Entities
{
    public partial class CourseDescription
    {
        public CourseDescription()
        {
        }

        public Guid Id { get; set; }
        public Guid CourseId { get; set; }

        [StringLength(255)]
        public string Title { get; set; }
        public int? SortOrder { get; set; }
        public ECourseDescriptionType Type { get; set; }
        public bool? IsShowInWeb { get; set; }
        public bool? IsShowInApp { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<CourseDescriptionDetail> CourseDescriptionDetails { get; set; }
    }
}