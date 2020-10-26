using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.DAL.Interfaces
{
    public interface ITaskDAL
    {
        public List<Models.Task> GetTasks();
        public int AddTask(TaskModel newTask);
        public int DeleteTask(long id);
        public List<int> FinishTask(long id, string role);
        public int AddTaskUser(TaskUser newTaskUser);
        public List<Models.Task> TasksForUser(long id);

    }
}
