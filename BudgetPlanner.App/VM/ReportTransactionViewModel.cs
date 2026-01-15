using BudgetPlanner.App.Command;
using BudgetPlanner.App.Models;

namespace BudgetPlanner.App.VM
{
	public class ReportTransactionViewModel : ViewModelBase
	{
		private decimal amount;
		public decimal Amount
		{
			get
			{
				return amount;

			}
			set
			{
				amount = value;
				RaisePropertyChanged();
			}
		}

		private string category = string.Empty;
		public string Category
		{
			get
			{
				return category;

			}
			set
			{
				category = value;
				RaisePropertyChanged();
			}
		}

		private DateTime transactionDate = DateTime.Now;
		public DateTime TransactionDate
		{
			get
			{
				return transactionDate;
			}
			set
			{
				transactionDate = value;
				RaisePropertyChanged();
			}
		}

		private bool reccuring;
		public bool Reccuring
		{
			get
			{
				return reccuring;

			}
			set
			{
				reccuring = value;
				RaisePropertyChanged();
			}
		}

		private TransactionType type = TransactionType.Income;
		public TransactionType Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
				RaisePropertyChanged();
			}
		}
		public DelegateCommand TypeChanged { get; }

		public ReportTransactionViewModel()
		{
			TypeChanged = new DelegateCommand((object? obj) =>
			{
				if(obj is TransactionType newType)
				{
					Type = newType;
				}
			});
		}
	}
}
