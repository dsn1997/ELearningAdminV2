#nullable disable
using IIG.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IIG.Core.Entities
{
    public partial class CourseDescriptionDetail
    {
        public CourseDescriptionDetail()
        {
        }

        public Guid Id { get; set; }
        public Guid CourseDescriptionId { get; set; }
        public string Content { get; set; }
        public bool? IsUrl { get; set; }
        public string Url { get; set; }
        public short? SortOrder { get; set; }
        public Guid? ImageFileId { get; set; }
        public Guid? TTeacherId { get; set; }
        public Guid? TStudentId { get; set; }
        public Guid? TStudentReviewId { get; set; }
        public Guid? TFaqId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual File ImageFile { get; set; }
        public virtual TTeacher TTeacher { get; set; }
        public virtual TStudent TStudent { get; set; }
        public virtual TStudentReview TStudentReview { get; set; }
        public virtual TFaq TFaq { get; set; }
        public virtual CourseDescription CourseDescription { get; set; }
    }
}