using Microsoft.EntityFrameworkCore;
using SittyCia.Data;
using SittyCia.Models;
using SittyCia.Service.IService;

namespace SittyCia.Service
{
    public class TaskService : GenericService<TaskEntity>, ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(string userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }
    }

}
