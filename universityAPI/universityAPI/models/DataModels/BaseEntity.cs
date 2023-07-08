using System.ComponentModel.DataAnnotations;

namespace universityAPI.models.DataModels
{
    public class BaseEntity
    {
        //Crearemos la definicion del modelo de la tabla
        [Required]
        [Key]
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; } 

        public string DeletedBy { get; set; } = string.Empty; 
        public DateTime? DeletedAt  { get; set; }

        public bool IsDeleted { get; set; } = false;


    }
}
