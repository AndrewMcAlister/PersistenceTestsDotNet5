using System;
using dfi = McAlister.Study.PersistenceTests.Definitions.Interfaces;

namespace McAlister.Study.PersistenceTests.Business
{
    public abstract class BusinessBase<T> where T : dfi.IBusinessModel
    {
        public abstract Boolean IsValid(T entity, ref String msg);
    }
}
