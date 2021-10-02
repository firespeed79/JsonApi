using System.ComponentModel.DataAnnotations;

namespace JsonApi.Tests.Models
{
    public class Model
    {
        [MaxLength(10)]
        public string Text { get; set; }
        
        [Required]
        public string RequiredTest { get; set; }
    }
}