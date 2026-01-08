using BudgetPlanner.App.Data;
using BudgetPlanner.App.Models;
using BudgetPlanner.App.VM;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace BudgetPlanner.App.Views
{
	public partial class CreateUserView : UserControl
	{
		public CreateUserViewModel vm = new();

		private readonly DataService data = new();
		private readonly Action<string> newUserCreatedAction;
		public CreateUserView(Action<string> newUserCreatedAction)
		{
			InitializeComponent();
			this.newUserCreatedAction = newUserCreatedAction;
			DataContext = vm;
		}

		private void CreateUserClicked(object sender, RoutedEventArgs e)
		{
			Trace.WriteLine($"New user {vm.FirstName} {vm.LastName}");
			var user = new User()
			{
				Id = Guid.NewGuid().ToString(),
				FirstName = vm.FirstName,
				LastName = vm.LastName,
				Account = new Account
				{
					Id = Guid.NewGuid().ToString(),
				}
			};
			data.AddUser(user);
			newUserCreatedAction.Invoke(user.Id);
		}
	}
}
