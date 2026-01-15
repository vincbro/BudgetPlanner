using BudgetPlanner.App.Data;
using BudgetPlanner.App.Models;
using BudgetPlanner.App.VM;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace BudgetPlanner.App.Views
{
	public partial class ReportTransactionView : UserControl
	{
		private readonly Action createReport;
		private ReportTransactionViewModel vm = new();
		private readonly string userId;
		public ReportTransactionView(string id, Action createReport)
		{
			InitializeComponent();
			this.createReport = createReport;
			vm.Type = TransactionType.Income;
			DataContext = vm;
			userId = id;
		}

		private void CreateReportClicked(object sender, RoutedEventArgs e)
		{
			Trace.WriteLine($"Report transaction of {vm.Amount} kr as {vm.Type}. Reccuring [{vm.Reccuring}], Time [{vm.TransactionDate}]");
			var data = new DataService();
			var user = data.GetUser(userId);
			var transaction = new Transaction()
			{
				Id = System.Guid.NewGuid().ToString(),
				Amount = vm.Amount,
				Category = vm.Category,
				TransactionDate = vm.TransactionDate,
				IsRecurring = vm.Reccuring,
				Type = vm.Type,
				AccountId = user.Account.Id,
			};
			data.AddTransaction(transaction);
			createReport.Invoke();
		}

		private void BackClicked(object sender, RoutedEventArgs e)
		{
			createReport.Invoke();
		}
	}
}
