


using System;
using System.Web.UI;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Visible = false;

                if (Request.QueryString["registered"] == "1")
                {
                    ShowMessage("Account created successfully! Please log in with your credentials.", true);
                }
                else if (Request.QueryString["logout"] == "1")
                {
                    ShowMessage("You have been successfully logged out.", true);
                }
            }
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ShowMessage("Please enter both email and password.", false);
                return;
            }


            // Authenticate against registered users in App_Data/users.json
            if (ExpenseDataService.ValidateUser(email, password, out var user))
            {
                Session["UserEmail"] = user.Email;
                Session["UserName"] = user.FullName;
                Session["UserPhone"] = user.Phone;
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                // Verify if email is registered to give accurate feedback
                var users = ExpenseDataService.GetUsers();
                bool emailExists = users.Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (!emailExists)
                {
                    ShowMessage("No account found with this email. Please create an account first.", false);
                }
                else
                {
                    ShowMessage("Incorrect password. Please verify your credentials.", false);
                }
            }
        }


        private void ShowMessage(string message, bool isSuccess)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = isSuccess ? "auth-alert visible success" : "auth-alert visible error";
            lblMessage.Visible = true;
        }
    }
}







