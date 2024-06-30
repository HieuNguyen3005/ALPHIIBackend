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
        public LocalAIRepository(IHttpClientFactory httpClientFactory, ALPHIIBackendDbContext dbContext,IImageRepository imageRepository )
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
                   human_base64 = "",
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

        public async Task GetSegmentAsync(string base_image_base64)
        {
            GetSegmentInput getSegmentInput = new GetSegmentInput {
                base_image = base_image_base64
            };

            string jsonBody = JsonConvert.SerializeObject(getSegmentInput);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://5cab-34-19-9-91.ngrok-free.app/segment");
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
               HttpResponseMessage response = await client.SendAsync(request);

               // Xử lý kết quả
               if (response.IsSuccessStatusCode)
               {
                    //string responseBody = await response.Content.ReadAsStringAsync();
                    //var result = JsonConvert.DeserializeObject<List<GetSegmentResponseDto>>(responseBody);
                    //return null;
                }
               else
               {
                   //return null;
               }
            }
        }

        public async Task<VirtualModelResponseDto> VirtualModelAsync(VirtualModelRequestDto virtualModelRequestDto)
        {
            string base_image_base64 = FunctionCommon.ConvertImageToBase64(virtualModelRequestDto.base_image_url);
            string mask_image_base64 = FunctionCommon.ConvertImageToBase64(virtualModelRequestDto.mask_image_url);
            VirtualModelInput virtualModelInput = new VirtualModelInput
            {
                img_base64 = base_image_base64,
                mask_bask64 = mask_image_base64,
                user_prompt = virtualModelRequestDto.user_prompt,
                negative_promt = virtualModelRequestDto.negative_prompt,
                quality = 2,
                n_sample = 2
            };

            string jsonBody = JsonConvert.SerializeObject(virtualModelInput);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://fbc3-34-16-147-21.ngrok-free.app/model_gen/");
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.SendAsync(request);

                // Xử lý kết quả
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadFromJsonAsync<VirtualModelOutput>();
                    // Save image to database

                    string urlImage = await imageRepository.SaveImage(responseBody.results[0]);
                    var virtualModelResponseDto = new VirtualModelResponseDto
                    {
                        status = "200",
                        image_result_url = urlImage
                    };

                    return virtualModelResponseDto;
                }
                else
                {
                    return null;
                }
            }


        }



       
    }
}
