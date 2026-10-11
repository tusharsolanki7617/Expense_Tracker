using System;

namespace Expense_Tracker.Models
{
    public class CurrencyModel
    {
        public string Code { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string FlagEmoji { get; set; }
        public bool IsSelected { get; set; }

        public string DisplayTitle
        {
            get
            {
                return Name + " — " + Code;
            }
        }

        public string CardCssClass
        {
            get
            {
                return IsSelected ? "currency-card active" : "currency-card";
            }
        }
    }
}
