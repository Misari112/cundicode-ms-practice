using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ms_practice.Models
{
    public class Stdin
    {
        [Key]
        public int Id { get; set; }
        public int CodeId { get; set; }
        public string? Input { get; set; }
        public string? ExpectedOutput { get; set; }
    }
}
