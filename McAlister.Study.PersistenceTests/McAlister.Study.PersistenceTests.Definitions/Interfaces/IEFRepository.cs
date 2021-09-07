using dfi = McAlister.Study.PersistenceTests.Definitions.Interfaces;

namespace McAlister.Study.PersistenceTests.Definitions.Interfaces
{
    public interface IEFRepository<T1> where T1 : dfi.IBusinessModel
    {
        //#region Generic
        //String ConnStr { get;}
        //DbContext Context { get; } // I want the context to be available so a business class can control a transaction
        //T Insert<T>(T model) where T : class;
        //T Update<T>(T model) where T : class;
        //void Delete<T>(T model) where T : class;
        //void Delete<T>(Expression<Func<T, bool>> predicate) where T : class;
        //void DeleteRange<T>(IEnumerable<T> lst) where T : class;
        //T GetById<T>(object[] ids) where T : class;
        //T Get<T>(Expression<Func<T, bool>> predicate) where T : class;
        //ICollection<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class;
        //ICollection<T> GetList<T, TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy) where T : class;
        //ICollection<T> GetList<T, TKey>(Expression<Func<T, TKey>> orderBy) where T : class;
        //ICollection<T> GetList<T>() where T : class;
        //OperationStatus ExecuteCommand(string cmdText, params object[] parameters);
        //int SubmitChanges();
        //Task<int> SubmitChangesAsync();
        //#endregion

    }
}
