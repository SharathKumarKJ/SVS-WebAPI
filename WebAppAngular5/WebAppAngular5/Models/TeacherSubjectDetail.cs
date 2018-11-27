namespace WebAppAngular5.Models
{
    public class TeacherSubjectDetail: BaseEntity
    {
        public Teacher Teacher { get; set; }

        public Subject Subject { get; set; }

    }
}