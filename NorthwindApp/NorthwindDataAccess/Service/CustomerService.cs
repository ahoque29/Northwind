using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NorthwindCommon;
using NorthwindCommon.DbModels;
using NorthwindCommon.DTOs;
using NorthwindDataAccess.DatabaseContext;

namespace NorthwindDataAccess.Service;

public class CustomerService : IReadService<CustomerDto>, ICreateService<CustomerDto>, IEditService<CustomerDto>,
	IRemoveService<CustomerDto>, IUpsertService<CustomerDto>
{
	private readonly NorthwindContext _context;
	private readonly IMapper _mapper;
	private DbSet<Customer> Customers => _context.Set<Customer>();
	private DbSet<JobTitle> JobTitles => _context.Set<JobTitle>();
	private DbSet<Country> Countries => _context.Set<Country>();

	public CustomerService(NorthwindContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task Create(IEnumerable<CustomerDto> customerDtos, string user)
	{
		foreach (var customerDto in customerDtos) await CreateEntry(customerDto);

		await _context.SaveChangesAsync();
	}

	public async Task Edit(IEnumerable<CustomerDto> customerDtos, string user)
	{
		foreach (var customerDto in customerDtos) await EditEntry(customerDto);

		await _context.SaveChangesAsync();
	}

	public IQueryable<CustomerDto> Get()
	{
		return Customers.ProjectTo<CustomerDto>(_mapper.ConfigurationProvider);
	}

	public async Task Remove(IEnumerable<CustomerDto> customerDtos, string user)
	{
		foreach (var customerDto in customerDtos) Customers.Delete(c => c.CustomerId == customerDto.CustomerId);

		await _context.SaveChangesAsync();
	}

	public async Task Upsert(IEnumerable<CustomerDto> customerDtos, string user)
	{
		foreach (var customerDto in customerDtos)
		{
			var entryExists = await Customers.AnyAsync(c => c.CustomerId == customerDto.CustomerId);

			if (entryExists)
				await EditEntry(customerDto);
			else
				await CreateEntry(customerDto);

			await _context.SaveChangesAsync();
		}
	}

	private async Task CreateEntry(CustomerDto customerDto)
	{
		var jobTitle = await JobTitles.SingleAsync(j => j.Title == customerDto.JobTitle);
		var country = await Countries.SingleAsync(c => c.Name == customerDto.Country);

		var customer = new Customer
		{
			JobTitleId = jobTitle.JobTitleId,
			CountryId = country.CountryId,
			CompanyName = customerDto.CompanyName,
			ContactName = customerDto.ContactName,
			Address = customerDto.Address,
			City = customerDto.City,
			Region = customerDto.Region,
			PostalCode = customerDto.PostalCode,
			Phone = customerDto.Phone,
			Fax = customerDto.Fax
		};

		await Customers.AddAsync(customer);
	}

	private async Task EditEntry(CustomerDto customerDto)
	{
		var customer = await Customers.SingleAsync(c => c.CustomerId == customerDto.CustomerId);
		var jobTitle = await JobTitles.SingleAsync(j => j.Title == customerDto.JobTitle);
		var country = await Countries.SingleAsync(c => c.Name == customerDto.Country);

		customer.JobTitleId = jobTitle.JobTitleId;
		customer.CountryId = country.CountryId;
		customer.CompanyName = customerDto.CompanyName;
		customer.ContactName = customerDto.ContactName;
		customer.Address = customerDto.Address;
		customer.City = customerDto.City;
		customer.Region = customerDto.Region;
		customer.PostalCode = customerDto.PostalCode;
		customer.Phone = customerDto.Phone;
		customer.Fax = customerDto.Fax;
	}
}