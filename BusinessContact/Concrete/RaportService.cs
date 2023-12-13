using Business.Abstract;
using Business.Base;
using Core.Abstract;
using Core.Helpers;
using DataAccess;
using DataAccess.Context;
using DataAccess.Entity;
using DataAccess.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RaportService : ServiseBase<Raport, DBContext>, IRaportService
    {
        public RaportService(IComminicationService iletisimService, IInfoTypeService bilgiTipiService)
        {
            IletisimService = iletisimService;
            BilgiTipiService = bilgiTipiService;
        }

        private readonly IComminicationService IletisimService;
        private readonly IInfoTypeService BilgiTipiService;

        public async Task<IServiceOutput<RaportModel>> GetRequestRapor(Guid infoTypeId, string content)
        {
            RaportModel rapors = new();

            var bilgiTipi = await BilgiTipiService.GetAsync(x => x.Id == infoTypeId);
            if (bilgiTipi != null)
            {
                var kisiler = await IletisimService.GetAllAsync(x => x.PersonId != Guid.Empty && x.InfoTypeId == bilgiTipi.Id && x.Content == content);

                rapors.PersonCount = kisiler.Count();
                rapors.RequestDate = DateTime.UtcNow;
                rapors.RaportStatus = "tamamlandı";
                rapors.Location = content;

                var telefonTypeId = await BilgiTipiService.GetAsync(x => x.Name == "telefon");
                if (telefonTypeId != null && kisiler != null)
                {
                    var kisiIdList = kisiler.Select(x => x.PersonId).ToList();
                    var telefonlar = await IletisimService.GetAllAsync(x => kisiIdList.Contains(x.PersonId) && x.InfoTypeId == telefonTypeId.Id);
                    rapors.PhoneNoCount = telefonlar.Count();
                }
            }

            var yeniRapor = new Raport
            {
                Id = new Guid(),
                RaportStatus = "tamamlandı" ,
                PersonCount = rapors.PersonCount,
                PhoneNoCount = rapors.PhoneNoCount,
                CreatedDate = rapors.RequestDate,
                Location = rapors.Location,
                
            };

            if (await AddAsync(yeniRapor) != null)
            {
                return await ServiceOutput<RaportModel>.GenerateAsync(200, true, "Başarılı", data: rapors);
            }
            return await ServiceOutput<RaportModel>.GenerateAsync(200, false, "Başarısız", data: rapors);
        }

        public async Task<IServiceOutput<List<Raport>>> GetList()
        {
            var list = await GetAllAsync();

            if (list != null)
            {
                return await ServiceOutput<List<Raport>>.GenerateAsync(200, true, "Listelendi", 1, list.Count(), data: list.ToList());
            }

            return await ServiceOutput<List<Raport>>.GenerateAsync(200, false, "Başarısız");

        }

        public async Task<IServiceOutput<Raport>> GetById(Guid raporId)
        {
            var raporDetail = await GetAsync(x => x.Id == raporId);

            if (raporDetail != null)
            {
                return await ServiceOutput<Raport>.GenerateAsync(200, true, "Başarılı", data: raporDetail);
            }

            return await ServiceOutput<Raport>.GenerateAsync(200, false, "Başarısız");

        }

    }
}

