using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaWebEL.Entities
{
    public class Customer : IBaseEntity<string>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public ICollection<Account> Accounts { get; set; }
       
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Customer()
        {
            Accounts = new List<Account>(); 
        }

    }


}
