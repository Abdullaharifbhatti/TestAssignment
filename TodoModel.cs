using System.Text.Json;
using System;

namespace Company.Function
{
    public class TodoModel
    {
        public string Id  { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
        public string CreatedTime { get; set; }
    }
}