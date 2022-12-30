namespace NorthwindCommon.Exceptions;

public class BadRequestException : Exception
{
	public BadRequestResponse BadRequestResponse { get; set; }

	public BadRequestException(BadRequestResponse badRequestResponse)
	{
		BadRequestResponse = badRequestResponse;
	}
}