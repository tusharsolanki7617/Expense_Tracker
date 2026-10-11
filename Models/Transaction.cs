using System;

namespace Expense_Tracker.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string CategoryBadgeClass { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
        public string IconType { get; set; }
        public string Note { get; set; }

        public string DateWithNote
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Note))
                {
                    return Date + " · " + Note;
                }
                return Date;
            }
        }

        public string FormattedAmount
        {
            get
            {
                string sym = Expense_Tracker.Services.ExpenseDataService.GetCurrencySymbol();
                if (IsIncome)
                {
                    return "+" + sym + Amount.ToString("N2");
                }
                else
                {
                    return "-" + sym + Amount.ToString("N2");
                }
            }
        }

        public string AmountCssClass
        {
            get
            {
                return IsIncome ? "amount-positive" : "amount-negative";
            }
        }
    }
}
