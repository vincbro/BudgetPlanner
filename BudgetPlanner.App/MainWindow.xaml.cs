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
			ActiveScreen.Content = NewDashbordView(id);
		}, () =>
		{
			ActiveScreen.Content = new Views.CreateUserView((id) =>
			{
				ActiveScreen.Content = NewDashbordView(id);
			});
		});
	}

	private Views.DashboardView NewDashbordView(string id)
	{
		return new Views.DashboardView(id, 
			() => ActiveScreen.Content = new Views.ReportTransactionView(id, () => ActiveScreen.Content = NewDashbordView(id)));
	}
}