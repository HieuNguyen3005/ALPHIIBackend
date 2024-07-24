using ALPHII.Data;
using ALPHII.Models.DTO;
using System.Text;
using ALPHII.Common;
using Newtonsoft.Json;
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

        public async Task<List<GetSegmentResponseDto>> GetSegmentAsync(string base_image_base64)
        {

            List<GetSegmentResponseDto> result = new();
            GetSegmentInput getSegmentInput = new GetSegmentInput {
                base_image = base_image_base64
            };

            string jsonBody = JsonConvert.SerializeObject(getSegmentInput);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://d28b-34-87-16-231.ngrok-free.app/segment");
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
               var response = await client.SendAsync(request);

               // Xử lý kết quả
               if (response.IsSuccessStatusCode)
               {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                     result = JsonConvert.DeserializeObject<List<GetSegmentResponseDto>>(responseBody)?? new();
                }
            }
            return result;
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
            var request = new HttpRequestMessage(HttpMethod.Post, "https://771d-34-125-19-84.ngrok-free.app/model_gen/");
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
