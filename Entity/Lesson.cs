using System.Text.Json.Serialization;

namespace InfoTree.Entity
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }


        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        [JsonIgnore]
        public ICollection<LessonFile> Files { get; set; }
    }
}
