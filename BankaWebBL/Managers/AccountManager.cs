using AutoMapper;
using BankaWebDL;
using BankaWebEL.DTOs;
using BankaWebEL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BankaWebBL.Managers
{
    public class AccountManager : BaseManager<Account, AccountDTO, int>
    {
        public AccountManager(BankaWebDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<AccountType> GetAccountTypes()
        {
            return _context.AccountTypes.ToList();
        }

        public List<Currency> GetCurrencies()
        {
            return _context.Currencies.ToList();
        }

        
        public void AddOrUpdate(AccountDTO accountDto)
        {
            var accountEntity = _mapper.Map<Account>(accountDto);
            if (accountEntity.Id == 0) 
            {
                _context.Accounts.Add(accountEntity);
            }
            else 
            {
                _context.Accounts.Update(accountEntity);
            }
            _context.SaveChanges();
        }

        public void Add(Account account)
        {
            _context.Accounts.Add(account); 
            _context.SaveChanges();   
        }

    }
}
