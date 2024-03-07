using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FPTJOB.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Email")]
        public string UserId { get; set; }
        [DisplayName("Name")]
        public string FullName { get; set; }
        public string Address {  get; set; } 
        public string Skill {  get; set; }
        public string Education {  get; set; }

        [DisplayName("CV")]
        public string MyFile { get; set; }

        [NotMapped] 
        public IFormFile ImageFile { get; set; }

        [InverseProperty("ObjProfile")]
        public virtual ICollection<ApplyJob>? ApplyJobs { get; set; }
    }
}
