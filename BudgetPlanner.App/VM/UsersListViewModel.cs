using BudgetPlanner.App.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BudgetPlanner.App.VM
{
	public class UsersListViewModel : ViewModelBase
	{
		private bool isUserSelected = false;
		public bool IsUserSelected
		{
			get
			{
				return isUserSelected;
			}
			set
			{
				isUserSelected = value;
				RaisePropertyChanged();
			}
		}

		private User? selectedUser = null;
		public User? SelectedUser
		{
			get
			{
				return selectedUser;
			}
			set
			{
				selectedUser = value;
				RaisePropertyChanged();
				IsUserSelected = (value != null);
			}
		}

		private ObservableCollection<User> users = [];
		public ObservableCollection<User> Users
		{
			get
			{
				return users;
			}
			set
			{
				users = value;
				RaisePropertyChanged();
			}
		}
	}
}
