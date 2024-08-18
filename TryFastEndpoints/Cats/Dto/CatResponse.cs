namespace TryFastEndpoints.Cats.Dto
{
    public record CatsListResponse
    {
        public required IEnumerable<CatResponse> Cats { get; set; }
    }

    public record CatResponse(int Id, string Name, string Breed, string Gender, int Age);
}