using System.Text.Json.Serialization;

namespace InfoTree.Entity
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AppUserId { get; set; }


        [JsonIgnore]
        public AppUser AppUser { get; set; }
        [JsonIgnore]
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
