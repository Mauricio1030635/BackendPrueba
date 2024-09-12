using SittyCia.Core.Models;

namespace SittyCia.Core.Repository
{
    public interface ITaskService : IGenericService<TaskEntity>
    {
        Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(string userId);
    }
}
