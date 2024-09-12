namespace SittyCia.Core.Dto
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completado { get; set; }
    }
}
