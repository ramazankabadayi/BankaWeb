using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaWebEL.DTOs
{
    public class TransferDTO
    {
        public int Id { get; set; }
        public string SenderAccountIBAN { get; set; }    
        public string ReceiverAccountIBAN { get; set; }  
        public decimal Amount { get; set; }               
        public DateTime TransferDate { get; set; }        
        public string TransferDescription { get; set; }  
    }

}
