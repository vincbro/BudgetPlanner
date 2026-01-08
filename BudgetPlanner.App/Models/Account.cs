using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetPlanner.App.Models
{
	public class Account
	{
		[Key]
		public string Id { get; set; } = string.Empty;
		public decimal Balance { get; set; }
		public decimal Savings { get; set; }
	}
}
