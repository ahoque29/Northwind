using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NorthwindCommon.DbModels;
using NorthwindCommon.DTOs;
using NorthwindDataAccess.Service;

namespace NorthwindDataAccessTests;

[Collection("Database")]
public class CustomerServiceTests : InMemoryTestBase
{
	private readonly ICreateService<CustomerDto> _createService;
	private readonly IEditService<CustomerDto> _editService;
	private readonly IReadService<CustomerDto> _readService;
	private readonly IRemoveService<CustomerDto> _removeService;
	private readonly IUpsertService<CustomerDto> _upsertService;

	public CustomerServiceTests()
	{
		var service = new CustomerService(Context, Mapper);
		_readService = service;
		_createService = service;
		_editService = service;
		_removeService = service;
		_upsertService = service;

		Context.Set<JobTitle>().Add(new JobTitle
		{
			JobTitleId = 1234,
			Title = "TestJobTitle"
		});

		Context.Set<Country>().Add(new Country
		{
			CountryId = 2345,
			Name = "TestCountry"
		});

		Context.Set<Customer>().Add(new Customer
		{
			CustomerId = "ABC",
			JobTitleId = 1234,
			CountryId = 2345,
			CompanyName = "TestCompany",
			ContactName = "TestContactName",
			Address = "TestAddress",
			City = "TestCity",
			Region = "TestRegion",
			PostalCode = "TestPostalCode",
			Phone = "TestPhone",
			Fax = "TestFax"
		});

		Context.SaveChanges();
	}

	[Fact]
	public async Task CustomerTable_IsCorrectlyRead()
	{
		var customerDto = await _readService.Get().SingleAsync();

		customerDto.CustomerId.Should().Be("ABC");
		customerDto.JobTitle.Should().Be("TestJobTitle");
		customerDto.Country.Should().Be("TestCountry");

		await DisposeAsync();
	}

	[Fact]
	public async Task CreateCustomer_IncreasesCountByOne_WhenAddingACustomer()
	{
		var customerDto = new CustomerDto
		{
			CustomerId = "BCD",
			CompanyName = "TestCompanyName2",
			ContactName = "TestContactName2",
			Address = "TestAddress2",
			City = "TestCity2",
			Region = "TestRegion2",
			PostalCode = "TestPostalCode2",
			Phone = "TestPhone2",
			Fax = "TestFax2",
			JobTitle = "TestJobTitle",
			Country = "TestCountry"
		};

		await _createService.Create(customerDto, "TestUser");

		var count = await Context.Set<Customer>().CountAsync();
		count.Should().Be(2);

		var isEntryAdded = await Context.Set<Customer>().AnyAsync(c => c.CustomerId == "BCD");
		isEntryAdded.Should().BeTrue();

		await DisposeAsync();
	}

	[Fact]
	public async Task EditCustomer_KeepsCountTheSame_AndUpdatesDataProperly()
	{
		var customerDto = new CustomerDto
		{
			CustomerId = "ABC",
			CompanyName = "TestCompanyName2",
			ContactName = "TestContactName2",
			Address = "TestAddress2",
			City = "TestCity2",
			Region = "TestRegion2",
			PostalCode = "TestPostalCode2",
			Phone = "TestPhone2",
			Fax = "TestFax2",
			JobTitle = "TestJobTitle",
			Country = "TestCountry"
		};

		await _editService.Edit(customerDto, "TestUser");

		var count = await Context.Set<Customer>().CountAsync();
		count.Should().Be(1);
		
		var editedEntry = await Context.Set<Customer>().SingleAsync(c => c.CustomerId == "ABC");
		editedEntry.CompanyName.Should().Be("TestCompanyName2");

		await DisposeAsync();
	}

	[Fact]
	public async Task RemoveCustomer_DecreasesCountByOne_AndRemovesEntry()
	{
		var customerDto = new CustomerDto { CustomerId = "ABC" };

		await _removeService.Remove(customerDto);

		var count = await Context.Set<Customer>().CountAsync();
		count.Should().Be(0);
		
		var isEntryInDb = await Context.Set<Customer>().AnyAsync(c => c.CustomerId == "ABC");
		isEntryInDb.Should().BeFalse();

		await DisposeAsync();
	}

	[Fact]
	public async Task UpsertService_UpdatesData()
	{
		var customerDtos = new List<CustomerDto>
		{
			new()
			{
				CustomerId = "BCD",
				CompanyName = "TestCompanyName2",
				ContactName = "TestContactName2",
				Address = "TestAddress2",
				City = "TestCity2",
				Region = "TestRegion2",
				PostalCode = "TestPostalCode2",
				Phone = "TestPhone2",
				Fax = "TestFax2",
				JobTitle = "TestJobTitle",
				Country = "TestCountry"
			},
			new()
			{
				CustomerId = "ABC",
				CompanyName = "TestCompanyName2",
				ContactName = "TestContactName2",
				Address = "TestAddress2",
				City = "TestCity2",
				Region = "TestRegion2",
				PostalCode = "TestPostalCode2",
				Phone = "TestPhone2",
				Fax = "TestFax2",
				JobTitle = "TestJobTitle",
				Country = "TestCountry"
			}
		};

		await _upsertService.Upsert(customerDtos, "TestUser");
		
		var count = await Context.Set<Customer>().CountAsync();
		count.Should().Be(2);

		await DisposeAsync();
	}
}