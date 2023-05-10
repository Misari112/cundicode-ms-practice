using System;

namespace ms_practice.Models.Response
{
    public class ExerciseToList
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? DifficultyLevel { get; set; }
        public string? Categories { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
