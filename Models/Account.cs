namespace FPTJOB.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public ICollection<Profile>? Profiles { get; set; }

    }
}
