using SittyCia.Models;

namespace SittyCia.Service.IService
{
    public interface ITaskService : IGenericService<TaskEntity>
    {
        Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(string userId);
    }
}
