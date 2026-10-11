using System;

namespace Expense_Tracker.Models
{
    public class NoteModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Content { get; set; }
        public string DateFormatted { get; set; }
    }
}
