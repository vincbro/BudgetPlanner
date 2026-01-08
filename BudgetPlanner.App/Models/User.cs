using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetPlanner.App.Models
{
	public class User
	{
		[Key]
		public string Id { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Name => $"{FirstName} {LastName}";
		//public string AccountId { get; set; } = null!;
		public virtual Account Account { get; set; } = null!;
	}
}
