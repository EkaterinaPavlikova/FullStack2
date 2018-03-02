using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIServer.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public int GroupNumber { get; set; }

        public List <Student> Students { get; set; }
    }
}
