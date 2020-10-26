using PraksaWebAPI.BLL.Interfaces;
using PraksaWebAPI.DAL.Interfaces;
using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.BLL
{
    public class TaskCommentBLL : ITaskCommentBLL
    {
        private readonly ITaskCommentDAL _taskCommentDAL;

        public TaskCommentBLL(ITaskCommentDAL taskCommentDAL)
        {
            this._taskCommentDAL = taskCommentDAL;
        }

        public int AddTaskComment(TaskComment taskComment)
        {
            return _taskCommentDAL.AddTaskComment(taskComment);
        }

        public List<TaskComment> CommentsForTask(int taskid)
        {
            return _taskCommentDAL.CommentsForTask(taskid);
        }
    }
}
