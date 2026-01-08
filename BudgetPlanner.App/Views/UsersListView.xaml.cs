using BudgetPlanner.App.Data;
using BudgetPlanner.App.Models;
using BudgetPlanner.App.VM;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BudgetPlanner.App.Views
{
	public partial class UsersListView : UserControl
	{
		public DataService data = new();
		public UsersListViewModel vm = new();
		private readonly Action<string> userSelectedAction;
		private readonly Action newUserAction;
		public UsersListView(Action<string> userSelectedAction, Action newUserAction)
		{
			InitializeComponent();
			// Fix: Find cleaner conversion
			var list = new ObservableCollection<User>();
			foreach(var user in data.GetAllUsers())
			{
				list.Add(user);
			}
			vm.Users = list;
			DataContext = vm;
			this.userSelectedAction = userSelectedAction;
			this.newUserAction = newUserAction;
		}

		private void CreateUserClicked(object sender, System.Windows.RoutedEventArgs e)
		{
			newUserAction.Invoke();
		}

		private void SelectUser(object sender, System.Windows.RoutedEventArgs e)
		{
			if(vm.SelectedUser != null)
			{
				userSelectedAction(vm.SelectedUser.Id);
			}
		}
	}
}
