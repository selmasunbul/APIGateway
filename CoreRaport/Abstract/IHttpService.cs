using Core.Helpers;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract
{
    public interface IHttpService
    {
        Task<ServiceOutput<RaporRequestModel>> GetRaporRequest(Guid iletisimBilgiTipiId, string icerik);
        Task<ServiceOutput<List<RaporModel>>> GetAllRapor();
        Task<ServiceOutput<RaporModel>> GetRaporDetail(Guid raporId);
    }
}
