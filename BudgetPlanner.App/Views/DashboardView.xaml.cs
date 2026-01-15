using BudgetPlanner.App.Data;
using BudgetPlanner.App.VM;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace BudgetPlanner.App.Views
{
	public partial class DashboardView : UserControl
	{
		public DashboardViewModel vm = new();
		private readonly DataService data = new();
		private readonly Action reportTransaction;
		public DashboardView(string id, Action reportTransaction)
		{
			InitializeComponent();
			var user = data.GetUser(id);
			user.Account.Update();
			vm.User = data.GetUser(id);
			Trace.WriteLine($"Dashboard for user {vm.User.FirstName} {vm.User.LastName} with {vm.User.Account.Balance} kr and {vm.User.Account.Transactions.Count} transactions");
			DataContext = vm;
			this.reportTransaction = reportTransaction;
		}

		private void ReportTransactionClicked(object sender, RoutedEventArgs e)
		{
			reportTransaction.Invoke();
		}
	}
}
