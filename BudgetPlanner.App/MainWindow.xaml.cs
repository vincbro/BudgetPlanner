using System.Windows;

namespace BudgetPlanner.App;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		using var db = new Data.AppContext();
		db.Database.EnsureCreated();
		ActiveScreen.Content = new Views.UsersListView((id) =>
		{
			ActiveScreen.Content = new Views.DashboardView(id);
		}, () =>
		{
			ActiveScreen.Content = new Views.CreateUserView((id) =>
			{
				ActiveScreen.Content = new Views.DashboardView(id);
			});
		});
	}
}