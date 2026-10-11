using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class BillReminders : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserInfo();
                txtBillDueDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindBills();
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

        private void BindBills()
        {
            var bills = ExpenseDataService.GetBills();
            if (bills != null && bills.Count > 0)
            {
                rptBills.DataSource = bills;
                rptBills.DataBind();
                rptBills.Visible = true;
                pnlNoBills.Visible = false;
            }
            else
            {
                rptBills.Visible = false;
                pnlNoBills.Visible = true;
            }
        }

        protected void btnAddReminder_Click(object sender, EventArgs e)
        {
            string name = txtBillName.Text.Trim();
            string dueDate = txtBillDueDate.Text.Trim();

            if (decimal.TryParse(txtBillAmount.Text.Trim(), out decimal amount) && amount > 0)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = "Bill";
                }

                if (string.IsNullOrWhiteSpace(dueDate))
                {
                    dueDate = DateTime.Now.ToString("dd/MM/yyyy");
                }

                // Status can be determined based on date: if date < today, Overdue; else Upcoming
                string status = "Upcoming";
                if (DateTime.TryParseExact(dueDate, new[] { "d/M/yyyy", "dd/MM/yyyy", "yyyy-MM-dd" },
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    if (parsedDate.Date < DateTime.Now.Date)
                    {
                        status = "Overdue";
                    }
                }

                ExpenseDataService.AddBill(name, amount, dueDate, status);

                txtBillName.Text = "";
                txtBillAmount.Text = "";
                txtBillDueDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                BindBills();
            }
        }

        protected void rptBills_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteBill")
            {
                if (int.TryParse(e.CommandArgument?.ToString(), out int id))
                {
                    ExpenseDataService.DeleteBill(id);
                    BindBills();
                }
            }
        }
    }
}
