namespace IIG.Application.Models
{
    public class LiveClassLessonModel
    {
        public Guid Id { get; set; }
       
        public long? ClassinLessonId { get; set; }
        
        public DateTime LearningDate { get; set; }
        
        public DateTime LearningEndDate { get; set; }
    }
}
