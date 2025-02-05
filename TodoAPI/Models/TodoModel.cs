namespace TodoAPI.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}