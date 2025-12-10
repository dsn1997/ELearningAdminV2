using IIG.Application.Models.MockTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Application.Models
{
    public class Menu_SeftStudyProgram_ExamToolDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string NameNonAscii { get; set; }
        public string LinkTreeUrl { get; set; }
        public string ImageFileUrl { get; set; }
        public IEnumerable<Menu_SeftStudyProgram_ExamToolClassDto> Classes { get; set; }
    }
    public class Menu_SeftStudyProgram_ExamToolPagingInputDto : Pagination
    {
        public string LanguageCode { get; set; }
        public Guid? Level0CategoryId { get; set; }
    }

    public class Menu_SeftStudyProgram_ExamToolClassDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CoursePriceId { get; set; }
        public bool IsPurchased { get; set; }
    }
}
