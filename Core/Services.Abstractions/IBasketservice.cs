using Shared;

namespace Services.Abstractions
{
    public interface IBasketservice
    {
        Task<BasketDto> GetbasketAsync(string id);
        Task<BasketDto> UpdatebasketAsync(BasketDto basketDto);
        Task<bool> DeletbasketAsync(string id);
         

    }
}