namespace SittyCia.Core.Models
{
    public class TaskEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completado { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
