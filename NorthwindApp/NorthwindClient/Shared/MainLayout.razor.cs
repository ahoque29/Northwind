using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace NorthwindClient.Shared;

public class MainLayoutComponent : LayoutComponentBase
{
	protected new RadzenBody Body;
	protected RadzenSidebar Sidebar;

	protected async Task SideBarToggleClick(dynamic args)
	{
		await InvokeAsync(() => { Sidebar.Toggle(); });
		await InvokeAsync(() => { Body.Toggle(); });
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);
	}
}