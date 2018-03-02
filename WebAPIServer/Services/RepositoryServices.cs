using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIServer.Models;

namespace WebAPIServer.Services
{
    public static class RepositoryServices
    {
        public static void AddRepoServices(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
            services.AddScoped<IGenericRepository<Group>, GenericRepository<Group>>();
            services.AddScoped<IGenericRepository<RecordBook>, GenericRepository<RecordBook>>();
            services.AddScoped<IGenericRepository<Subject>, GenericRepository<Subject>>();
            services.AddScoped<IGenericRepository<StudentSubject>, GenericRepository<StudentSubject>>();
        }
    }
}
