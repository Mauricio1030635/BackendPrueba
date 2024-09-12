namespace SittyCia.Core.Dto
{
    public class CreateTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completado { get; set; }
        public string UserId { get; set; }
    }
}
