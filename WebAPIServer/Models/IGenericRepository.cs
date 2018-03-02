using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIServer.Models
{
    public interface IGenericRepository<T> where T : class 
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationContext context;
        public GenericRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(T item)
        {
            context.Set<T>().Add(item);
        }

        public void Delete(T item)
        {
            if (item != null)
            {
                context.Set<T>().Remove(item);
            }
        }

        public virtual T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
           return context.Set<T>().Include(i=>i);
        }

        public void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }




    public class StudentRepository: GenericRepository<Student>
    {
        public StudentRepository(ApplicationContext context) :base(context)
            { }

        public override IEnumerable <Student> GetAll() {
            return context.Students
                .Include(s => s.Group)
                     .ThenInclude(g=> g.Students)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .Include(s => s.UserRecordBook);
        }
    }

    public class GroupRepository : GenericRepository<Group>
    {

        public GroupRepository(ApplicationContext context) : base(context)
        { }

        public override IEnumerable<Group> GetAll()
        {
            return context.Groups
                .Include(s => s.Students);
        }

        
    }

    public class SubjectRepository : GenericRepository<Subject>
    {
        public SubjectRepository(ApplicationContext context) : base(context)
        { }
              
        public override IEnumerable<Subject> GetAll()
        {
            return context.Subjects
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss=>ss.Student);
        }

    }


    public class RecordBookRepository : GenericRepository<RecordBook>
    {
        public RecordBookRepository(ApplicationContext context) : base(context)
        { }

        public override IEnumerable<RecordBook> GetAll()
        {
            return context.RecordBooks
                .Include(r => r.Student);
        }

    }

    public class StudentSubjectRepository : GenericRepository<StudentSubject>
    {
        public StudentSubjectRepository(ApplicationContext context) : base(context)
        { }

        public override IEnumerable<StudentSubject> GetAll()
        {
            return context.StudentSubject
                .Include(r => r.Student);

        }


    }
}
