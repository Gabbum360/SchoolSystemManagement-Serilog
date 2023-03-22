using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Book
    {
        public Book(string title)
        {
            if(string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title can not be null for this book");
            Title = title;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string PublishedBy { get; set; }
    }
}
