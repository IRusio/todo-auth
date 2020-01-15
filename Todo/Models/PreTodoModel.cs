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

        public string TaskHeader { get; set; }
        public string TaskContent { get; set; }
    }
}