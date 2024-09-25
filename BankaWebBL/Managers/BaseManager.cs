using AutoMapper;
using BankaWebDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaWebBL.Managers
{
    public class BaseManager<T, TDTO, Tid> where T : class, new()
    {
        protected readonly BankaWebDbContext _context;
        protected readonly IMapper _mapper;

        public BaseManager(BankaWebDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual List<TDTO> GetAll()
        {
            var entities = _context.Set<T>().ToList();
            return _mapper.Map<List<TDTO>>(entities);
        }

        public virtual TDTO GetById(Tid id)
        {
            var entity = _context.Set<T>().Find(id);
            return _mapper.Map<TDTO>(entity);
        }

        public virtual void Add(TDTO dto)
        {
            var entity = _mapper.Map<T>(dto);
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(TDTO dto)
        {
            var entity = _mapper.Map<T>(dto);
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
       
        public virtual void Delete(Tid id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }
    }

}
