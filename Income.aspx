// this is income page

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Income.aspx.cs" Inherits="Expense_Tracker.Income" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <!-- Defines character encoding and responsive viewport settings -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <!-- Sets the browser tab title -->
    <title>Add Salary / Income - Expense Tracker</title>

    <!-- Links the external CSS stylesheet for page design and layout -->
    <link href="Content/dashboard.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <!-- Main ASP.NET server form required for Web Forms controls and postbacks -->
    <form id="form1" runat="server">

        <!-- Main application container containing the sidebar and page content -->
        <div class="app-container">

            <!-- Sidebar Navigation: Contains links to all application modules -->
            <aside class="sidebar">
                <div>

                    <!-- Application logo and brand name -->
                    <div class="brand-header">
                        <div class="brand-icon-box">
                            <!-- SVG icon representing the Indian rupee -->
                            <svg viewBox="0 0 24 24" class="rupee-icon">
                                <path d="M6 3h12M6 8h12M6 13l8.5 8M6 13h3a4 4 0 0 0 4-4 4 4 0 0 0-4-4H6"></path>
                            </svg>
                        </div>
                        <span class="brand-title">Expense Tracker</span>
                    </div>

                    <!-- Navigation menu linking to different pages -->
                    <ul class="nav-menu">

                        <!-- Opens the dashboard page -->
                        <li class="nav-item">
                            <a href="Dashboard.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="7" height="7" x="3" y="3" rx="1"/><rect width="7" height="7" x="14" y="3" rx="1"/><rect width="7" height="7" x="14" y="14" rx="1"/><rect width="7" height="7" x="3" y="14" rx="1"/></svg>
                                </span>
                                1. Dashboard
                            </a>
                        </li>

                        <!-- Opens the complete transaction history -->
                        <li class="nav-item">
                            <a href="Transactions.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="m17 2 4 4-4 4"/><path d="M3 11v-1a4 4 0 0 1 4-4h14"/><path d="m7 22-4-4 4-4"/><path d="M21 13v1a4 4 0 0 1-4 4H3"/></svg>
                                </span>
                                2. All Transactions
                            </a>
                        </li>

                        <!-- Opens the category-wise expense breakdown -->
                        <li class="nav-item">
                            <a href="Categories.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="M21.21 15.89A10 10 0 1 1 8 2.83"/><path d="M22 12A10 10 0 0 0 12 2v10z"/></svg>
                                </span>
                                3. Categories Breakdown
                            </a>
                        </li>

                        <!-- Opens the monthly budget management page -->
                        <li class="nav-item">
                            <a href="Budget.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><line x1="12" x2="12" y1="2" y2="22"/><path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/></svg>
                                </span>
                                4. Monthly Budget
                            </a>
                        </li>

                        <!-- Opens the form for recording a new expense -->
                        <li class="nav-item">
                            <a href="LogExpense.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="20" height="14" x="2" y="5" rx="2"/><line x1="2" x2="22" y1="10" y2="10"/></svg>
                                </span>
                                5. Log Expense
                            </a>
                        </li>

                        <!-- Highlights the currently active income page -->
                        <li class="nav-item active">
                            <a href="Income.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="20" height="14" x="2" y="7" rx="2"/><path d="M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16"/></svg>
                                </span>
                                6. Add Salary / Income
                            </a>
                        </li>

                        <!-- Opens the transaction search page -->
                        <li class="nav-item">
                            <a href="Search.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.3-4.3"/></svg>
                                </span>
                                7. Search
                            </a>
                        </li>

                        <!-- Opens currency settings -->
                        <li class="nav-item">
                            <a href="Currency.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><line x1="12" x2="12" y1="2" y2="22"/><path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/></svg>
                                </span>
                                8. Currency Settings
                            </a>
                        </li>

                        <!-- Opens the user's profile and settings -->
                        <li class="nav-item">
                            <a href="Profile.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="M19 21v-2a4 4 0 0 0-4-4H9a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>
                                </span>
                                9. Profile &amp; Settings
                            </a>
                        </li>

                        <!-- Opens subscription tracking -->
                        <li class="nav-item">
                            <a href="Subscriptions.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="20" height="14" x="2" y="5" rx="2"/><line x1="2" x2="22" y1="10" y2="10"/></svg>
                                </span>
                                10. Subscriptions
                            </a>
                        </li>

                        <!-- Opens bill reminders -->
                        <li class="nav-item">
                            <a href="BillReminders.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="M17 8h1a4 4 0 1 1 0 8h-1"/><path d="M3 8h14v9a4 4 0 0 1-4 4H7a4 4 0 0 1-4-4Z"/><line x1="6" x2="6" y1="2" y2="4"/><line x1="10" x2="10" y1="2" y2="4"/><line x1="14" x2="14" y1="2" y2="4"/></svg>
                                </span>
                                11. Bill Reminders
                            </a>
                        </li>

                        <!-- Opens the loan tracking page -->
                        <li class="nav-item">
                            <a href="LoanTracker.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><line x1="12" x2="12" y1="2" y2="22"/><path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/></svg>
                                </span>
                                12. Loan Tracker
                            </a>
                        </li>

                        <!-- Opens the notes and memos page -->
                        <li class="nav-item">
                            <a href="NotesMemos.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z"/><polyline points="14 2 14 8 20 8"/></svg>
                                </span>
                                13. Notes &amp; Memos
                            </a>
                        </li>
                    </ul>
                </div>

                <!-- Sidebar footer containing the logout link -->
                <div class="sidebar-footer">
                    <a href="Login.aspx" class="logout-btn">
                        <!-- Logout icon -->
                        <svg viewBox="0 0 24 24"><path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"/><polyline points="16 17 21 12 16 7"/><line x1="21" x2="9" y1="12" y2="12"/></svg>
                        Logout
                    </a>
                </div>
            </aside>

            <!-- Main content area displayed beside the sidebar -->
            <div class="main-wrapper">

                <!-- Top header containing the page title and user information -->
                <header class="top-bar">
                    <div class="top-bar-left">

                        <!-- Button used to toggle the sidebar on supported layouts -->
                        <button type="button" class="menu-toggle" aria-label="Toggle menu">
                            <svg viewBox="0 0 24 24"><line x1="4" x2="20" y1="12" y2="12"/><line x1="4" x2="20" y1="6" y2="6"/><line x1="4" x2="20" y1="18" y2="18"/></svg>
                        </button>

                        <!-- Current page heading -->
                        <h1 class="page-title">Add Salary / Income</h1>
                    </div>

                    <!-- Displays the logged-in user's profile information -->
                    <div class="top-bar-right">
                        <a href="Profile.aspx" class="user-profile" title="Account & Profile Settings">

                            <!-- Server-side literal displays the user's initial -->
                            <div class="user-avatar">
                                <asp:Literal ID="litUserInitial" runat="server">T</asp:Literal>
                            </div>

                            <!-- Server-side literal displays the user's name -->
                            <span class="user-name">
                                <asp:Literal ID="litUserName" runat="server">tushar</asp:Literal>
                            </span>
                        </a>
                    </div>
                </header>

                <!-- Main page body containing the income form and recent income list -->
                <main class="content-body">

                    <!-- Two-column layout for the income form and recent transactions -->
                    <div class="log-expense-grid">

                        <!-- LEFT COLUMN: Form for adding a new income transaction -->
                        <div class="log-expense-card">
                            <h2 class="budget-card-title" style="margin-bottom: 22px;">
                                Add New Income
                            </h2>

                            <!-- Input field for the income source, such as salary or freelance work -->
                            <div class="form-group">
                                <label class="form-label-upper">SOURCE</label>
                                <asp:TextBox
                                    ID="txtIncomeSource"
                                    runat="server"
                                    CssClass="form-input-dark"
                                    placeholder="e.g. Monthly Salary, Freelance...">
                                </asp:TextBox>
                            </div>

                            <!-- Row containing the income amount and transaction date -->
                            <div class="form-row-2col">

                                <!-- Input field for the income amount in Indian rupees -->
                                <div class="form-group">
                                    <label class="form-label-upper">AMOUNT (&#8377;)</label>
                                    <asp:TextBox
                                        ID="txtIncomeAmount"
                                        runat="server"
                                        CssClass="form-input-dark"
                                        placeholder="0.00">
                                    </asp:TextBox>
                                </div>

                                <!-- Date input for the income transaction -->
                                <div class="form-group">
                                    <label class="form-label-upper">DATE</label>
                                    <asp:TextBox
                                        ID="txtIncomeDate"
                                        runat="server"
                                        TextMode="Date"
                                        CssClass="form-input-dark">
                                    </asp:TextBox>
                                </div>
                            </div>

                            <!-- Dropdown list for selecting the income category -->
                            <div class="form-group">
                                <label class="form-label-upper">CATEGORY</label>
                                <asp:DropDownList
                                    ID="ddlIncomeCategory"
                                    runat="server"
                                    CssClass="form-input-dark">

                                    <!-- Salary is selected by default -->
                                    <asp:ListItem Value="Salary" Selected="True">Salary</asp:ListItem>

                                    <!-- Other available income categories -->
                                    <asp:ListItem Value="Investment">Investment</asp:ListItem>
                                    <asp:ListItem Value="Freelance">Freelance</asp:ListItem>
                                    <asp:ListItem Value="Bonus">Bonus</asp:ListItem>
                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <!-- Optional text field for additional information about the income -->
                            <div class="form-group">
                                <label class="form-label-upper">NOTE (OPTIONAL)</label>
                                <asp:TextBox
                                    ID="txtIncomeNote"
                                    runat="server"
                                    CssClass="form-input-dark"
                                    placeholder="Any note...">
                                </asp:TextBox>
                            </div>

                            <!-- Submits the form and invokes btnAddIncome_Click in Income.aspx.cs -->
                            <asp:Button
                                ID="btnAddIncome"
                                runat="server"
                                Text="Add Income"
                                CssClass="btn-income-green"
                                OnClick="btnAddIncome_Click" />
                        </div>

                        <!-- RIGHT COLUMN: Displays the user's most recent income transactions -->
                        <div class="recent-expenses-card">

                            <!-- Header for the recent income section -->
                            <div class="recent-expenses-header">
                                <h2 class="budget-card-title" style="margin-bottom:0;">
                                    Recent Income
                                </h2>

                                <!-- Navigates to the full transaction history -->
                                <a href="Transactions.aspx" class="view-all-link">View All</a>
                            </div>

                            <!-- Container holding the list of recent income records -->
                            <div class="transaction-list">

                                <!-- Repeater dynamically generates a row for each income record -->
                                <asp:Repeater ID="rptRecentIncomes" runat="server">
                                    <ItemTemplate>

                                        <!-- Individual income transaction row -->
                                        <div class="transaction-row">

                                            <!-- Left side: icon, title, category, date, and note -->
                                            <div class="transaction-left">

                                                <!-- Selects the icon container CSS class using the income icon type -->
                                                <div class="<%# GetIconBoxClass(Eval("IconType").ToString()) %>">

                                                    <!-- Generates the appropriate SVG icon for the transaction -->
                                                    <%# GetIconSvg(Eval("IconType").ToString()) %>
                                                </div>

                                                <!-- Transaction title and additional information -->
                                                <div class="txn-info">
                                                    <div class="txn-title-row">

                                                        <!-- Displays the income source or transaction title -->
                                                        <span class="txn-title"><%# Eval("Title") %></span>

                                                        <!-- Displays the income category with its corresponding badge style -->
                                                        <span class="category-tag <%# Eval("CategoryBadgeClass") %>">
                                                            <%# Eval("Category") %>
                                                        </span>
                                                    </div>

                                                    <!-- Displays the transaction date and optional note -->
                                                    <span class="txn-date"><%# Eval("DateWithNote") %></span>
                                                </div>
                                            </div>

                                            <!-- Displays the formatted income amount using positive styling -->
                                            <div class="txn-amount amount-positive">
                                                <%# Eval("FormattedAmount") %>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <!-- Empty-state message shown when no income records are available -->
                                <asp:Panel
                                    ID="pnlNoIncomes"
                                    runat="server"
                                    Visible="false"
                                    style="text-align:center; padding: 40px 20px; color:#8a90a6;">

                                    <p>No income transactions logged yet.</p>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </div>
    </form>
</body>
</html>
