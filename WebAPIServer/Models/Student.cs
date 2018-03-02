using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebAPIServer.Models
{    
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public RecordBook UserRecordBook { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }
      
        public List<StudentSubject> StudentSubjects { get; set; }
        public Student()
        {
            StudentSubjects = new List<StudentSubject>();
        }

    }
}
