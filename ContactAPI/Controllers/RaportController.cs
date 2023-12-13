using Business.Abstract;
using Business.Concrete;
using Core.Abstract;
using Core.Helpers;
using DataAccess;
using DataAccess.Entity;
using DataAccess.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RehberApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaportController : ControllerBase
    {
        public RaportController(IRaportService raportService)
        {
            _raportService = raportService;
        }

        private readonly IRaportService _raportService;

        [HttpGet]
        [Route("get-request")]
        public async Task<IActionResult> GetRequest(Guid iletisimBilgiTipiId, string icerik)
        {

            IServiceOutput<RaportModel> output = await _raportService.GetRequestRapor(iletisimBilgiTipiId, icerik);

            return await ActionOutput<RaportModel>.GenerateAsync(output);
        }

        [HttpGet]
        [Route("get-list")]
        public async Task<IActionResult> GetListRapor()
        {


            IServiceOutput<List<Raport>> output = await _raportService.GetList();

            return await ActionOutput<List<Raport>>.GenerateAsync(output);
        }


        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetById(Guid raporId)
        {

            IServiceOutput<Raport> output = await _raportService.GetById(raporId);

            return await ActionOutput<Raport>.GenerateAsync(output);
        }


    }
}
