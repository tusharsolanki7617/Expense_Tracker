using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using Expense_Tracker.Services;

namespace Expense_Tracker
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Visible = false;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Validate required fields
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || 
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                ShowMessage("Please fill in all the required fields.", false);
                return;
            }

            // Validate email format
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                ShowMessage("Please enter a valid email address.", false);
                return;
            }

            // Validate password match
            if (password != confirmPassword)
            {
                ShowMessage("Passwords do not match. Please verify.", false);
                return;
            }

            // Register user in App_Data/users.json
            if (ExpenseDataService.RegisterUser(fullName, email, password, "", out string errorMessage))
            {
                // Auto login new registered user into session
                Session["UserEmail"] = email;
                Session["UserName"] = fullName;

                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                ShowMessage(errorMessage, false);
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
