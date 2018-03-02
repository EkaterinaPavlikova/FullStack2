using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIServer.Models
{
    public interface IUnitOfWork 
    {
        IGenericRepository<Student> StudentRepository { get; }
        IGenericRepository<Group> GroupRepository { get; }
        IGenericRepository<RecordBook> RecordBookRepository{ get; }
        IGenericRepository<Subject> SubjectRepository { get; }
        IGenericRepository<StudentSubject> StudentSubjectRepository { get; }
        void Save();
    }
}
