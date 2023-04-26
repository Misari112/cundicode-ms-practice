using System.Collections.Generic;

namespace ms_practice.Models
{
    public class NewExercise
    {
        public string? Title { get; set; }
        public string? DifficultyLevel { get; set; }
        public string? Categories { get; set; }
        public string? Description { get; set; }
        public List<Examples>? Examples { get; set; }
        public float TimeLimit { get; set; }
        public int MemoryLimit { get; set; }
        public string? FunctionSignature { get; set; }
        public string? SolutionTemplate { get; set; }
        public string? Hints { get; set; }
        public List<Examples>? TestCases { get; set; }
    }
    public class Examples
    {
        public string? input { get; set; }
        public string? output { get; set; }
    }
}
