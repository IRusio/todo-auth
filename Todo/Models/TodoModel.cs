using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Models
{
    public class TodoModel : PreTodoModel
    {
        public int Id { get; set; }

        public string TaskOwner { get; set; }
    }

    public class PreTodoModel
    {
        public string TaskHeader { get; set; }
        public string TaskContent { get; set; }
    }
}
