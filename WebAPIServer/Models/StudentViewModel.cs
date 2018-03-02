using System;

namespace WebAPIServer.Models
{
    public class StudentViewModel
    {

        public int Id;
        public string FullName;
        public DateTime DateOfBirth;
        public int? UserRecordBookID;
        public int? GroupId;
        public int? GroupNum;

        public StudentViewModel(Student student)
        {
            Id = student.Id;
            FullName = student.FullName;
            DateOfBirth = student.DateOfBirth;
            UserRecordBookID = student.UserRecordBook?.RecordBookId;
            GroupId = student.GroupId;
            GroupNum = student.Group?.GroupNumber ?? 0;
        }
    }
}
