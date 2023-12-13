using Business.Abstract;
using Business.Concrete;
using Core.Abstract;
using Core.Helpers;
using DataAccess;
using DataAccess.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RehberApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoTypeController : ControllerBase
    {
        public InfoTypeController(IInfoTypeService infoTypeService)
        {
            _infoTypeService = infoTypeService;
        }
        private readonly IInfoTypeService _infoTypeService;


        [HttpGet]
        [Route("get-list")]
        public async Task<IActionResult> GetList()
        {

            IServiceOutput<List<InfoType>> output = await _infoTypeService.GetList();

            return await ActionOutput<InfoType>.GenerateAsync(output);
        }


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] string name)
        {

            IServiceOutput<InfoType> output = await _infoTypeService.CreateAsync(name);

            return await ActionOutput<InfoType>.GenerateAsync<InfoType>(200, true, message: output.Message, data: output.Data);

        }
    }
}
