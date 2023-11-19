namespace TaskManagerAPI.Repositories

{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TaskManagerAPI.Model;

    public class TaskRepository
    {
        private List<TaskModel> tasks = new List<TaskModel>();

        public IEnumerable<TaskModel> GetAllTasks()
        {
            return tasks.Where(t => t.IsActive);
        }

        public TaskModel GetTaskById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id && t.IsActive);
        }

        public void InsertTask(TaskModel task)
        {
            task.CreatedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;
            task.IsActive = true;
            tasks.Add(task);
        }

        public void UpdateTask(TaskModel updatedTask)
        {
            var existingTask = tasks.FirstOrDefault(t => t.Id == updatedTask.Id && t.IsActive);

            if (existingTask != null)
            {
                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.UpdatedAt = DateTime.UtcNow;
            }
        }

        public void SoftDeleteTask(int id)
        {
            var existingTask = tasks.FirstOrDefault(t => t.Id == id && t.IsActive);

            if (existingTask != null)
            {
                existingTask.IsActive = false;
                existingTask.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
