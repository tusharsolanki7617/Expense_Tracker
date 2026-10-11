using System;

namespace Expense_Tracker.Models
{
    public class BudgetModel
    {
        public string Category { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal SpentAmount { get; set; }
        public string IconSvg { get; set; }
        public string DefaultColor { get; set; }

        public bool IsOverBudget
        {
            get
            {
                return BudgetAmount > 0 && SpentAmount > BudgetAmount;
            }
        }

        public decimal Percentage
        {
            get
            {
                if (BudgetAmount <= 0) return 0m;
                return Math.Round((SpentAmount / BudgetAmount) * 100m, 1);
            }
        }

        public string FormattedComparison
        {
            get
            {
                string sym = Expense_Tracker.Services.ExpenseDataService.GetCurrencySymbol();
                return sym + SpentAmount.ToString("N2") + " / " + sym + BudgetAmount.ToString("N2");
            }
        }

        public string BarColor
        {
            get
            {
                // When over budget, display reddish alert bar matching Photo 1
                if (IsOverBudget)
                {
                    return "#f87171"; // soft coral red
                }
                return DefaultColor ?? "#3b82f6";
            }
        }

        public string BarWidthStyle
        {
            get
            {
                // Cap progress width between 2% and 100%
                decimal displayWidth = Math.Max(2m, Math.Min(100m, Percentage));
                return "width: " + displayWidth.ToString("0.0") + "%; background-color: " + BarColor + ";";
            }
        }
    }
}
