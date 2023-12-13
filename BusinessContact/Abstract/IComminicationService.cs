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
    public interface IComminicationService : IServiceBase<Comminication>
    {
        Task<IServiceOutput<Comminication>> CreateAsync(ComminicationModel entity);
        Task<IServiceOutput<List<Comminication>>> RemoveAsync(Guid id);
    }
}
