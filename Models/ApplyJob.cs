using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTJOB.Models
{
    public class ApplyJob
    {
        [Key]
        public int Id { get; set; }
        public DateTime RegDate { get; set; }

        [ForeignKey("ProfileId")]
        public int ProfileId { get; set; }

        public virtual Profile? ObjProfile { get; set; }

        [ForeignKey("JobId")]
        public int JobId { get; set; }

        public virtual Job? ObjJob { get; set; }
    }
}
