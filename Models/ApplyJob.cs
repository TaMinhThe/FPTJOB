using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTJOB.Models
{
    public class ApplyJob
    {
        public int Id { get; set; }
 

        [ForeignKey("ProfileId")]
        [DisplayName("Profile")]
        public int ProfileId { get; set; }

        public virtual Profile? Profile { get; set; }

        [ForeignKey("JobId")]
        [DisplayName("Job")]
        public int JobId { get; set; }

        public virtual Job? Job { get; set; }
    }
}
