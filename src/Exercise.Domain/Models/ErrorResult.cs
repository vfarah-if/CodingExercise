using System.Collections.Generic;

namespace Exercise.Domain.Models
{
    public class ErrorResult
    {
        public Dictionary<string, object> Errors { get; set; }
        public bool HasErrors => Errors?.Count > 0;
        public int Status { get; set; }
        public string Title { get; set; }
        public string TraceId { get; set; }
        
    }
}
