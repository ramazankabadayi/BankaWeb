using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaWebEL.DTOs
{
    public class CustomerDTO
    {
        public string Id { get; set; }              
        public string FirstName { get; set; }    
        public string LastName { get; set; }      
        public string Address { get; set; }         
        public string PhoneNumber { get; set; }     
        public string Email { get; set; }           
        public List<AccountDTO>? Accounts { get; set; }  

        public DateTime CreatedDate { get; set; }  
    }

}
