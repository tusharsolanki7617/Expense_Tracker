using System;

namespace Expense_Tracker.Models
{
    public class BillModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; } // "Overdue" or "Upcoming"

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

        public string StatusBadgeClass
        {
            get
            {
                return Status != null && Status.Equals("Overdue", StringComparison.OrdinalIgnoreCase)
                    ? "badge-overdue"
                    : "badge-upcoming";
            }
        }
    }
}
