using PraksaWebAPI.BLL.Interfaces;
using PraksaWebAPI.DAL.Interfaces;
using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.BLL
{
    public class TaskBLL : ITaskBLL
    {
        private readonly ITaskDAL _taskDAL;
        private readonly IEmployeeDAL _employeeDAL;

        public TaskBLL(ITaskDAL taskDAL,IEmployeeDAL employeeDAL)
        {
            this._taskDAL = taskDAL;
            this._employeeDAL = employeeDAL;
        }

        public int AddTask(TaskModel newTask)
        {
            var res =  _taskDAL.AddTask(newTask);
            for(int i = 0; i<newTask.Employees.Length; i++)
            {
                _taskDAL.AddTaskUser(new TaskUser { EmployeeID = newTask.Employees[i], TaskID = res});
            }
            return 0;
        }

        public int DeleteTask(long id)
        {
            return _taskDAL.DeleteTask(id);
        }

        public int FinishTask(long id, string role)
        {
             var res =  _taskDAL.FinishTask(id, role);
            foreach(var item in res)
            {
                Employee employee = _employeeDAL.GetEmployeeById(item);
                employee.FinishedTasks = employee.FinishedTasks + 1;
                _employeeDAL.UpdateEmploye(employee);

            }
            return 0;
        }

        public List<Models.Task> GetTasks()
        {
            return _taskDAL.GetTasks();
        }

        public List<Models.Task> TasksForUser(long id)
        {
            return _taskDAL.TasksForUser(id);
        }
    }
}
