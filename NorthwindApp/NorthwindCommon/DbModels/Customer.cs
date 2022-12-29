namespace NorthwindCommon.DbModels;

public class Customer
{
	public string CustomerId { get; set; }
	public int? JobTitleId { get; set; }
	public int? CountryId { get; set; }
	public string CompanyName { get; set; }
	public string? ContactName { get; set; }
	public string? Address { get; set; }
	public string? City { get; set; }
	public string? Region { get; set; }
	public string? PostalCode { get; set; }
	public string? Phone { get; set; }
	public string? Fax { get; set; }

	public JobTitle JobTitle { get; set; }
	public Country Country { get; set; }
}