using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BudgetPlanner.App.Models
{

	public enum TransactionType
	{
		Income,
		Expense,
		Saving
	};

	public enum RecurrenceType
	{
		OneTime,
		Daily,
		Weekly,
		Monthly,
		Yearly,
	};


	public class Transaction : INotifyPropertyChanged
	{
		[Key]
		public string Id { get; set; } = string.Empty;
		public decimal Amount { get; set; }
		public DateTime TransactionDate { get; set; } = DateTime.Now;
		public TransactionType Type { get; set; }
		public string Category { get; set; } = string.Empty;
		public bool IsProcessed { get; set; } = false;
		public RecurrenceType Recurrence { get; set; }
		public string? BaseTransactionId { get; set; } = null;
		public string AccountId { get; set; } = string.Empty;

		public string Display => $"{TransactionDate.ToShortDateString()} - {Type} - {Category}: {string.Format("{0:N0}", Amount)} kr - Processed: [{IsProcessed}]";

		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
