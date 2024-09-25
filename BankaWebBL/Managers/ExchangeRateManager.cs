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
    public class ExchangeRateManager : BaseManager<ExchangeRate, ExchangeRateDTO, int>
    {
        public ExchangeRateManager(BankaWebDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

    }

}
