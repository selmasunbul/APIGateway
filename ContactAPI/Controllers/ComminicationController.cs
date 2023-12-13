using Business.Abstract;
using Business.Concrete;
using Core.Abstract;
using Core.Helpers;
using DataAccess.ViewModel;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RehberApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComminicationController : ControllerBase
    {
        public ComminicationController(IComminicationService comminicationService)
        {
            _comminicationService = comminicationService;
        }

        private readonly IComminicationService _comminicationService;


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] ComminicationModel kisi)
        {

            IServiceOutput<Comminication> output = await _comminicationService.CreateAsync(kisi);

            return await ActionOutput<Comminication>.GenerateAsync<Comminication>(200, true, message: output.Message, data: output.Data);

        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromForm] Guid id)
        {

            IServiceOutput<List<Comminication>> output = await _comminicationService.RemoveAsync(id);

            return await ActionOutput<List<Comminication>>.GenerateAsync(output);
        }

    }
}
