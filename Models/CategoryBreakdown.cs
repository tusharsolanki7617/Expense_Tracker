using System;

namespace Expense_Tracker.Models
{
    public class CategoryBreakdown
    {
        public string CategoryName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Percentage { get; set; }
        public string BarColor { get; set; }
        public string IconSvg { get; set; }
        public int TransactionCount { get; set; }

        public string FormattedAmount
        {
            get
            {
                return Expense_Tracker.Services.ExpenseDataService.GetCurrencySymbol() + TotalAmount.ToString("N2");
            }
        }

        public string FormattedPercentage
        {
            get
            {
                return "(" + Percentage.ToString("0.0") + "%)";
            }
        }

        public string BarWidthStyle
        {
            get
            {
                // Cap between 2% and 100% for aesthetic bar display
                decimal width = Math.Max(2m, Math.Min(100m, Percentage));
                return "width: " + width.ToString("0.0") + "%; background-color: " + BarColor + ";";
            }
        }
    }
}
