using Business.Base;
using Core.Abstract;
using DataAccess;
using DataAccess.Entity;
using DataAccess.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRaportService : IServiceBase<Raport>
    {
        Task<IServiceOutput<RaportModel>> GetRequestRapor(Guid infoTypeId, string icerik);
        Task<IServiceOutput<List<Raport>>> GetList();
        Task<IServiceOutput<Raport>> GetById(Guid raporId);
    }
}
