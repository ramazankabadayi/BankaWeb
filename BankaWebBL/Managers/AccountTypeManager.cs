using AutoMapper;
using BankaWebDL;
using BankaWebEL.DTOs;
using BankaWebEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaWebBL.Managers
{
    public class AccountTypeManager : BaseManager<AccountType, AccountTypeDTO, int>
    {

        public AccountTypeManager(BankaWebDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
