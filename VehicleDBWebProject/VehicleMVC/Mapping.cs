using AutoMapper;

namespace VehicleMVC
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<VehicleService.Models.VehicleMake, Models.VehicleMakeDTO>().ReverseMap();
            CreateMap<VehicleService.Models.VehicleModel, Models.VehicleModelDTO>().ReverseMap();
            CreateMap<VehicleService.Common.Filtering, Models.FilteringDTO>().ReverseMap();
            CreateMap<VehicleService.Common.Sorting, Models.SortingDTO>().ReverseMap();
            CreateMap<VehicleService.Common.Paging, Models.PagingDTO>().ReverseMap();
        }
    }
}
