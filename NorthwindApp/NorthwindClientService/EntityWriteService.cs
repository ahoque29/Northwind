using System.Net;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using NorthwindCommon;
using NorthwindCommon.DTOs;
using NorthwindCommon.Exceptions;

namespace NorthwindClientService;

public abstract class EntityWriteService
{
	protected readonly HttpClient Client;

	protected EntityWriteService(HttpClient client)
	{
		Client = client;
	}

	protected virtual async Task Execute<T>(string instruction, IEnumerable<T> entities) where T : BaseDto, new()
	{
		var response = await Client.PostAsJsonAsync($"api/{new T().EndpointName}/{instruction}", entities);

		if (response.StatusCode == HttpStatusCode.BadRequest)
		{
			var content = await response.Deserialize<BadRequestResponse>();
			throw new BadRequestException(content);
		}

		response.EnsureSuccessStatusCode();
	}
}

public class EntityCreateService : EntityWriteService, IEntityCreateService
{
	public EntityCreateService(HttpClient client) : base(client)
	{
	}

	public async Task Create<T>(IEnumerable<T> entities) where T : BaseDto, new()
	{
		await Execute("Create", entities);
	}
}

public class EntityEditService : EntityWriteService, IEntityEditService
{
	public EntityEditService(HttpClient client) : base(client)
	{
	}

	public async Task Edit<T>(IEnumerable<T> entities) where T : BaseDto, new()
	{
		await Execute("Edit", entities);
	}
}

public class EntityUpsertService : EntityWriteService, IEntityUpsertService
{
	public EntityUpsertService(HttpClient client) : base(client)
	{
	}

	public async Task UpsertService<T>(IEnumerable<T> entities) where T : BaseDto, new()
	{
		await Execute("Upsert", entities);
	}
}

public class EntityRemoveService : EntityWriteService, IEntityRemoveService
{
	public EntityRemoveService(HttpClient client) : base(client)
	{
	}

	public async Task Remove<T>(IEnumerable<T> entities) where T : BaseDto, new()
	{
		await Execute("Remove", entities);
	}

	protected override async Task Execute<T>(string instruction, IEnumerable<T> entities)
	{
		var request = new HttpRequestMessage(HttpMethod.Delete, $"api/{new T().EndpointName}/{instruction}");
		request.Content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json");
		var response = await Client.SendAsync(request);

		response.EnsureSuccessStatusCode();
	}
}

public class EntityRemoveAllService : EntityWriteService, IEntityRemoveAllService
{
	public EntityRemoveAllService(HttpClient client) : base(client)
	{
	}

	public async Task RemoveAll<T>() where T : BaseDto, new()
	{
		await Execute<T>("RemoveAll");
	}

	protected override async Task Execute<T>(string instruction, IEnumerable<T> entities = default)
	{
		var response = await Client.DeleteAsync($"api/{new T().EndpointName}/{instruction}");

		if (response.StatusCode == HttpStatusCode.BadRequest)
		{
			var content = await response.Deserialize<BadRequestResponse>();
			throw new BadRequestException(content);
		}

		response.EnsureSuccessStatusCode();
	}
}