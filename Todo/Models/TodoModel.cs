using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Models
{
    public class TodoModel : PreTodoModel
    {

        public TodoModel(string taskHeader, string taskContent, int id, string taskOwner)
        {
            Id = id;
            TaskOwner = taskOwner;
            TaskHeader = taskHeader;
            TaskContent = taskContent;
        }

        public TodoModel(PreTodoModel model)
        {
            this.TaskHeader = model.TaskHeader;
            this.TaskContent = model.TaskContent;
        }

        public TodoModel()
        {
        }

        public int Id { get; set; }

        public string TaskOwner { get; set; }
    }
}