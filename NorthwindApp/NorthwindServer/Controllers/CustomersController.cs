using Microsoft.AspNetCore.Mvc;
using NorthwindCommon.DTOs;
using NorthwindDataAccess.Service;

namespace NorthwindServer.Controllers;

[Route("api/[controller]")]
public class CustomersController : Controller
{
	private readonly ICreateService<CustomerDto> _createService;
	private readonly IEditService<CustomerDto> _editService;
	private readonly IReadService<CustomerDto> _readService;
	private readonly IRemoveService<CustomerDto> _removeService;
	private readonly IUpsertService<CustomerDto> _upsertService;

	public CustomersController(IReadService<CustomerDto> readService, ICreateService<CustomerDto> createService,
		IEditService<CustomerDto> editService, IRemoveService<CustomerDto> removeService,
		IUpsertService<CustomerDto> upsertService)
	{
		_readService = readService;
		_createService = createService;
		_editService = editService;
		_removeService = removeService;
		_upsertService = upsertService;
	}

	[HttpGet]
	public IQueryable<CustomerDto> Get()
	{
		return _readService.Get();
	}

	[HttpPost("Create")]
	public async Task<IActionResult> Create([FromBody] IReadOnlyCollection<CustomerDto> customerDtos)
	{
		if (customerDtos is null) return BadRequest();

		await _createService.Create(customerDtos, HttpContext.User.Identity.Name);

		return Ok();
	}

	[HttpPost("Edit")]
	public async Task<IActionResult> Edit([FromBody] IReadOnlyCollection<CustomerDto> customerDtos)
	{
		if (customerDtos is null) return BadRequest();

		await _editService.Edit(customerDtos, HttpContext.User.Identity.Name);

		return Ok();
	}

	[HttpDelete("Delete")]
	public async Task<IActionResult> Remove([FromBody] IReadOnlyCollection<CustomerDto> customerDtos)
	{
		if (customerDtos is null) return BadRequest();

		await _removeService.Remove(customerDtos);

		return Ok();
	}

	[HttpPost("Upsert")]
	public async Task<IActionResult> Upsert([FromBody] IReadOnlyCollection<CustomerDto> customerDtos)
	{
		if (customerDtos is null) return BadRequest();

		await _upsertService.Upsert(customerDtos, HttpContext.User.Identity.Name);

		return Ok();
	}
}