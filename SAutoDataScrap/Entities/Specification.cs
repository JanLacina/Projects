namespace SAutoDataScrap.Entities
{
    public class Specification
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }

        public Guid CarId { get; set; }
        public Car? Car { get; set; }
    }
}
