namespace GrooveNest.Domain.Entities
{
    /// <summary>
    /// Predefined roles like Admin, Artist, Listener.
    /// </summary>
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
