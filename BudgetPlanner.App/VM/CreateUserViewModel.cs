namespace BudgetPlanner.App.VM
{
	public class CreateUserViewModel : ViewModelBase
	{
		private string firstName = string.Empty;
		public string FirstName
		{
			get
			{
				return firstName;

			}
			set
			{
				firstName = value;
				RaisePropertyChanged();
			}
		}

		private string lastName = string.Empty;
		public string LastName
		{
			get
			{
				return lastName;

			}
			set
			{
				lastName = value;
				RaisePropertyChanged();
			}
		}
	}
}
