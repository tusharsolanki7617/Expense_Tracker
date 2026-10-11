# Expense_Tracker

A personal finance management web application built using **ASP.NET Web Forms and C#**. Expense Tracker helps users organize their finances by managing income, expenses, budgets, transactions, bill reminders, subscriptions, loans, and personal notes in one place.

The application provides a centralized dashboard to review financial activity, manage expense categories, and understand spending patterns. Its goal is to make everyday money management easier, more organized, and more efficient.

## 📌 Table of Contents

- [Project Overview](#-project-overview)
- [Objectives](#-objectives)
- [Key Features](#-key-features)
- [Application Pages](#-application-pages)
- [Project Architecture](#-project-architecture)
- [Project Structure](#-project-structure)
- [Technology Stack](#-technology-stack)
- [How It Works](#-how-it-works)
- [Installation and Setup](#-installation-and-setup)
- [Future Enhancements](#-future-enhancements)

## 📖 Project Overview

Managing personal finances can become difficult when income, expenses, bills, subscriptions, and loans are tracked separately. This project aims to provide a single web-based platform for organizing these financial activities.

Users can access dedicated pages for recording expenses and income, reviewing transactions, managing budgets, organizing categories, tracking loans, and maintaining bill reminders. The dashboard acts as a central place for accessing financial information.

The application uses ASP.NET Web Forms to create interactive web pages and C# for application logic and event handling.

## 🎯 Objectives

- Simplify daily income and expense tracking.
- Organize financial records into categories.
- Help users plan and monitor budgets.
- Provide a central dashboard for financial information.
- Make transactions easier to search and review.
- Keep bill, subscription, and loan information organized.
- Provide account registration, login, and profile management.

## 🚀 Key Features

### 1. User Authentication
- Registration page for creating an account.
- Login page for accessing the application.
- Profile page for managing user information.

### 2. Dashboard
- Central navigation to major application features.
- Financial overview and summary information, depending on the implemented dashboard logic.

### 3. Income Management
- Record income entries.
- Organize income information.
- Review income records.

### 4. Expense Management
- Record daily expenses.
- Enter transaction details.
- Maintain expense records for financial tracking.

### 5. Transaction History
- View financial transactions in one place.
- Review recorded income and expenses.
- Search transactions using the available search functionality.

### 6. Budget Management
- Organize budget information.
- Monitor spending against planned budgets where supported by the implementation.

### 7. Category Management
- Organize expenses using categories.
- Keep financial records structured and easier to review.

### 8. Bill Reminders
- Maintain bill-related information.
- Organize upcoming payment reminders, according to the implemented functionality.

### 9. Subscription Management
- Maintain subscription information.
- Keep recurring service expenses organized.

### 10. Loan Tracker
- Maintain loan-related records.
- Organize loan information for easier review.

### 11. Currency Management
- Access the currency management page.
- Support the currency options provided by the application.

### 12. Notes and Memos
- Keep personal financial notes.
- Store useful reminders and information related to financial management.

### 13. Search
- Access the search page to locate relevant financial records using the available search features.

## 📱 Application Pages

| Page | Purpose |
|---|---|
| `Login.aspx` | User login |
| `Register.aspx` | Account registration |
| `Dashboard.aspx` | Main dashboard |
| `Income.aspx` | Income management |
| `LogExpense.aspx` | Record expenses |
| `Transactions.aspx` | View transaction records |
| `Budget.aspx` | Budget management |
| `Categories.aspx` | Manage categories |
| `Search.aspx` | Search financial information |
| `BillReminders.aspx` | Bill reminder management |
| `Subscriptions.aspx` | Subscription management |
| `LoanTracker.aspx` | Loan tracking |
| `Currency.aspx` | Currency management |
| `NotesMemos.aspx` | Notes and memos |
| `Profile.aspx` | User profile |

## 🏗️ Project Architecture

The application follows the ASP.NET Web Forms page and code-behind approach.

```text
User
  |
  v
Web Browser
  |
  v
ASP.NET Web Forms Pages (.aspx)
  |
  v
C# Code-Behind Files (.aspx.cs)
  |
  v
Services / Models
  |
  v
Data Storage
```

**Architecture components:**

- **Presentation layer:** `.aspx` pages display the interface and collect user input.
- **Application logic:** `.aspx.cs` code-behind files handle page events and business logic.
- **Models:** Represent application data and related structures.
- **Services:** Provide reusable operations and supporting functionality.
- **Data storage:** Stores information using the configured storage mechanism.

The exact database and service implementations should be documented according to the source code.

## 📂 Project Structure

```text
Expense_Tracker/
│
├── App_Data/
│   └── Application data files, if configured
│
├── Content/
│   └── Stylesheets and other content resources
│
├── Models/
│   └── Application data models
│
├── Properties/
│   └── Project configuration
│
├── Services/
│   └── Reusable application services
│
├── Login.aspx
├── Login.aspx.cs
├── Login.aspx.designer.cs
│
├── Register.aspx
├── Register.aspx.cs
├── Register.aspx.designer.cs
│
├── Dashboard.aspx
├── Dashboard.aspx.cs
├── Dashboard.aspx.designer.cs
│
├── Income.aspx
├── Income.aspx.cs
│
├── LogExpense.aspx
├── LogExpense.aspx.cs
│
├── Transactions.aspx
├── Transactions.aspx.cs
│
├── Budget.aspx
├── Budget.aspx.cs
│
├── Categories.aspx
├── Categories.aspx.cs
│
├── Search.aspx
├── Search.aspx.cs
│
├── BillReminders.aspx
├── Subscriptions.aspx
├── LoanTracker.aspx
├── Currency.aspx
├── NotesMemos.aspx
├── Profile.aspx
│
├── Web.config
├── packages.config
├── Expense_Tracker.csproj
└── README.md
```

*Note: This is a simplified overview of the repository. The `.aspx.designer.cs` files, additional configuration files, and other supporting files are omitted where not shown. Folder contents should be verified against the source code.*

## 🛠️ Technology Stack

| Technology | Purpose |
|---|---|
| ASP.NET Web Forms | Web application framework |
| C# | Application logic and event handling |
| HTML | Page structure |
| CSS | Page styling |
| JavaScript | Client-side interactions, where implemented |
| .NET Framework | Application runtime and libraries |
| Visual Studio | Development environment |

## ⚙️ How It Works

1. The user opens the web application in a browser.
2. The user registers or logs in, depending on the configured authentication system.
3. The dashboard provides access to the different finance-management pages.
4. The user records income and expenses.
5. The user reviews transactions, categories, and budget information.
6. The user manages related financial records, such as bills, subscriptions, loans, and notes.
7. The application's C# logic processes page actions and interacts with the configured data-storage layer.

## 💻 Installation and Setup

### Prerequisites

- Windows environment compatible with the project's target .NET Framework.
- Visual Studio with the required ASP.NET and .NET Framework development components.
- The configured database or other data-storage dependencies, if required.

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/tusharsolanki7617/Expense_Tracker.git
   ```

2. Open the downloaded project folder.

3. Open `Expense_Tracker.csproj` using a compatible Visual Studio installation.

4. Restore the project's NuGet packages.

5. Review `Web.config` and configure any required database connections or application settings.

6. Build the solution and resolve any missing framework or dependency errors.

7. Run the application using Visual Studio's supported web development configuration.

**macOS note:** This repository uses ASP.NET Web Forms and a .NET Framework project. It may not run directly on macOS with modern Visual Studio for Mac alternatives. Use a compatible Windows environment for the most reliable development and execution experience.

## 🔮 Future Enhancements

- Add financial charts and spending reports.
- Improve monthly budget summaries and notifications.
- Add CSV or PDF report exports.
- Improve search filters for transactions.
- Enhance validation and error handling.
- Improve mobile-responsive layouts.
- Strengthen authentication and data protection.
- Add automated tests for important application features.

## 🎓 Project Purpose

Expense Tracker is a software development project focused on applying web development concepts to personal finance management. It demonstrates a multi-page web application structure, C# code-behind logic, reusable services, and the organization of common financial-management features.

## 🔗 Repository

GitHub: https://github.com/tusharsolanki7617/Expense_Tracker
