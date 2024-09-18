using System.ComponentModel.DataAnnotations;

namespace Route.PL.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}
