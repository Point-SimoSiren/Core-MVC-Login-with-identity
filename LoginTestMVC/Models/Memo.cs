using System;
using System.Collections.Generic;

namespace LoginTestMVC.Models
{
    public partial class Memo
    {
        public int Id { get; set; }
        public string Note { get; set; } = null!;
        public bool Important { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
