using AutoMapper;
using dfe = McAlister.Study.PersistenceTests.Definitions.Entities;
using dfm = McAlister.Study.PersistenceTests.Definitions.Models;

namespace McAlister.Study.PersistenceTests.MapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<dfe.Order, dfm.Order>();
            CreateMap<dfm.Order, dfe.Order>();
        }
    }
}
