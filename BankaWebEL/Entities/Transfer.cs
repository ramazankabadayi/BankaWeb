using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaWebEL.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        public int SenderAccountId { get; set; } 
        public int ReceiverAccountId { get; set; } 

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; } 
        public DateTime TransferDate { get; set; } 
        public string TransferDescription { get; set; } 

        [ForeignKey("SenderAccountId")]
        public Account SenderAccount { get; set; }

        [ForeignKey("ReceiverAccountId")]
        public Account ReceiverAccount { get; set; }
    }
}