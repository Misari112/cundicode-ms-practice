using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ms_practice.Data;
using ms_practice.Models;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ms_practice.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ms_practice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CodesController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public CodesController(IConfiguration configuration, ApplicationDbContext context)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("solved/{id}")]
        public async Task<IActionResult> SolvedExercisesGet(string id)
        {
            int tries = 0;
            int solved = 0;
            SolvedExercises solvedExercices = new SolvedExercises();
            List<ExerciseToProfileData> exercisesToProfileData = new List<ExerciseToProfileData>();
            List<CompleteProgrammingExercise> completeProgrammingExercises = await _context.CompleteExercises.Where(e => e.IdUser == id).ToListAsync();
            for (int i=0;i<completeProgrammingExercises.Count;i++) {
                completeProgrammingExercises.ElementAt(i).ProgrammingExercise = await _context.Exercises.Where(e => e.Id == completeProgrammingExercises.ElementAt(i).ProgrammingExerciseId).FirstAsync();
                if (completeProgrammingExercises.ElementAt(i).IsCompleted == false)
                {
                    tries++;
                }
                else {
                    solved++;
                }
                ExerciseToProfileData exerciseToProfileData = new ExerciseToProfileData();
                exerciseToProfileData.Title = completeProgrammingExercises.ElementAt(i).ProgrammingExercise.Title;
                exerciseToProfileData.DateTime = completeProgrammingExercises.ElementAt(i).SendDate;
                exerciseToProfileData.Id = completeProgrammingExercises.ElementAt(i).ProgrammingExercise.Id;
                exerciseToProfileData.IsCompleted = completeProgrammingExercises.ElementAt(i).IsCompleted;
                exerciseToProfileData.Language = completeProgrammingExercises.ElementAt(i).Language;
                exercisesToProfileData.Add(exerciseToProfileData);
            }
            solvedExercices.ExercisesToProfileData = exercisesToProfileData;
            solvedExercices.Tried = tries;
            solvedExercices.Solved = solved;
            string jsonString = JsonConvert.SerializeObject(solvedExercices);
            return Content(jsonString, "application/json");
        }
        // GET: api/<CodesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<ProgrammingExercise> programmingExercises = await _context.Exercises.ToListAsync();
            List<ExerciseToList> exerciseToLists = new List<ExerciseToList>();
            foreach (ProgrammingExercise e in programmingExercises) { 
                ExerciseToList exerciseToList = new ExerciseToList();
                exerciseToList.Id = e.Id;
                exerciseToList.Title = e.Title;
                exerciseToList.DifficultyLevel = e.DifficultyLevel;
                exerciseToList.Categories = e.Categories;
                exerciseToList.Description = e.Description;
                exerciseToList.DateCreated = e.DateCreated;
                exerciseToList.LastUpdated = e.LastUpdated;
                exerciseToLists.Add(exerciseToList);
            }

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string jsonString = JsonConvert.SerializeObject(exerciseToLists, Formatting.Indented, settings);
            return Content(jsonString, "application/json");
        }

        // GET api/<CodesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ProgrammingExercise programmingExercise = await _context.Exercises.Where(e => e.Id == id).FirstAsync();
            ExerciseToEditor exerciseToEditor = new ExerciseToEditor();
            exerciseToEditor.Id = id;
            exerciseToEditor.Title = programmingExercise.Title;
            exerciseToEditor.Description = programmingExercise.Description;
            exerciseToEditor.Examples = programmingExercise.Examples;
            exerciseToEditor.TimeLimit = programmingExercise.TimeLimit;
            exerciseToEditor.MemoryLimit = programmingExercise.MemoryLimit;
            exerciseToEditor.FunctionSignature = programmingExercise.FunctionSignature;
            exerciseToEditor.SolutionTemplate = programmingExercise.SolutionTemplate;
            exerciseToEditor.Author = programmingExercise.Author;
            string jsonString = JsonConvert.SerializeObject(exerciseToEditor);
            return Content(jsonString, "application/json"); 
        }

        // POST api/<CodesController>/add
        [HttpPost("add")]
        public async Task<ActionResult> AddExercisePost([FromBody] NewExercise request)
        {
            ProgrammingExercise programmingExercise = new ProgrammingExercise();
            programmingExercise.Title = request.Title;
            programmingExercise.DifficultyLevel = request.DifficultyLevel;
            programmingExercise.Categories = request.Categories;
            programmingExercise.Description = request.Description;
            programmingExercise.Examples = JsonConvert.SerializeObject(request.Examples);
            programmingExercise.TimeLimit = request.TimeLimit;
            programmingExercise.MemoryLimit = request.MemoryLimit;
            programmingExercise.FunctionSignature = request.FunctionSignature;
            programmingExercise.SolutionTemplate = request.SolutionTemplate;
            programmingExercise.Hints = request.Hints;
            programmingExercise.TestCases = JsonConvert.SerializeObject(request.TestCases);
            programmingExercise.DateCreated = DateTime.Now;
            programmingExercise.LastUpdated = DateTime.Now;

            _context.Exercises.Add(programmingExercise);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Exercise Correct!");
        }

        // POST api/<CodesController>/ExecuteExamples
        [HttpPost("ExecuteExamples")]
        public async Task<ActionResult> ExecuteExamplesPost([FromBody] ExecuteExamples executeExamples)
        {
            _httpClient.BaseAddress = new Uri("https://api.jdoodle.com/");
            ProgrammingExercise programmingExercise = await _context.Exercises.Where(e => e.Id == executeExamples.Id).FirstAsync();
            List<Examples> examples = JsonConvert.DeserializeObject<List<Examples>>(programmingExercise.Examples);
            List<ResponseExample> responseExecuteExamples = new List<ResponseExample>();
            for (int i=0; i<examples.Count;i++) {
                
                var data = new
                {
                    clientId = _configuration.GetSection("JDoodleAPIKeys")["ClientId"],
                    clientSecret = _configuration.GetSection("JDoodleAPIKeys")["ClientSecret"],
                    script = executeExamples.Script,
                    stdin = examples.ElementAt(i).input,
                    language = executeExamples.Language,
                    versionIndex = executeExamples.Version,
                    compileOnly = false,
                };

                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("v1/execute", content);

                string contenido = await response.Content.ReadAsStringAsync();

                ResponseExample respoAux = new ResponseExample();
                dynamic outputObject = JsonConvert.DeserializeObject(contenido);
                respoAux.Output = contenido;
                string eOutAux = examples.ElementAt(i).output;
                respoAux.ExpectedOutput = eOutAux;
                if (outputObject.output == respoAux.ExpectedOutput)
                {
                    respoAux.State = true;
                }
                else
                {
                    respoAux.State = false;
                }
                responseExecuteExamples.Add(respoAux);

            }
            string jsonString = JsonConvert.SerializeObject(responseExecuteExamples);
            return Content(jsonString, "application/json");
        }

        // POST api/<CodesController>/ExecuteTestCases
        [HttpPost("ExecuteTestCases")]
        public async Task<ActionResult> ExecuteTestCasesPost([FromBody] ExecuteTestCases executeTestCases)
        {
            _httpClient.BaseAddress = new Uri("https://api.jdoodle.com/");
            ProgrammingExercise programmingExercise = await _context.Exercises.Where(e => e.Id == executeTestCases.Id).FirstAsync();
            List<Examples> testCases = JsonConvert.DeserializeObject<List<Examples>>(programmingExercise.TestCases);
            List<ResponseExecuteTestCases> responseExecuteTestCases = new List<ResponseExecuteTestCases>();
            Boolean complete = true;
            for (int i = 0; i < testCases.Count; i++)
            {

                var data = new
                {
                    clientId = _configuration.GetSection("JDoodleAPIKeys")["ClientId"],
                    clientSecret = _configuration.GetSection("JDoodleAPIKeys")["ClientSecret"],
                    script = executeTestCases.Script,
                    stdin = testCases.ElementAt(i).input,
                    language = executeTestCases.Language,
                    versionIndex = executeTestCases.Version,
                    compileOnly = false,
                };

                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("v1/execute", content);

                string contenido = await response.Content.ReadAsStringAsync();

                ResponseExecuteTestCases respoAux = new ResponseExecuteTestCases();
                dynamic outputObject = JsonConvert.DeserializeObject(contenido);
                respoAux.Output = contenido;
                string eOutAux = testCases.ElementAt(i).output;
                respoAux.ExpectedOutput = eOutAux;
                if (outputObject.output == respoAux.ExpectedOutput)
                {
                    respoAux.State = true;
                }
                else
                {
                    respoAux.State = false;
                    complete = false;
                }
                responseExecuteTestCases.Add(respoAux);
            }
            Boolean alreadyCompleted = _context.CompleteExercises.Any(t => t.Language == executeTestCases.Language
                                                                            && t.ProgrammingExercise == programmingExercise
                                                                            && t.IdUser == executeTestCases.IdUser
                                                                            && t.IsCompleted == true);
            if (alreadyCompleted) { return new ObjectResult("Ya ha solucionado este ejercicio en este lenguaje."); }
            if (complete)
            {
                
                CompleteProgrammingExercise completeProgrammingExercise = new CompleteProgrammingExercise();
                completeProgrammingExercise.ProgrammingExercise = programmingExercise;
                completeProgrammingExercise.Script = executeTestCases.Script;
                completeProgrammingExercise.Version = executeTestCases.Version;
                completeProgrammingExercise.Language = executeTestCases.Language;
                completeProgrammingExercise.SendDate = DateTime.Now;
                completeProgrammingExercise.IdUser = executeTestCases.IdUser;
                completeProgrammingExercise.IsCompleted = true;
                _context.CompleteExercises.Add(completeProgrammingExercise);
                await _context.SaveChangesAsync();
                return new ObjectResult("Completado");
            }
            else {
                CompleteProgrammingExercise completeProgrammingExercise = new CompleteProgrammingExercise();
                completeProgrammingExercise.ProgrammingExercise = programmingExercise;
                completeProgrammingExercise.Script = executeTestCases.Script;
                completeProgrammingExercise.Version = executeTestCases.Version;
                completeProgrammingExercise.Language = executeTestCases.Language;
                completeProgrammingExercise.SendDate = DateTime.Now;
                completeProgrammingExercise.IdUser = executeTestCases.IdUser;
                completeProgrammingExercise.IsCompleted = false;
                _context.CompleteExercises.Add(completeProgrammingExercise);
                await _context.SaveChangesAsync();
                return new ObjectResult("No es correcto");
            }
        }

        // POST api/<CodesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ExecuteCustom request)
        {
            _httpClient.BaseAddress = new Uri("https://api.jdoodle.com/");
            var data = new
            {
                clientId = _configuration.GetSection("JDoodleAPIKeys")["ClientId"],
                clientSecret = _configuration.GetSection("JDoodleAPIKeys")["ClientSecret"],
                script = request.Script,
                stdin = request.Stdin,
                language = request.Language,
                versionIndex = request.Version,
                compileOnly = false,
            };

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("v1/execute", content);

            string contenido = await response.Content.ReadAsStringAsync();

            return new OkObjectResult(contenido);
        }

        // PUT api/<CodesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CodesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
