<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Expense_Tracker.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Welcome Back - Expense Tracker</title>
    <link href="Content/auth.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="auth-wrapper">
        <div class="auth-card">
            <!-- Header Icon Badge (Branded Rupee Symbol) -->
            <div class="badge-icon">
                <svg viewBox="0 0 24 24" style="width: 26px; height: 26px; stroke: #ffffff; stroke-width: 2.5; fill: none; stroke-linecap: round; stroke-linejoin: round;">
                    <path d="M6 3h12M6 8h12M6 13l8.5 8M6 13h3a4 4 0 0 0 4-4 4 4 0 0 0-4-4H6"></path>
                </svg>
            </div>

            <!-- Title & Subtitle -->
            <h1 class="auth-title">Welcome Back</h1>
            <p class="auth-subtitle">Log in to manage your budget &amp; expenses</p>

            <!-- Main Form -->
            <form id="form1" runat="server" class="auth-form">
                <!-- Status Message Banner -->
                <asp:Label ID="lblMessage" runat="server" CssClass="auth-alert" Visible="false"></asp:Label>

                <!-- Email Input -->
                <div class="input-group">
                    <span class="input-icon-left">
                        <svg viewBox="0 0 24 24">
                            <rect width="20" height="16" x="2" y="4" rx="2"></rect>
                            <path d="m22 7-8.97 5.7a1.94 1.94 0 0 1-2.06 0L2 7"></path>
                        </svg>
                    </span>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="auth-input" TextMode="Email" placeholder="Email Address" required="required" autocomplete="email"></asp:TextBox>
                </div>

                <!-- Password Input -->
                <div class="input-group">
                    <span class="input-icon-left">
                        <svg viewBox="0 0 24 24">
                            <rect width="18" height="11" x="3" y="11" rx="2" ry="2"></rect>
                            <path d="M7 11V7a5 5 0 0 1 10 0v4"></path>
                        </svg>
                    </span>

                    <asp:TextBox ID="txtPassword" runat="server" CssClass="auth-input" TextMode="Password" placeholder="Password" required="required" autocomplete="current-password"></asp:TextBox>
                    <button type="button" class="toggle-password" id="btnTogglePassword" onclick="togglePasswordVisibility('txtPassword', this)" title="Show/Hide Password" aria-label="Toggle password visibility">
                        <svg id="eyeIcon" viewBox="0 0 24 24">
                            <path d="M2 12s3-7 10-7 10 7 10 7-3 7-10 7-10-7-10-7Z"></path>
                            <circle cx="12" cy="12" r="3"></circle>
                        </svg>
                    </button>
                </div>


                <!-- Action Button -->
                <asp:Button ID="btnLogin" runat="server" Text="Log In" CssClass="btn-submit" OnClick="btnLogin_Click" />

                <!-- Footer Link -->
                <div class="auth-footer">
                    Don't have an account? <a href="Register.aspx">Create Account</a>
                </div>
            </form>
        </div>
    </div>



    <!-- Toggle Password Visibility Script -->
    <script type="text/javascript">
        function togglePasswordVisibility(inputId, toggleBtn) {
            var input = document.getElementById(inputId);
            if (!input) return;

            var isPassword = input.type === 'password';
            input.type = isPassword ? 'text' : 'password';

            if (isPassword) {
                toggleBtn.innerHTML = `
                    <svg viewBox="0 0 24 24">
                        <path d="M9.88 9.88a3 3 0 1 0 4.24 4.24"></path>
                        <path d="M10.73 5.08A10.43 10.43 0 0 1 12 5c7 0 10 7 10 7a13.16 13.16 0 0 1-1.67 2.68"></path>
                        <path d="M6.61 6.61A13.526 13.526 0 0 0 2 12s3 7 10 7a9.74 9.74 0 0 0 5.39-1.61"></path>
                        <line x1="2" y1="2" x2="22" y2="22"></line>
                    </svg>`;
            } else {
                toggleBtn.innerHTML = `
                    <svg viewBox="0 0 24 24">
                        <path d="M2 12s3-7 10-7 10 7 10 7-3 7-10 7-10-7-10-7Z"></path>
                        <circle cx="12" cy="12" r="3"></circle>
                    </svg>`;
            }
        }

    </script>
</body>
</html>
