using Domain.Entities;
using Infrastructure.ApiResponse;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService)
{
    [HttpGet("GetAllProducts")]
    public async Task<Response<List<Product>>> GetAll()
    {
        return await productService.GetAll();
    }

    [HttpGet("GetById")]
    public async Task<Response<Product>> GetById(int id)
    {
        return await productService.GetById(id);
    }

    [HttpPost("CreateProduct")]
    public async Task<Response<string>> Create(Product product)
    {
        return await productService.Create(product);
    }
    
    [HttpPut("UpdateProduct")]
    public async Task<Response<string>> Update(Product product)
    {
        return await productService.Update(product);
    }

    [HttpDelete("Delete")]
    public async Task<Response<string>> Delete(int id)
    {
        return await productService.Delete(id);
    }

}