// this is budget page u can view budget and many more
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Budget.aspx.cs" Inherits="Expense_Tracker.Budget" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Monthly Budget - Expense Tracker</title>
    <link href="Content/dashboard.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="app-container">
            <!-- Sidebar Navigation -->
            <aside class="sidebar">
                <div>
                    <!-- Brand / App Logo -->
                    <div class="brand-header">
                        <div class="brand-icon-box">
                            <svg viewBox="0 0 24 24" class="rupee-icon">
                                <path d="M6 3h12M6 8h12M6 13l8.5 8M6 13h3a4 4 0 0 0 4-4 4 4 0 0 0-4-4H6"></path>
                            </svg>
                        </div>
                        <span class="brand-title">Expense Tracker</span>
                    </div>


                    <!-- Navigation Links -->
                    <ul class="nav-menu">
                        <li class="nav-item">
                            <a href="Dashboard.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="7" height="7" x="3" y="3" rx="1"/><rect width="7" height="7" x="14" y="3" rx="1"/><rect width="7" height="7" x="14" y="14" rx="1"/><rect width="7" height="7" x="3" y="14" rx="1"/></svg>
                                </span>
                                1. Dashboard
                            </a>
                        </li>
                    <!--- nav 2 ---->
                        <li class="nav-item">
                            <a href="Transactions.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="m17 2 4 4-4 4"/><path d="M3 11v-1a4 4 0 0 1 4-4h14"/><path d="m7 22-4-4 4-4"/><path d="M21 13v1a4 4 0 0 1-4 4H3"/></svg>
                                </span>
                                2. All Transactions
                            </a>
                        </li>
                    <!---- nav 3 --->
                        <li class="nav-item">
                            <a href="Categories.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="M21.21 15.89A10 10 0 1 1 8 2.83"/><path d="M22 12A10 10 0 0 0 12 2v10z"/></svg>
                                </span>
                                3. Categories Breakdown
                            </a>
                        </li>
                        <li class="nav-item active">
                            <a href="Budget.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><line x1="12" x2="12" y1="2" y2="22"/><path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/></svg>
                                </span>
                                4. Monthly Budget
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="LogExpense.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="20" height="14" x="2" y="5" rx="2"/><line x1="2" x2="22" y1="10" y2="10"/></svg>
                                </span>
                                5. Log Expense
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Income.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="20" height="14" x="2" y="7" rx="2"/><path d="M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16"/></svg>
                                </span>
                                6. Add Salary / Income
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Search.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.3-4.3"/></svg>
                                </span>
                                7. Search
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Currency.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><line x1="12" x2="12" y1="2" y2="22"/><path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/></svg>
                                </span>
                                8. Currency Settings
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Profile.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="M19 21v-2a4 4 0 0 0-4-4H9a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>
                                </span>
                                9. Profile &amp; Settings
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="Subscriptions.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><rect width="20" height="14" x="2" y="5" rx="2"/><line x1="2" x2="22" y1="10" y2="10"/></svg>
                                </span>
                                10. Subscriptions
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="BillReminders.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><path d="M17 8h1a4 4 0 1 1 0 8h-1"/><path d="M3 8h14v9a4 4 0 0 1-4 4H7a4 4 0 0 1-4-4Z"/><line x1="6" x2="6" y1="2" y2="4"/><line x1="10" x2="10" y1="2" y2="4"/><line x1="14" x2="14" y1="2" y2="4"/></svg>
                                </span>
                                11. Bill Reminders
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="LoanTracker.aspx">
                                <span class="nav-icon">
                                    <svg viewBox="0 0 24 24"><line x1="12" x2="12" y1="2" y2="22"/><path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/></svg>
                                </span>
                                12. Loan Tracker
                            </a>
                        </li>
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

                <!-- Logout Button -->
                <div class="sidebar-footer">
                    <a href="Login.aspx" class="logout-btn">
                        <svg viewBox="0 0 24 24"><path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"/><polyline points="16 17 21 12 16 7"/><line x1="21" x2="9" y1="12" y2="12"/></svg>
                        Logout
                    </a>
                </div>
            </aside>

            <!-- Main Content Wrapper -->
            <div class="main-wrapper">
                <!-- Top Header Bar -->
                <header class="top-bar">
                    <div class="top-bar-left">
                        <button type="button" class="menu-toggle" aria-label="Toggle menu">
                            <svg viewBox="0 0 24 24"><line x1="4" x2="20" y1="12" y2="12"/><line x1="4" x2="20" y1="6" y2="6"/><line x1="4" x2="20" y1="18" y2="18"/></svg>
                        </button>
                        <div>
                            <h1 class="page-title">Monthly Budget</h1>
                            <p style="font-size: 13px; color: #8c91a6; margin-top: 2px;">Set budget limits per category to manage your spending</p>
                        </div>
                    </div>

                    <div class="top-bar-right">
                        <div class="quick-actions">
                            <a href="LogExpense.aspx" class="btn-quick btn-quick-expense">
                                <svg viewBox="0 0 24 24"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
                                Log Expense
                            </a>
                            <button type="button" class="btn-quick btn-quick-income" onclick="openIncomeModal();">
                                <svg viewBox="0 0 24 24"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
                                Add Income
                            </button>
                        </div>

                        <a href="Profile.aspx" class="user-profile" title="Account & Profile Settings">
                            <div class="user-avatar">
                                <asp:Literal ID="litUserInitial" runat="server">T</asp:Literal>
                            </div>
                            <span class="user-name">
                                <asp:Literal ID="litUserName" runat="server">tushar</asp:Literal>
                            </span>
                        </a>
                    </div>
                </header>

                <!-- Page Content Body -->
                <main class="content-body">
                    <div class="budget-container">
                        <!-- Top Card: Set Budget -->
                        <div class="budget-set-card">
                            <h2 class="budget-card-title">Set Budget</h2>
                            <p class="budget-card-subtitle">Set or update your monthly budget limit for a category</p>

                            <div class="budget-inline-form">
                                <div class="budget-form-group">
                                    <label class="form-label-upper">Category</label>
                                    <asp:DropDownList ID="ddlBudgetCategory" runat="server" CssClass="form-input-dark" style="margin-bottom:0;">
                                        <asp:ListItem Value="Food">Food</asp:ListItem>
                                        <asp:ListItem Value="Shopping">Shopping</asp:ListItem>
                                        <asp:ListItem Value="Transport">Transport</asp:ListItem>
                                        <asp:ListItem Value="Entertainment">Entertainment</asp:ListItem>
                                        <asp:ListItem Value="Health">Health</asp:ListItem>
                                        <asp:ListItem Value="Utilities">Utilities</asp:ListItem>
                                        <asp:ListItem Value="Bills">Bills</asp:ListItem>
                                        <asp:ListItem Value="Gift">Gift</asp:ListItem>
                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="budget-form-group">
                                    <label class="form-label-upper">Budget Limit (&#8377;)</label>
                                    <asp:TextBox ID="txtBudgetAmount" runat="server" CssClass="form-input-dark" placeholder="e.g. 5000" style="margin-bottom:0;"></asp:TextBox>
                                </div>

                                <div class="budget-btn-wrap">
                                    <asp:Button ID="btnSetBudget" runat="server" Text="Set Budget" CssClass="btn-budget-submit" OnClick="btnSetBudget_Click" />
                                </div>
                            </div>
                        </div>

                        <!-- Main Card: Budget vs Spending -->
                        <div class="budget-list-card">
                            <h2 class="budget-card-title">Budget vs Spending</h2>
                            <p class="budget-card-subtitle">Track how much you've spent against your budget limits</p>

                            <div class="budget-items-list">
                                <asp:Repeater ID="rptBudgets" runat="server" OnItemCommand="rptBudgets_ItemCommand">
                                    <ItemTemplate>
                                        <div class="budget-item-card">
                                            <div class="budget-item-header">
                                                <div class="budget-item-cat">
                                                    <div class="budget-icon-circle">
                                                        <%# Eval("IconSvg") %>
                                                    </div>
                                                    <span><%# Eval("Category") %></span>
                                                </div>

                                                <div class="budget-item-actions">
                                                    <%# (bool)Eval("IsOverBudget") ? "<span class='badge-over-budget'><svg viewBox='0 0 24 24' width='12' height='12' stroke='currentColor' fill='none' stroke-width='2' style='margin-right:4px; vertical-align:-1px;'><path d='m21.73 18-8-14a2 2 0 0 0-3.48 0l-8 14A2 2 0 0 0 4 21h16a2 2 0 0 0 1.73-3Z'/><line x1='12' y1='9' x2='12' y2='13'/><line x1='12' y1='17' x2='12.01' y2='17'/></svg>Over budget</span>" : "" %>
                                                    <asp:LinkButton ID="btnRemove" runat="server" CommandName="RemoveBudget" CommandArgument='<%# Eval("Category") %>' CssClass="btn-remove-budget">
                                                        <svg viewBox="0 0 24 24" width="13" height="13" stroke="currentColor" stroke-width="2" fill="none" style="vertical-align:-1px;"><polyline points="3 6 5 6 21 6"></polyline><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path></svg>
                                                        Remove
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class="progress-track">
                                                <div class="progress-fill" style="<%# Eval("BarWidthStyle") %>"></div>
                                            </div>

                                            <div class="budget-item-stats">
                                                <div class="budget-spent-total">
                                                    <strong>&#8377;<%# ((decimal)Eval("SpentAmount")).ToString("N2") %></strong> / &#8377;<%# ((decimal)Eval("BudgetAmount")).ToString("N2") %>
                                                </div>
                                                <div class="<%# (bool)Eval("IsOverBudget") ? "budget-pct-value over" : "budget-pct-value" %>">
                                                    <%# Eval("Percentage") %>%
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <asp:Panel ID="pnlNoBudgets" runat="server" Visible="false" style="text-align:center; padding: 40px 20px; color:#8a90a6;">
                                    <p>No budgets configured yet. Use the form above to set your first monthly budget!</p>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </div>

        <!-- Add Income Modal Dialog -->
        <div id="incomeModal" class="modal-overlay">
            <div class="modal-box">
                <div class="modal-header">
                    <div class="modal-title">
                        <svg viewBox="0 0 24 24" width="20" height="20" stroke="#10b981" stroke-width="2" fill="none" style="margin-right: 8px; vertical-align: -3px;"><rect width="20" height="14" x="2" y="7" rx="2"/><path d="M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16"/></svg>
                        Add Salary / Income
                    </div>
                    <button type="button" class="modal-close" onclick="closeIncomeModal();">&times;</button>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Title / Source</label>
                    <asp:TextBox ID="txtIncomeTitle" runat="server" CssClass="modal-input" placeholder="e.g. Monthly Salary, Freelance"></asp:TextBox>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Category</label>
                    <asp:DropDownList ID="ddlIncomeCategory" runat="server" CssClass="modal-select">
                        <asp:ListItem Value="Salary">Salary</asp:ListItem>
                        <asp:ListItem Value="Bonus">Bonus</asp:ListItem>
                        <asp:ListItem Value="Investments">Investments</asp:ListItem>
                        <asp:ListItem Value="Other">Other</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Amount (&#8377;)</label>
                    <asp:TextBox ID="txtIncomeAmount" runat="server" CssClass="modal-input" placeholder="0.00"></asp:TextBox>
                </div>
                <div class="modal-field">
                    <label class="modal-label">Date</label>
                    <asp:TextBox ID="txtIncomeDate" runat="server" TextMode="Date" CssClass="modal-input"></asp:TextBox>
                </div>
                <asp:Button ID="btnSubmitIncome" runat="server" Text="Add Income" CssClass="btn-modal-submit btn-income-submit" OnClick="btnSubmitIncome_Click" />
            </div>
        </div>
    </form>



    <script>
        function openIncomeModal() {
            document.getElementById('incomeModal').classList.add('active');
        }
        function closeIncomeModal() {
            document.getElementById('incomeModal').classList.remove('active');
        }
    </script>
</body>
</html>



