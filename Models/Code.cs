using ms_practice.Models;
using System.ComponentModel.DataAnnotations;

namespace ms_practice.Models
{
    public class Code
    {
        [Key]
        public int Id { get; set; }
        public string? Program { get; set; }
        public List<Stdin>? Stdins { get; set; }
        public string? Language { get; set; }
        public string? Version { get; set; }
        
    }
}
