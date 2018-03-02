using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIServer.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        [Required]
        public string SubjectName { get; set; }

        
        public List<StudentSubject> StudentSubjects { get; set;}
        public Subject()
        {
            StudentSubjects = new List<StudentSubject>();
        }
    }
}
