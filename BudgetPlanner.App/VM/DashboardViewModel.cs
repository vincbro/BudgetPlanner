using BudgetPlanner.App.Command;
using BudgetPlanner.App.Data;
using BudgetPlanner.App.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BudgetPlanner.App.VM
{
	public class DashboardViewModel : ViewModelBase
	{
		private User user = null!;

		public User User
		{
			get
			{
				return user;
			}
			set
			{
				user = value;
				user.Account.Update();
				RaisePropertyChanged();
				Transactions = [.. user.Account.Transactions.OrderByDescending(t => t.TransactionDate)];
			}
		}

		private Transaction? selectedTransaction;
		public Transaction? SelectedTransaction
		{
			get
			{
				return selectedTransaction;
			}
			set
			{
				selectedTransaction = value;
				RaisePropertyChanged();
				NotifyTypeChanged();
				Trace.WriteLine($"Selected transaction: {selectedTransaction?.Display}");
			}
		}

		public bool IsIncomeSelected
		{
			get => selectedTransaction != null && selectedTransaction.Type == TransactionType.Income;
			set
			{
				if(value && selectedTransaction != null)
				{
					selectedTransaction.Type = TransactionType.Income;
					NotifyTypeChanged();
				}
			}
		}

		public bool IsExpenseSelected
		{
			get => selectedTransaction != null && selectedTransaction.Type == TransactionType.Expense;
			set
			{
				if(value && selectedTransaction != null)
				{
					selectedTransaction.Type = TransactionType.Expense;
					NotifyTypeChanged();
				}
			}
		}

		public bool IsSavingSelected
		{
			get => selectedTransaction != null && selectedTransaction.Type == TransactionType.Saving;
			set
			{
				if(value && selectedTransaction != null)
				{
					selectedTransaction.Type = TransactionType.Saving;
					NotifyTypeChanged();
				}
			}
		}

		private void NotifyTypeChanged()
		{
			RaisePropertyChanged(nameof(IsIncomeSelected));
			RaisePropertyChanged(nameof(IsExpenseSelected));
			RaisePropertyChanged(nameof(IsSavingSelected));
			RaisePropertyChanged(nameof(SelectedTransaction));
			RaisePropertyChanged(nameof(Transactions));
		}

		private ObservableCollection<Transaction> transactions = [];
		public ObservableCollection<Transaction> Transactions
		{
			get { return transactions; }
			set
			{
				transactions = value;
				RaisePropertyChanged();
			}
		}

		public DelegateCommand RefreshCommand { get; }

		public DashboardViewModel()
		{
			RefreshCommand = new DelegateCommand((object? _) =>
			{
				if(user != null)
				{
					var id = user.Id;
					user = null!;
					var data = new DataService();
					User = data.GetUser(id);
				}
			}, (object? _) =>
			{
				return user != null;
			});
		}

	}
}
