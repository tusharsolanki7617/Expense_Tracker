using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserProfile();
                LoadCategoryBreakdowns();
                txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void LoadUserProfile()
        {
            string userName = Session["UserName"] as string;
            if (string.IsNullOrWhiteSpace(userName))
            {
                string email = Session["UserEmail"] as string;
                if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                {
                    userName = email.Split('@')[0];
                }
                else
                {
                    userName = "tushar";
                }
            }

            litUserName.Text = Server.HtmlEncode(userName);
            string initial = "T";
            string trimmed = userName.Trim();
            if (!string.IsNullOrEmpty(trimmed))
            {
                initial = char.ToUpper(trimmed[0]).ToString();
            }
            litUserInitial.Text = Server.HtmlEncode(initial);
        }

        private void LoadCategoryBreakdowns()
        {
            var breakdowns = ExpenseDataService.GetCategoryBreakdowns();

            if (breakdowns != null && breakdowns.Count > 0)
            {
                rptCategories.DataSource = breakdowns;
                rptCategories.DataBind();
                rptCategories.Visible = true;
                pnlEmpty.Visible = false;
            }
            else
            {
                rptCategories.Visible = false;
                pnlEmpty.Visible = true;
            }
        }

        protected void btnSaveExpense_Click(object sender, EventArgs e)
        {
            string title = txtExpenseTitle.Text.Trim();
            string category = ddlExpenseCategory.SelectedValue;
            string date = txtExpenseDate.Text.Trim();
            decimal amount;

            if (string.IsNullOrEmpty(title))
            {
                title = "Expense";
            }

            if (decimal.TryParse(txtExpenseAmount.Text.Trim(), out amount) && amount > 0)
            {
                ExpenseDataService.AddTransaction(title, category, amount, false, date);
                txtExpenseTitle.Text = "";
                txtExpenseAmount.Text = "";
                txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadCategoryBreakdowns();
            }
        }

        protected void btnSaveIncome_Click(object sender, EventArgs e)
        {
            string title = txtIncomeTitle.Text.Trim();
            string category = ddlIncomeCategory.SelectedValue;
            string date = txtIncomeDate.Text.Trim();
            decimal amount;

            if (string.IsNullOrEmpty(title))
            {
                title = "Income";
            }

            if (decimal.TryParse(txtIncomeAmount.Text.Trim(), out amount) && amount > 0)
            {
                ExpenseDataService.AddTransaction(title, category, amount, true, date);
                txtIncomeTitle.Text = "";
                txtIncomeAmount.Text = "";
                txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSaveCategory_Click(object sender, EventArgs e)
        {
            string newCat = txtNewCategoryName.Text.Trim();
            if (!string.IsNullOrEmpty(newCat))
            {
                // Add new category option to dropdown if not already exists
                if (ddlExpenseCategory.Items.FindByValue(newCat) == null)
                {
                    ddlExpenseCategory.Items.Add(new ListItem(newCat, newCat));
                }
                txtNewCategoryName.Text = "";
            }
        }
    }
}
