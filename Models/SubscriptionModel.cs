using System;

namespace Expense_Tracker.Models
{
    public class SubscriptionModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string NextDueDate { get; set; }
        public string StatusBadge { get; set; } = "Upcoming";

        public string FormattedAmount
        {
            get
            {
                return Expense_Tracker.Services.ExpenseDataService.GetCurrencySymbol() + Amount.ToString("N2") + "/mo";
            }
        }

        public string DueDateDisplay
        {
            get
            {
                return "Due: " + NextDueDate;
            }
        }
    }
}
