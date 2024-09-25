using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaWebEL.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string AccountIBAN { get; set; }    
        public decimal Amount { get; set; }        
        public DateTime TransactionDate { get; set; } 
        public string Currency { get; set; }    
    }

}
