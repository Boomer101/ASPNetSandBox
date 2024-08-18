namespace CatsLibrary
{
    public interface ICatRepository
    {
        Task<Cat> GetCatAsync(int id);
        Task<IEnumerable<Cat>> GetCatsAsync();
    }

    public class CatRepository : ICatRepository
    {
        private readonly List<Cat> _cats;

        public CatRepository()
        {
            _cats = new List<Cat>
            {
                new Cat { Name = "Fluffy", Age = 2, Gender = Gender.Female, Coloring = "Calico" },
                new Cat { Name = "Whiskers", Age = 5, Gender = Gender.Female, Coloring = "Al black" },
                new Cat { Name = "Mr. Bigglesworth", Age = 5, Gender = Gender.Male, Coloring ="Gray stripes" }
            };

            for (int i = 0; i < _cats.Count; i++)
            {
                _cats[i].Id = i;
            }
        }

        public async Task<Cat> GetCatAsync(int id)
        {
            if (id < 0 || id >= _cats.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid cat ID");
            }
            
            return await Task.FromResult(_cats[id]);
        }

        public async Task<IEnumerable<Cat>> GetCatsAsync()
        {
            return await Task.FromResult(_cats);
        }
    }
}
