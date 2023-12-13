using Business.Abstract;
using Business.Base;
using Core.Abstract;
using Core.Helpers;
using DataAccess;
using DataAccess.Context;
using DataAccess.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class InfoTypeService : ServiseBase<InfoType, DBContext>, IInfoTypeService
    {
        public async Task<IServiceOutput<List<InfoType>>> GetList()
        {
            var list = await GetAllAsync();

            if (list != null)
            {
                return await ServiceOutput<List<InfoType>>.GenerateAsync(200, true, "Listelendi", 1, list.Count(), data: list.ToList());
            }

            return await ServiceOutput<List<InfoType>>.GenerateAsync(200, false, "Başarısız");

        }
        public async Task<IServiceOutput<InfoType>> CreateAsync(string name)
        {

            var bilgiTipi = new InfoType
            {
                Name = name,
            };

            if (await AddAsync(bilgiTipi) != null)
            {

                return await ServiceOutput<InfoType>.GenerateAsync(200, true, "Başarılı", data: bilgiTipi);
            }

            return await ServiceOutput<InfoType>.GenerateAsync(200, false, "Başarısız", data: bilgiTipi);
        }

    }
}
