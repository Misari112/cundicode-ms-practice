using System.ComponentModel.DataAnnotations;

namespace ms_practice.Entities
{
    public class ProgrammingExercise
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? DifficultyLevel { get; set; }
        public string? Categories { get; set; }
        public string? Description { get; set; }
        public string? Examples { get; set; }
        public float TimeLimit { get; set; }
        public int MemoryLimit { get; set; }
        public string? FunctionSignature { get; set; }
        public string? SolutionTemplate { get; set; }
        public string? Hints { get; set; }
        public string? TestCases { get; set; }
        public bool Visibility { get; set; }
        public string? Author { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
