using Microsoft.AspNetCore.Components;
using NorthwindClientService;
using NorthwindCommon.DTOs;

namespace NorthwindClient.Pages;

public class CustomersBase : ComponentBase
{
	protected IEnumerable<CustomerDto> Customers { get; private set; }
	[Inject] protected IReadOnlyService ReadService { get; set; }

	protected override async Task OnInitializedAsync()
	{
		await base.OnInitializedAsync();

		Customers = await ReadService.Get<CustomerDto>();
	}
}