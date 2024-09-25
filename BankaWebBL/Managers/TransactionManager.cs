using AutoMapper;
using BankaWebDL;
using BankaWebEL.DTOs;
using BankaWebEL.Entities;

namespace BankaWebBL.Managers
{
    public class TransactionManager : BaseManager<Transaction, TransactionDTO, int>
    {
        public TransactionManager(BankaWebDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

      
       
        
    }
}
