using AutoMapper;
using NorthwindCommon.DbModels;
using NorthwindCommon.DTOs;

namespace NorthwindCommon;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		AllowNullCollections = true;

		CreateMap<Customer, CustomerDto>()
			.ForMember(dest => dest.JobTitle, opts => opts.MapFrom(src => src.JobTitle.Title))
			.ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Country.Name));
	}
}