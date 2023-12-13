using Core.Abstract;
using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class HttpService: IHttpService
    {
        private readonly string baseURL = "https://localhost:5117/api/";

        public async Task<ServiceOutput<RaporRequestModel>> GetRaporRequest(Guid iletisimBilgiTipiId, string icerik)
        {
            string apiUrl = baseURL + $"Raport/get-request?iletisimBilgiTipiId={iletisimBilgiTipiId}&icerik={icerik}";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                   
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceOutput<RaporRequestModel>>(content);

                        if (result != null )
                        {
                            return result;
                        }
                        else
                        {
                            return new ServiceOutput<RaporRequestModel> { Data = new RaporRequestModel() };
                        }
                    }
                    else
                    {
                        var error = response.StatusCode;
                        return new ServiceOutput<RaporRequestModel> { Data = new RaporRequestModel() };
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    return new ServiceOutput<RaporRequestModel> { Data = new RaporRequestModel() };
                }
            }
        }

        public async Task<ServiceOutput<List<RaporModel>>> GetAllRapor()
        {
            string apiUrl = baseURL + "Raport/get-list";


            using (HttpClient httpClient = new HttpClient())
            {
                try
                {

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceOutput<List<RaporModel>>>(content);
                        if (result != null)
                        {
                            if (result.Data != null && result.Data.Count() > 0)
                            {
                                return result;
                            }

                        }

                        return new ServiceOutput<List<RaporModel>> { Data = new List<RaporModel>() };
                    }
                    else
                    {
                        var error = response.StatusCode;
                        return new ServiceOutput<List<RaporModel>> { Data = new List<RaporModel>() };
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    return new ServiceOutput<List<RaporModel>> { Data = new List<RaporModel>() };
                }
            }
        }


        public async Task<ServiceOutput<RaporModel>> GetRaporDetail(Guid raporId)
        {
            string apiUrl = baseURL + $"Raport/get-by-id?raporId={raporId}";


            using (HttpClient httpClient = new HttpClient())
            {
                try
                {

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceOutput<RaporModel>>(content);
                        if (result != null)
                        {
                            if (result.Data != null )
                            {
                                return result;
                            }

                        }

                        return new ServiceOutput<RaporModel> { Data = new RaporModel() };
                    }
                    else
                    {
                        var error = response.StatusCode;
                        return new ServiceOutput<RaporModel> { Data = new RaporModel() };
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    return new ServiceOutput<RaporModel> { Data = new RaporModel() };
                }
            }
        }
    }
}
