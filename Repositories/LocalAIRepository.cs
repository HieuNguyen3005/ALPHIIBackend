using ALPHII.Data;
using ALPHII.Models.Domain;
using ALPHII.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using ALPHII.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ALPHII.Common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ALPHII.Data;
using ALPHII.Repositories;
namespace ALPHII.Repositories
{
    public class LocalAIRepository : IAIRepository
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ALPHIIBackendDbContext _dbContext;
        private readonly IImageRepository imageRepository;
        public AIController(IHttpClientFactory httpClientFactory, ALPHIIBackendDbContext dbContext,IImageRepository imageRepository )
        {
            this.httpClientFactory = httpClientFactory;
            this._dbContext = dbContext;
            this.imageRepository = imageRepository;
        }

        public async Task<VirtualTryOnResponse> VirtualTryOnAsync(VirtualTryOnRequestDto virtualTryOnRequestDto)
        {
            var cloth_base64 = FunctionCommon.ConvertImageToBase64(virtualTryOnRequestDto.cloth_image);
            VirtualTryOnInput virtualTryOnInput = new VirtualTryOnInput
            {
               input = new Input
               {
                   model_type = "hd",
                   n_steps = 20,
                   image_scale = 3,
                   human_base64 = human_base64,
                   cloth_base64 = cloth_base64,
                   //human_base64 = "",
                   n_samples = 1,
                   task_category = 0
               }
            };
            string jsonBody = JsonConvert.SerializeObject(virtualTryOnInput);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.runpod.ai/v2/hkr2lke3wootls/runsync");
            request.Headers.Add("Authorization", "TRA0VI26UGLVMU6TWCQR09WZCL27512BB6KQE0DP");
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
               HttpResponseMessage response = await client.SendAsync(request);

               // Xử lý kết quả
               if (response.IsSuccessStatusCode)
               {
                   response.EnsureSuccessStatusCode();
                   var responseBody = await response.Content.ReadFromJsonAsync<VirtualTryOnOutputDto>();
                   // Save image to database
                   string urlImage = await imageRepository.SaveImage(responseBody.output.results[0]);
                   var responseVirtualTryon = new VirtualTryOnResponse
                   {
                       code_status = responseBody.status,
                       result = urlImage
                   };

                   return responseVirtualTryon;
               }
               else
               {

                   return null;
               }
            }
        }

        public async Task<List<GetSegmentResponseDto>> GetSegmentAsync(string base_image_base64)
        {
            GetSegmentInput getSegmentInput = new GetSegmentInput {
                base_image = base_image_base64
            };

            string jsonBody = JsonConvert.SerializeObject(getSegmentInput);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://f5e1-34-105-62-129.ngrok-free.app/segment");
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
               HttpResponseMessage response = await client.SendAsync(request);

               // Xử lý kết quả
               if (response.IsSuccessStatusCode)
               {
                   response.EnsureSuccessStatusCode();
                   var responseBody = await response.Content.ReadFromJsonAsync<List<GetSegmentResponseDto>();
                   return responseBody;
               }
               else
               {
                   return null;
               }
            }
        }

        public async Task<VirtualModelResponseDto> VirtualModelAsync(VirtualModelRequestDto virtualModelRequestDto)
        {
            
        }



       
    }
}
