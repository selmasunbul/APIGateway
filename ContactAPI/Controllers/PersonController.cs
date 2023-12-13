using Business.Abstract;
using Core.Abstract;
using Core.Helpers;
using DataAccess;
using DataAccess.ViewModel;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RehberApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        public PersonController(IPersonService personService) 
        {
            _personService = personService;
        }
        private readonly IPersonService _personService;



        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] PersonModel kisi)
        {

            IServiceOutput<Person> output = await _personService.CreateAsync(kisi);

            return await ActionOutput<Person>.GenerateAsync<Person>(200, true, message: output.Message, data: output.Data);

        }

        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetById(Guid kisiId)
        {

            IServiceOutput<Person> output = await _personService.GetById(kisiId);

            return await ActionOutput<Person>.GenerateAsync(output);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromForm] Guid id)
        {

            IServiceOutput<List<Person>> output = await _personService.RemoveAsync(id);

            return await ActionOutput<List<Person>>.GenerateAsync(output);
        }


        [HttpGet]
        [Route("get-list")]
        public async Task<IActionResult> GetList()
        {

            IServiceOutput<List<Person>> output = await _personService.GetList();

            return await ActionOutput<Person>.GenerateAsync(output);
        }


    }
}
