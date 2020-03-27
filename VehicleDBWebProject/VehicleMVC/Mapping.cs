using AutoMapper;

namespace VehicleMVC
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<VehicleService.VehicleMake, Models.VehicleMakeDTO>().ReverseMap();
            CreateMap<VehicleService.VehicleModel, Models.VehicleModelDTO>().ReverseMap();
            CreateMap<VehicleService.Filtering, Models.FilteringDTO>().ReverseMap();
            CreateMap<VehicleService.Sorting, Models.SortingDTO>().ReverseMap();
            CreateMap<VehicleService.Paging, Models.PagingDTO>().ReverseMap();
        }
    }
}
