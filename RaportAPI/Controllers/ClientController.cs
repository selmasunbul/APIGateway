using Core.Abstract;
using Core.Helpers;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RaporApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        public ClientController(IHttpService httpService, ILogger<ClientController> logger, IMessageService messageService)
        {
            HttpService = httpService;
            Logger = logger;
            MessageService = messageService;
        }
        private readonly IHttpService HttpService;
        private readonly ILogger<ClientController> Logger;
        private readonly IMessageService MessageService;


        [HttpGet]
        [Route("rapor-request")]
        public async Task<IActionResult> GetRaporRequest(Guid iletisimBilgiTipiId, string icerik)
        {
            MessageService.SendMessage("hazırlanıyor");

            ServiceOutput<RaporRequestModel> output = await HttpService.GetRaporRequest(iletisimBilgiTipiId, icerik);
            if (output.Status)
                MessageService.SendMessage("tamamlandı");

            return await ActionOutput<RaporRequestModel>.GenerateAsync(200, true, data: output.Data);
        }



        [HttpGet]
        [Route("get-list-rapor")]
        public async Task<IActionResult> GetList()
        {
            ServiceOutput<List<RaporModel>> output = await HttpService.GetAllRapor();

            return await ActionOutput<List<RaporModel>>.GenerateAsync<List<RaporModel>>(200, true, data: output.Data);
        }



        [HttpGet]
        [Route("get-by-id-rapor")]
        public async Task<IActionResult> GetRapor(Guid raporId)
        {
            ServiceOutput<RaporModel> output = await HttpService.GetRaporDetail(raporId);

            return await ActionOutput<RaporModel>.GenerateAsync<RaporModel>(200, true, data: output.Data);
        }
    }
}
