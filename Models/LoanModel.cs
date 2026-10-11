using System;

namespace Expense_Tracker.Models
{
    public class LoanModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string PersonOrBank { get; set; }
        public decimal Amount { get; set; }
        public string DueDate { get; set; }
        public string Type { get; set; } // "I Borrowed" or "I Lent"

        public string FormattedAmount
        {
            get
            {
                return Expense_Tracker.Services.ExpenseDataService.GetCurrencySymbol() + Amount.ToString("N2");
            }
        }

        public string DueDateDisplay
        {
            get
            {
                return "Due " + DueDate;
            }
        }

        public string TypeBadgeClass
        {
            get
            {
                return Type != null && Type.Equals("I Lent", StringComparison.OrdinalIgnoreCase)
                    ? "badge-lent"
                    : "badge-borrowed";
            }
        }
    }
}
