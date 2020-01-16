using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class PreTodoModel
    {
        public PreTodoModel(string taskHeader, string taskContent)
        {
            TaskHeader = taskHeader;
            TaskContent = taskContent;
        }

        public PreTodoModel()
        {
        }

        [Required]
        public string TaskHeader { get; set; }
        public string TaskContent { get; set; }
    }
}