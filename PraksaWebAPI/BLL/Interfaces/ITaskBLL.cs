using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.BLL.Interfaces
{
    public interface ITaskBLL
    {
        public List<Models.Task> GetTasks();
        public int AddTask(TaskModel newTask);
        public int DeleteTask(long id);
        public List<Models.Task> TasksForUser(long id);
        public int FinishTask(long id, string role);

    }
}
