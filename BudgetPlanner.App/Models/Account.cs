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
			if(t.IsProcessed && t.Type == TransactionType.Income)
			{
				return t.Amount;
			}
			return 0;
		}
		);
		public decimal TotalExpenses => Transactions.Sum(t =>
		{
			if(t.IsProcessed && t.Type == TransactionType.Expense)
			{
				return t.Amount;
			}
			return 0;
		}
		);

		public decimal MonthIncome => Transactions.Sum(t =>
		{
			if(t.Type == TransactionType.Income && DateTime.Now.Month == t.TransactionDate.Month)
			{
				return t.Amount;
			}
			return 0;
		}
		);
		public decimal MonthExpenses => Transactions.Sum(t =>
		{
			if(t.Type == TransactionType.Expense && DateTime.Now.Month == t.TransactionDate.Month)
			{
				return t.Amount;
			}
			return 0;
		}
		);


		public decimal YearIncome => Transactions.Sum(t =>
		{
			if(t.BaseTransactionId == null && t.Type == TransactionType.Income)
			{
				switch(t.Recurrence)
				{
					case RecurrenceType.Daily:
						return t.Amount * 365;
					case RecurrenceType.Weekly:
						return t.Amount * 52;
					case RecurrenceType.Monthly:
						return t.Amount * 12;
					case RecurrenceType.Yearly:
						return t.Amount;
					default:
						if(t.TransactionDate.Year == DateTime.Now.Year)
						{
							return t.Amount;
						}
						return 0;
				}
			}
			return 0;
		});

		public decimal YearExpenses => Transactions.Sum(t =>
		{
			if(t.BaseTransactionId == null && t.Type == TransactionType.Expense)
			{
				switch(t.Recurrence)
				{
					case RecurrenceType.Daily:
						return t.Amount * 365;
					case RecurrenceType.Weekly:
						return t.Amount * 52;
					case RecurrenceType.Monthly:
						return t.Amount * 12;
					case RecurrenceType.Yearly:
						return t.Amount;
					default:
						if(t.TransactionDate.Year == DateTime.Now.Year)
						{
							return t.Amount;
						}
						return 0;
				}
			}
			return 0;
		});

		public decimal Savings => Transactions.Sum(t =>
		{
			if(t.IsProcessed && t.Type == TransactionType.Saving)
			{
				return t.Amount;
			}
			return 0;
		}
		);

		public decimal Balance => TotalIncome - TotalExpenses - Savings;

		public void ProcessTransactions()
		{
			var data = new DataService();
			var queue = new Queue<Transaction>(Transactions.Where(t => !t.IsProcessed && DateTime.Now >= t.TransactionDate));

			while(queue.Count > 0)
			{
				var transaction = queue.Dequeue();

				// Mark as processed
				transaction.IsProcessed = true;
				data.UpdateTransaction(transaction);

				if(transaction.Recurrence != RecurrenceType.OneTime)
				{
					var nextDate = AddTime(transaction.TransactionDate, transaction.Recurrence);

					var newTransaction = new Transaction
					{
						Id = System.Guid.NewGuid().ToString(),
						Amount = transaction.Amount,
						Category = transaction.Category,
						Type = transaction.Type,
						Recurrence = transaction.Recurrence,
						AccountId = transaction.AccountId,
						TransactionDate = AddTime(transaction.TransactionDate, transaction.Recurrence),
						BaseTransactionId = transaction.BaseTransactionId ?? transaction.Id,
					};

					data.AddTransaction(newTransaction);

					if(DateTime.Now >= newTransaction.TransactionDate)
					{
						queue.Enqueue(newTransaction);
					}
				}
			}
			data.UpdateAccount(this);
		}

		private DateTime AddTime(DateTime time, RecurrenceType recurrence)
		{
			switch(recurrence)
			{
				case RecurrenceType.Daily:
					return time.AddDays(1);
				case RecurrenceType.Weekly:
					return time.AddDays(7);
				case RecurrenceType.Monthly:
					return time.AddMonths(1);
				case RecurrenceType.Yearly:
					return time.AddYears(1);
				default:
					return time;
			}

		}
	}
}
