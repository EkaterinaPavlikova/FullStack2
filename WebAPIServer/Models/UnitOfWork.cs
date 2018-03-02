using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIServer.Models
{
    public partial class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;
        private IGenericRepository<Student> _studentRepository;
        private IGenericRepository<Group> _groupRepository;
        private IGenericRepository<RecordBook> _recordBookRepository;
        private IGenericRepository<Subject> _subjectRepository;
        private IGenericRepository<StudentSubject> _studentSubjectRepository;

        public UnitOfWork(ApplicationContext context)
        {
            this.context = context;
        }

        public IGenericRepository<Student> StudentRepository
        {
            get
            {
                return _studentRepository = _studentRepository ?? new StudentRepository(context);              
            }
        }

        public IGenericRepository<Group> GroupRepository
        {
            get
            {
                return _groupRepository = _groupRepository ?? new GroupRepository(context);              
            }
        }

        public IGenericRepository<RecordBook> RecordBookRepository
        {
            get
            {
                return _recordBookRepository = _recordBookRepository ?? new RecordBookRepository(context);                
            }
        }

        public IGenericRepository<Subject> SubjectRepository
        {
            get
            {
                return _subjectRepository = _subjectRepository ?? new SubjectRepository(context);
                
            }
        }

        public IGenericRepository<StudentSubject> StudentSubjectRepository
        {
            get
            {
                return _studentSubjectRepository = _studentSubjectRepository ?? new StudentSubjectRepository(context);

            }
        }

        public void Save()
        {
           context.SaveChanges();
        }
      
    }
}
