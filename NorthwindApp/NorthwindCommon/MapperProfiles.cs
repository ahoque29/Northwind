using AutoMapper;
using NorthwindCommon.DbModels;
using NorthwindCommon.DTOs;

namespace NorthwindCommon;

public class MapperProfiles : Profile
{
	public MapperProfiles()
	{
		AllowNullCollections = true;

		CreateMap<Customer, CustomerDto>()
			.ForMember(dest => dest.JobTitle, opts => opts.MapFrom(src => src.JobTitle.Title))
			.ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Country.Name));
	}
}