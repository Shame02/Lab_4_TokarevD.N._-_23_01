using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_TokarevD.N._БПИ_23_01.Model
{
    public class Person
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public Person() { }

        public Person(int id, int roleId, string firstName, string lastName, DateTime birthday)
        {
            Id = id;
            RoleId = roleId;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        public Person CopyFromPersonDPO(PersonDpo dpo)
        {
            return new Person
            {
                Id = dpo.Id,
                RoleId = dpo.RoleId,
                FirstName = dpo.FirstName,
                LastName = dpo.LastName,
                Birthday = dpo.Birthday
            };
        }
    }

}
