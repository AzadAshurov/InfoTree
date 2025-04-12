namespace InfoTree.Entity
{
    public class LessonFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
