using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Application.Models
{
    public class BannerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string LinkUrl { get; set; }
        public int? SortOrder { get; set; }
        public string FullTextSearch { get; set; }
        public int? Status { get; set; }
        public DateTime? EndTime { get; set; }
        public string ColorCode { get; set; }
        public string TitleBefore { get; set; }
        public string TitleAfter { get; set; }
        public string ContentBefore { get; set; }
        public string ContentAfter { get; set; }
        public string ImageFileUrl { get; set; }
    }

    public class GetBannerPagingDto : Pagination
    {
        public int? Type { get; set; }
    }

}
