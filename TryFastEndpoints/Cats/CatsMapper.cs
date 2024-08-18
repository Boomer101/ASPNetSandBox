using CatsLibrary;
using TryFastEndpoints.Cats.Dto;

namespace TryFastEndpoints.Cats
{
    public static class CatsMapper
    {
        public static IEnumerable<CatResponse> ToCatsListResponse(IEnumerable<Cat> cats)
        {
            var catDtos = new List<CatResponse>();
            foreach (var cat in cats)
            {
                catDtos.Add(new CatResponse(cat.Id, cat.Name, cat.Breed, cat.Gender.ToString(), cat.Age));
            }

            return catDtos;
        }

        public static CatResponse ToCatResponse(Cat cat)
        {
            return new CatResponse(cat.Id, cat.Name, cat.Breed, cat.Gender.ToString(), cat.Age);
        }
    }
}
