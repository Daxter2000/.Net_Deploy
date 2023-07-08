using System.ComponentModel.DataAnnotations;

namespace universityAPI.models.DataModels
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; } =  string.Empty;

        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}
