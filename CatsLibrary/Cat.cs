namespace CatsLibrary
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Cat
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Gender Gender { get; set; }
        public required string Coloring { get; set; }
        public string Breed { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
