using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.BLL.Interfaces
{
    public interface ITaskCommentBLL
    {
        public int AddTaskComment(TaskComment taskComment);
        public List<TaskComment> CommentsForTask(int taskid); 
    }
}
