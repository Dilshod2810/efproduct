using Domain.Entities;
using Infrastructure.ApiResponse;

namespace Infrastructure.Service;

public interface IProductService
{
    Task<Response<List<Product>>> GetAll();
    Task<Response<Product>> GetById(int id);
    Task<Response<string>> Create(Product product);
    Task<Response<string>> Update(Product product);
    Task<Response<string>> Delete(int id);
}