using System;
using System.ComponentModel.DataAnnotations;

namespace ms_practice.Entities
{
    public class CompleteProgrammingExercise
    {
        [Key]
        public int Id { get; set; }
        public ProgrammingExercise ProgrammingExercise { get; set; }
        public int ProgrammingExerciseId { get; set; }
        public string Script { get; set; }
        public string Language { get; set; }
        public string Version { get; set; }
        public string IdUser { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
