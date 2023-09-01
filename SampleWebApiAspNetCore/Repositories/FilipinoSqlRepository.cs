using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Helpers;
using SampleWebApiAspNetCore.Models;
using System.Linq.Dynamic.Core;

namespace SampleWebApiAspNetCore.Repositories
{
    public class FilipinoSqlRepository : IFilipinoRepository
    {
        private readonly FilipinoDbContext _filipinoDbContext;

        public FilipinoSqlRepository(FilipinoDbContext filipinoDbContext)
        {
            _filipinoDbContext = filipinoDbContext;
        }

        public FilipinoEntity GetSingle(int id)
        {
            return _filipinoDbContext.FilipinoItems.FirstOrDefault(x => x.Id == id);
        }

        public void Add(FilipinoEntity item)
        {
            _filipinoDbContext.FilipinoItems.Add(item);
        }

        public void Delete(int id)
        {
            FilipinoEntity filipinoItem = GetSingle(id);
            _filipinoDbContext.FilipinoItems.Remove(filipinoItem);
        }

        public FilipinoEntity Update(int id, FilipinoEntity item)
        {
            _filipinoDbContext.FilipinoItems.Update(item);
            return item;
        }

        public IQueryable<FilipinoEntity> GetAll(QueryParameters queryParameters)
        {
            IQueryable<FilipinoEntity> _allItems = _filipinoDbContext.FilipinoItems.OrderBy(queryParameters.OrderBy,
              queryParameters.IsDescending());

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Calories.ToString().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.Name.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public int Count()
        {
            return _filipinoDbContext.FilipinoItems.Count();
        }

        public bool Save()
        {
            return (_filipinoDbContext.SaveChanges() >= 0);
        }

        public ICollection<FilipinoEntity> GetRandomMeal()
        {
            List<FilipinoEntity> toReturn = new List<FilipinoEntity>();

            toReturn.Add(GetRandomItem("Starter"));
            toReturn.Add(GetRandomItem("Main"));
            toReturn.Add(GetRandomItem("Dessert"));

            return toReturn;
        }

        private FilipinoEntity GetRandomItem(string type)
        {
            return _filipinoDbContext.FilipinoItems
                .Where(x => x.Type == type)
                .OrderBy(o => Guid.NewGuid())
                .FirstOrDefault();
        }
    }
}