using System;
using System.Collections.Generic;
using System.Text;

namespace ODATA.Client.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
