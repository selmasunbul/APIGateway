using Business.Base;
using Core.Abstract;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IInfoTypeService : IServiceBase<InfoType>

    {
        Task<IServiceOutput<List<InfoType>>> GetList();
        Task<IServiceOutput<InfoType>> CreateAsync(string name);
    }
}
