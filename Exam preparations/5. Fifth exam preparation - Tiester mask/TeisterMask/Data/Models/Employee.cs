using System.ComponentModel.DataAnnotations;
using TeisterMask.Common;

namespace TeisterMask.Data.Models;

public class Employee
{
	public Employee()
	{
		this.EmployeesTasks = new HashSet<EmployeeTask>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(ValidationConstraints.EmployeeUsernameMaxLength)]
	public string Username { get; set; } = null!;

	[Required]
	public string Email { get; set; } = null!;

	[Required]
	public string Phone { get; set; } = null!;

	public virtual ICollection<EmployeeTask> EmployeesTasks { get; set; }
}

