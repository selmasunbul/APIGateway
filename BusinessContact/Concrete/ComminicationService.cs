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
    public class ComminicationService : ServiseBase<Comminication, DBContext>, IComminicationService
    {
        public async Task<IServiceOutput<Comminication>> CreateAsync(ComminicationModel entity)
        {

            var comminication = new Comminication
            {
                InfoTypeId = entity.InfoTypeId,
                PersonId = entity.PersonId,
                Content = entity.Content,
            };

            if (await AddAsync(comminication) != null)
            {

                return await ServiceOutput<Comminication>.GenerateAsync(200, true, "Başarılı", data: comminication);
            }

            return await ServiceOutput<Comminication>.GenerateAsync(200, false, "Başarısız", data: comminication);
        }

        public async Task<IServiceOutput<List<Comminication>>> RemoveAsync(Guid id)
        {

            bool isItemRemoved = SoftDelete(x => x.Id == id);

            if (isItemRemoved)
            {
                return await ServiceOutput<List<Comminication>>.GenerateAsync(200, true, "Silindi");
            }
            return await ServiceOutput<List<Comminication>>.GenerateAsync(200, false, "Başarısız");

        }
    }
}
