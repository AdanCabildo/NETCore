using SampleWebApiAspNetCore.Entities;

namespace SampleWebApiAspNetCore.Repositories
{
    public interface IFilipinoRepository
    {
        FilipinoEntity GetSingle(int id);
        void Add(FilipinoEntity item);
        void Delete(int id);
        FilipinoEntity Update(int id, FilipinoEntity item);
        int Count();
        bool Save();
    }
}