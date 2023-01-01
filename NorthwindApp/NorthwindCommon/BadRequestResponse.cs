namespace NorthwindCommon;

public record BadRequestResponse(IEnumerable<string> Errors);