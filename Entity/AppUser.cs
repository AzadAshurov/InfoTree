using Microsoft.AspNetCore.Identity;

namespace InfoTree.Entity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
