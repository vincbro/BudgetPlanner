using BudgetPlanner.App.Data;
using System.ComponentModel.DataAnnotations;

namespace BudgetPlanner.App.Models
{
	public class Account
	{
		[Key]
		public string Id { get; set; } = string.Empty;
		public List<Transaction> Transactions { get; set; } = [];

		public decimal TotalIncome => Transactions.Sum(t =>
		{
			if(t.IsProcessed && t.Type == TransactionType.Income && DateTime.Now.Subtract(t.TransactionDate).Days <= 30)
			{
				return t.Amount;
			}
			return 0;
		}
		);
		public decimal TotalExpenses => Transactions.Sum(t =>
		{
			if(t.IsProcessed && t.Type == TransactionType.Expense && DateTime.Now.Subtract(t.TransactionDate).Days <= 30)
			{
				return t.Amount;
			}
			return 0;
		}
		);

		public decimal Balance { get; set; }
		public decimal Savings { get; set; }

		public void Update()
		{
			var data = new DataService();
			do
			{
				foreach(var transaction in Transactions)
				{
					if(!transaction.IsProcessed && DateTime.Now >= transaction.TransactionDate)
					{
						transaction.IsProcessed = true;
						switch(transaction.Type)
						{
							case TransactionType.Income:
								Balance += transaction.Amount;
								break;
							case TransactionType.Expense:
								Balance -= transaction.Amount;
								break;
							case TransactionType.Saving:
								Balance -= transaction.Amount;
								Savings += transaction.Amount;
								break;
						}
						if(transaction.IsRecurring)
						{
							var newTransaction = new Transaction
							{
								Id = System.Guid.NewGuid().ToString(),
								Amount = transaction.Amount,
								Category = transaction.Category,
								Type = transaction.Type,
								IsRecurring = transaction.IsRecurring,
								AccountId = transaction.AccountId,
								TransactionDate = transaction.TransactionDate.AddMonths(1),
							};
							data.AddTransaction(newTransaction);
						}
						data.UpdateTransaction(transaction);
					}
				}
			}
			while(Transactions.Any(t => !t.IsProcessed && DateTime.Now >= t.TransactionDate));
			data.UpdateAccount(this);
		}
	}
}
