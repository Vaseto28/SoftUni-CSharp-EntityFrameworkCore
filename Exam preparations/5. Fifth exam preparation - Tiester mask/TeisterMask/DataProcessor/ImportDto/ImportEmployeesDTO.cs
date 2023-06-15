using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TeisterMask.Common;

namespace TeisterMask.DataProcessor.ImportDto;

public class ImportEmployeesDTO
{
	[JsonProperty("Username")]
	[Required]
	[MinLength(ValidationConstraints.EmployeeUsernameMinLength)]
	[MaxLength(ValidationConstraints.EmployeeUsernameMaxLength)]
	[RegularExpression(ValidationConstraints.EmployeeUsernameRegExPattern)]
	public string Username { get; set; } = null!;

	[JsonProperty("Email")]
	[Required]
	[EmailAddress]
	public string Email { get; set; } = null!;

	[JsonProperty("Phone")]
	[Required]
	[RegularExpression(ValidationConstraints.EmployeePhoneRegExPattern)]
	public string PhoneNumber { get; set; } = null!;

	[JsonProperty("Tasks")]
	public int[]? Tasks { get; set; }
}

