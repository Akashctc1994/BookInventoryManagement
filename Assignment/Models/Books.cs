using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string  Titles { get; set; }
        public string Author { get; set; }
        public long ISBN { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
    }
}