namespace ms_practice.Models.Response
{
    public class SolvedExercises
    {
        public int Tried { get; set; }
        public int Solved { get; set; }
        public List<ExerciseToProfileData> ExercisesToProfileData { get; set; }
    }

}
