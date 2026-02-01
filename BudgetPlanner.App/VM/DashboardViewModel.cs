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
			user.Account.ProcessTransactions();
			RaisePropertyChanged();
			ApplySortingAndFiltering();
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
				SaveCommand.RaiseCanExecuteChanged();
				DeleteCommand.RaiseCanExecuteChanged();
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


		public bool IsOneTimeSelected
		{
			get => selectedTransaction != null && selectedTransaction.Recurrence == RecurrenceType.OneTime;
			set
			{
				if(value && selectedTransaction != null)
				{
					selectedTransaction.Recurrence = RecurrenceType.OneTime;
					NotifyTypeChanged();
				}
			}
		}

		public bool IsDailySelected
		{
			get => selectedTransaction != null && selectedTransaction.Recurrence == RecurrenceType.Daily;
			set
			{
				if(value && selectedTransaction != null)
				{
					selectedTransaction.Recurrence = RecurrenceType.Daily;
					NotifyTypeChanged();
				}
			}
		}

		public bool IsWeeklySelected
		{
			get => selectedTransaction != null && selectedTransaction.Recurrence == RecurrenceType.Weekly;
			set
			{
				if(value && selectedTransaction != null)
				{
					selectedTransaction.Recurrence = RecurrenceType.Weekly;
					NotifyTypeChanged();
				}
			}
		}

		public bool IsMonthlySelected
		{
			get => selectedTransaction != null && selectedTransaction.Recurrence == RecurrenceType.Monthly;
			set
			{
				if(value && selectedTransaction != null)
				{
					selectedTransaction.Recurrence = RecurrenceType.Monthly;
					NotifyTypeChanged();
				}
			}
		}

		public bool IsYearlySelected
		{
			get => selectedTransaction != null && selectedTransaction.Recurrence == RecurrenceType.Yearly;
			set
			{
				if(value && selectedTransaction != null)
				{
					selectedTransaction.Recurrence = RecurrenceType.Yearly;
					NotifyTypeChanged();
				}
			}
		}


		private void NotifyTypeChanged()
		{
			RaisePropertyChanged(nameof(IsIncomeSelected));
			RaisePropertyChanged(nameof(IsExpenseSelected));
			RaisePropertyChanged(nameof(IsSavingSelected));
			RaisePropertyChanged(nameof(IsOneTimeSelected));
			RaisePropertyChanged(nameof(IsDailySelected));
			RaisePropertyChanged(nameof(IsWeeklySelected));
			RaisePropertyChanged(nameof(IsMonthlySelected));
			RaisePropertyChanged(nameof(IsYearlySelected));
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

	private string currentSortBy = "Date";
	public string CurrentSortBy
	{
		get { return currentSortBy; }
		set
		{
			currentSortBy = value;
			RaisePropertyChanged();
			ApplySortingAndFiltering();
		}
	}

	private bool sortAscending = false;
	public bool SortAscending
	{
		get { return sortAscending; }
		set
		{
			sortAscending = value;
			RaisePropertyChanged();
			ApplySortingAndFiltering();
		}
	}

	private string? filterType = null;
	public string? FilterType
	{
		get { return filterType; }
		set
		{
			filterType = value;
			RaisePropertyChanged();
			ApplySortingAndFiltering();
		}
	}

	private string? filterProcessed = null;
	public string? FilterProcessed
	{
		get { return filterProcessed; }
		set
		{
			filterProcessed = value;
			RaisePropertyChanged();
			ApplySortingAndFiltering();
		}
	}

	private string categorySearch = string.Empty;
	public string CategorySearch
	{
		get { return categorySearch; }
		set
		{
			categorySearch = value;
			RaisePropertyChanged();
			ApplySortingAndFiltering();
		}
	}

	public List<string> SortOptions { get; } = ["Date", "Category", "Type", "Amount", "Processed"];
	public List<string> SortDirections { get; } = ["Ascending", "Descending"];
	public List<string> TypeFilterOptions { get; } = ["All", "Income", "Expense", "Saving"];
	public List<string> ProcessedFilterOptions { get; } = ["All", "Processed", "Not Processed"];


	public DelegateCommand RefreshCommand { get; }
	public DelegateCommand SaveCommand { get; }
	public DelegateCommand DeleteCommand { get; }
	public DelegateCommand DeleteAllCommand { get; }

	public DashboardViewModel()
	{
		FilterType = "All";
		FilterProcessed = "All";
		
		RefreshCommand = new DelegateCommand((object? _) =>
		{
			if(user != null)
			{
				var id = user.Id;
				user = null!;
				var data = new DataService();
				User = data.GetUser(id);
				NotifyTypeChanged();
			}
		}, (object? _) =>
		{
			return user != null;
		});
			SaveCommand = new DelegateCommand((object? _) =>
			{
				if(SelectedTransaction != null)
				{
					Trace.WriteLine($"Saving transaction: {SelectedTransaction.Display}");
					var data = new DataService();
					var transaction = data.GetTransaction(SelectedTransaction.Id);
					transaction.Type = SelectedTransaction.Type;
					transaction.Amount = SelectedTransaction.Amount;
					transaction.Category = SelectedTransaction.Category;
					transaction.TransactionDate = SelectedTransaction.TransactionDate;
					transaction.Recurrence = SelectedTransaction.Recurrence;
					transaction.PayoutPercentage = SelectedTransaction.PayoutPercentage;
					data.UpdateTransaction(transaction);
					SelectedTransaction = null;
					RefreshCommand.Execute(null);
				}
			}, (object? _) =>
			{
				return SelectedTransaction != null;
			});

			DeleteCommand = new DelegateCommand((object? _) =>
			{

				if(SelectedTransaction != null)
				{
					var data = new DataService();
					data.DeleteTransaction(SelectedTransaction.Id);
					SelectedTransaction = null;
					RefreshCommand.Execute(null);
				}

			}, (object? _) =>
			{
				return SelectedTransaction != null;
			});

		DeleteAllCommand = new DelegateCommand((object? _) =>
		{
			if(SelectedTransaction != null)
			{
				var data = new DataService();
				data.DeleteAllLinkedTransaction(SelectedTransaction.Id);
				SelectedTransaction = null;
				RefreshCommand.Execute(null);
			}
		});
	}

	private void ApplySortingAndFiltering()
	{
		if(user == null || user.Account == null) return;

		IEnumerable<Transaction> filtered = user.Account.Transactions;

		// Apply type filter
		if(FilterType != null && FilterType != "All")
		{
			filtered = FilterType switch
			{
				"Income" => filtered.Where(t => t.Type == TransactionType.Income),
				"Expense" => filtered.Where(t => t.Type == TransactionType.Expense),
				"Saving" => filtered.Where(t => t.Type == TransactionType.Saving),
				_ => filtered
			};
		}

		// Apply processed filter
		if(FilterProcessed != null && FilterProcessed != "All")
		{
			filtered = FilterProcessed switch
			{
				"Processed" => filtered.Where(t => t.IsProcessed),
				"Not Processed" => filtered.Where(t => !t.IsProcessed),
				_ => filtered
			};
		}

		// Apply category search
		if(!string.IsNullOrWhiteSpace(CategorySearch))
		{
			filtered = filtered.Where(t => 
				t.Category.Contains(CategorySearch, StringComparison.OrdinalIgnoreCase));
		}

		// Apply sorting
		bool ascending = SortAscending;
		IEnumerable<Transaction> sorted = CurrentSortBy switch
		{
			"Date" => ascending 
				? filtered.OrderBy(t => t.TransactionDate) 
				: filtered.OrderByDescending(t => t.TransactionDate),
			"Category" => ascending 
				? filtered.OrderBy(t => t.Category).ThenByDescending(t => t.TransactionDate)
				: filtered.OrderByDescending(t => t.Category).ThenByDescending(t => t.TransactionDate),
			"Type" => ascending 
				? filtered.OrderBy(t => t.Type).ThenByDescending(t => t.TransactionDate)
				: filtered.OrderByDescending(t => t.Type).ThenByDescending(t => t.TransactionDate),
			"Amount" => ascending 
				? filtered.OrderBy(t => t.Amount).ThenByDescending(t => t.TransactionDate)
				: filtered.OrderByDescending(t => t.Amount).ThenByDescending(t => t.TransactionDate),
			"Processed" => ascending 
				? filtered.OrderBy(t => t.IsProcessed).ThenByDescending(t => t.TransactionDate)
				: filtered.OrderByDescending(t => t.IsProcessed).ThenByDescending(t => t.TransactionDate),
			_ => filtered.OrderByDescending(t => t.TransactionDate)
		};

		Transactions = [.. sorted];
	}

}
}
