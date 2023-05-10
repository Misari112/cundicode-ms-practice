namespace ms_practice.Models.Response
{
    public class ExerciseToEditor
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Examples { get; set; }
        public float TimeLimit { get; set; }
        public int MemoryLimit { get; set; }
        public string? FunctionSignature { get; set; }
        public string? SolutionTemplate { get; set; }
        public string? Author { get; set; }
    }
}
