using NorthwindCommon.DTOs;
using Radzen;

namespace NorthwindClientService;

public interface IReadOnlyService
{
	Task<IQueryable<T>> Get<T>() where T : BaseDto, new();
}

public class ReadOnlyService : IReadOnlyService
{
	private readonly HttpClient _client;

	public ReadOnlyService(HttpClient client)
	{
		_client = client;
	}
	
	public async Task<IQueryable<T>> Get<T>() where T : BaseDto, new()
	{
		using var httpClient = new HttpClient();
		var uri = new Uri(_client.BaseAddress!, $"/api/{new T().EndpointName}");
		var request = new HttpRequestMessage(HttpMethod.Get, uri);
		var response = await _client.SendAsync(request);
		response.EnsureSuccessStatusCode();
		return await response.ReadAsync<IQueryable<T>>();
	}
}