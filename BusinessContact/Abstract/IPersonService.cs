using Business.Base;
using Core.Abstract;
using DataAccess;
using DataAccess.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPersonService : IServiceBase<Person>
    {
        Task<IServiceOutput<List<Person>>> GetList();
        Task<IServiceOutput<Person>> CreateAsync(PersonModel entity);
        Task<IServiceOutput<List<Person>>> RemoveAsync(Guid id);
        Task<IServiceOutput<Person>> GetById(Guid kisiId);
    }
}
