using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class Currency : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserInfo();
                BindCurrencies();
            }
        }

        private void LoadUserInfo()
        {
            string userName = Session["UserName"] as string;
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "tushar";
            }

            litUserName.Text = userName;
            litUserInitial.Text = (!string.IsNullOrEmpty(userName) ? userName[0].ToString().ToUpper() : "T");
        }

        private void BindCurrencies()
        {
            var currencies = ExpenseDataService.GetAllCurrencies();
            rptCurrencies.DataSource = currencies;
            rptCurrencies.DataBind();

            var selected = ExpenseDataService.GetSelectedCurrencyModel();
            if (selected != null)
            {
                litHeroSymbol.Text = selected.Symbol;
                litHeroTitle.Text = selected.DisplayTitle;
            }
        }

        protected void rptCurrencies_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelectCurrency")
            {
                string code = e.CommandArgument?.ToString();
                if (!string.IsNullOrEmpty(code))
                {
                    ExpenseDataService.SetSelectedCurrencyCode(code);
                    Session["Currency"] = code;
                    BindCurrencies();
                }
            }
        }
    }
}
