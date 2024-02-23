using FrontendService.Application.Models;
using FrontendService.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Mime;
using System.Text;
using System;
using FrontendService.Helpers;

namespace FrontendService.Application.Service
{
    public class PlaceServices(IHttpClientFactory httpClientFactory,
        IOptions<ApiUrlSetting> setting) : IPlaceServices
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient(nameof(PlaceServices));


        public async Task<List<PlaceReadDto>> GetAllPlaceAsync(CancellationToken cancellationToken)
        {
            //Define url
            var uri = new Uri(setting.Value.BaseUrl + "Places");
            //Request
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri,
            };

            var response = await httpClient.SendAsync(request, cancellationToken);
            var responseAsString = await response.Content.ReadAsStringAsync(cancellationToken);

            var returnData = JsonConvert.DeserializeObject<List<PlaceReadDto>>(responseAsString)!;

            return returnData;
        }

        public async Task<PlaceReadDto> GetSinglePlaceAsync(Guid id, CancellationToken cancellationToken)
        {
            //Define url
            var uri = new Uri(setting.Value.BaseUrl + "Places/" + id);
            //Request
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri,
            };

            var response = await httpClient.SendAsync(request, cancellationToken);
            var responseAsString = await response.Content.ReadAsStringAsync(cancellationToken);

            var returnData = JsonConvert.DeserializeObject<PlaceReadDto>(responseAsString)!;

            return returnData;
        }

        public async Task<ResponseBaseModel> PostPlaceAsync(PlaceWriteDto placeReadDto, CancellationToken cancellationToken)
        {
            var returnResponse = new ResponseBaseModel();
            //Define url
            var url = new Uri(setting.Value.BaseUrl + "Places");
            //Set Body
            var bodyAsString = JsonConvert.SerializeObject(placeReadDto, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            //Request to server
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = url,
                Content = new StringContent(bodyAsString, Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            var response = await httpClient.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                returnResponse.IsError = false;
            } else
            {
                returnResponse.IsError = true;
            }

            return returnResponse;
        }

        public async Task<ResponseBaseModel> UpdatePlaceAsync(Guid id, PlaceWriteDto placeReadDto, CancellationToken cancellationToken)
        {
            var returnResponse = new ResponseBaseModel();
            //Define url
            var url = new Uri(setting.Value.BaseUrl + "Places/" + id);
            //Set Body
            var bodyAsString = JsonConvert.SerializeObject(placeReadDto, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            //Request to server
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = url,
                Content = new StringContent(bodyAsString, Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            var response = await httpClient.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                returnResponse.IsError = false;
            }
            else
            {
                returnResponse.IsError = true;
            }

            return returnResponse;
        }

        public async Task<ResponseBaseModel> DeletePlaceAsync(Guid id, CancellationToken cancellationToken)
        {
            var returnResponse = new ResponseBaseModel();
            //Define url
            var url = new Uri(setting.Value.BaseUrl + "Places/" + id);
            //Set Body

            //Request to server
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = url,
            };

            var response = await httpClient.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                returnResponse.IsError = false;
            }
            else
            {
                returnResponse.IsError = true;
            }

            return returnResponse;
        }
    }
}
