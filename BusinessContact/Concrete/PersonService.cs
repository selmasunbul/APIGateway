using Business.Abstract;
using Business.Base;
using Core.Abstract;
using Core.Helpers;
using DataAccess;
using DataAccess.Context;
using DataAccess.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PersonService : ServiseBase<Person, DBContext>, IPersonService
    {
        public async Task<IServiceOutput<List<Person>>> GetList()
        {
            var list = await GetAllAsync();

            if (list != null)
            {
                return await ServiceOutput<List<Person>>.GenerateAsync(200, true, "Listelendi", 1, list.Count(), data: list.ToList());
            }

            return await ServiceOutput<List<Person>>.GenerateAsync(200, false, "Başarısız");

        }


        public async Task<IServiceOutput<Person>> CreateAsync(PersonModel entity)
        {
            var person = new Person
            {
                Name = entity.Name,
                Surname = entity.Surname,
                Firm = entity.Firm,
            };

            if (await AddAsync(person) != null)
            {
                return await ServiceOutput<Person>.GenerateAsync(200, true, "Başarılı", data: person);
            }

            return await ServiceOutput<Person>.GenerateAsync(200, false, "Başarısız", data: person);
        }

        public async Task<IServiceOutput<List<Person>>> RemoveAsync(Guid id)
        {

            bool isItemRemoved = SoftDelete(x => x.Id == id);

            if (isItemRemoved)
            {
                return await ServiceOutput<List<Person>>.GenerateAsync(200, true, "Silindi");
            }
            return await ServiceOutput<List<Person>>.GenerateAsync(200, false, "Başarısız");

        }

        public async Task<IServiceOutput<Person>> GetById(Guid kisiId)
        {
            var kisi = await GetAsync(x => x.Id == kisiId);

            if (kisi != null)
            {
                return await ServiceOutput<Person>.GenerateAsync(200, true, "Başarılı", data: kisi);
            }

            return await ServiceOutput<Person>.GenerateAsync(200, false, "Başarısız");

        }



    }
}
