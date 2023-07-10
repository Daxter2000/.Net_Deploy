using universityAPI.models.DataModels;

namespace universityAPI.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Student> GetStudentsWithNoCourses();


    }
}
