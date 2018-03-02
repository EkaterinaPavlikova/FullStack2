using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIServer.Models
{
    public class RecordBook
    {
        public int RecordBookId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}
