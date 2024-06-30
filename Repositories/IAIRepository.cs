using ALPHII.Models.Domain;
using ALPHII.Models.DTO;

namespace ALPHII.Repositories
{
    public interface IAIRepository
    {

        Task<VirtualTryOnResponse> VirtualTryOnAsync(VirtualTryOnRequestDto virtualTryOnRequestDto);
        Task GetSegmentAsync(string base_image_base64);

        Task<VirtualModelResponseDto> VirtualModelAsync(VirtualModelRequestDto virtualModelRequestDto);

    }
}
