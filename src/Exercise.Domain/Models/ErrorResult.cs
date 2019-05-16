using System.Collections.Generic;

namespace Exercise.Domain.Models
{
    public class ErrorResult
    {
        public string Title { get; set; }
        public Dictionary<string, object> Errors { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
    }
}
