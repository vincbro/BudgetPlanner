# BudgetPlanner

**BudgetPlanner** is a desktop personal finance application built with **.NET 9** and **WPF** (Windows Presentation Foundation). It allows users to manage their personal finances by tracking income, expenses, and savings with support for recurring transactions and detailed financial summaries.

## Features

- **User Management**: Support for multiple users, allowing different individuals to maintain separate financial records.
- **Dashboard Overview**: Real-time summary of your financial health, including:
- Total Balance and Savings.
- Total Income vs. Total Expenses.
- Monthly and Yearly breakdowns.


- **Transaction Management**:
- **Add Transactions**: Record Income, Expenses, or Savings.
- **Recurring Transactions**: Set transactions to repeat Daily, Weekly, Monthly, or Yearly. The system automatically processes these when due.
- **Edit/Delete**: Modify or remove existing transactions directly from the dashboard.
- **Payout Percentage**: Unique feature to adjust income based on payout rates (e.g., set to 0.8 for 80% income during sick leave/VAB).


- **Advanced Filtering & Sorting**:
- Sort transactions by Date, Category, Type, Amount, or Processed status.
- Filter by Type (Income/Expense/Saving) or Processed status.
- Search transactions by Category.


- **Data Persistence**: Automatically creates and uses a local SQLite database (`main.db`) to store all data.

## Techstack

* **Framework**: .NET 9.0
* **UI Framework**: WPF (Windows Presentation Foundation)
* **Architecture**: MVVM (Model-View-ViewModel)
* **Database**: SQLite via Entity Framework Core
* **ORM**: Microsoft.EntityFrameworkCore (v9.0.11) with Proxies

## Project Structure

The project follows a standard MVVM structure:

* **BudgetPlanner.App**
* `Commands/`: Custom implementations of `ICommand` (e.g., `DelegateCommand`).
* `Converters/`: UI Value Converters (e.g., `BoolToIndexConverter`).
* `Data/`: Database context (`AppContext`) and data access services (`DataService`).
* `Models/`: Entity definitions (`User`, `Account`, `Transaction`).
* `VM/`: ViewModels handling logic for the views (`DashboardViewModel`, `ReportTransactionViewModel`, etc.).
* `Views/`: XAML UserControls for the UI (`DashboardView`, `CreateUserView`, etc.).

## Getting Started

### Prerequisites

* [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* Visual Studio 2022 (or a compatible IDE like JetBrains Rider or VS Code)

### Installation & Run

1. **Clone the repository**.
2. **Open the Solution**: Open `BudgetPlanner.sln` in Visual Studio.
3. **Restore Packages**: The project uses NuGet packages for Entity Framework Core. Visual Studio should restore them automatically, or you can run:
```bash
dotnet restore
```


4. **Build and Run**: Set `BudgetPlanner.App` as the startup project and run the application.
* *Note*: On the first run, the application will automatically create a `main.db` SQLite file in the output directory.



## UI & Styling

The application features a dark-themed UI with a custom color palette defined in `App.xaml`:

* **Background**: Dark Purple (`#191724`)
* **Layer**: Darker Purple (`#1f1d2e`)
* **Accent**: Light Blue (`#9ccfd8`)
* **Danger**: Red/Pink (`#eb6f92`)
* **Success**: Cyan (`#9ccfd8`)

It utilizes custom styles for buttons, text boxes, and list views to ensure a consistent modern look.

## License

This project is configured for private use. See `.gitignore` and `.gitattributes` for repository configuration.