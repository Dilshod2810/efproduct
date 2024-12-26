using System.Net;
using Domain.Entities;
using Infrastructure.ApiResponse;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class ProductService(DataContext context) : IProductService
{
    public async Task<Response<List<Product>>> GetAll()
    {
        var products = await context.Products.ToListAsync();
        return new Response<List<Product>>(products);
    }

    public async Task<Response<Product>> GetById(int id)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product == null
            ? new Response<Product>(HttpStatusCode.NotFound, "Not Found")
            : new Response<Product>(HttpStatusCode.OK, "Ok");
    }

    public async Task<Response<string>> Create(Product product)
    {
        await context.Products.AddAsync(product);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Response<string>(HttpStatusCode.Created, "Created");
    }

    public async Task<Response<string>> Update(Product product)
    {
        var exisprod = await context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        if (exisprod == null) return new Response<string>(HttpStatusCode.NotFound, "Not Found");
        exisprod.Category = product.Category;
        exisprod.Name=product.Name;
        exisprod.Price=product.Price;
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Server errror")
            : new Response<string>(HttpStatusCode.OK, "Updated");

    }

    public async Task<Response<string>> Delete(int id)
    {
        var exisprod = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (exisprod == null) return new Response<string>(HttpStatusCode.NotFound, "Not Found");
        context.Products.Remove(exisprod);
        var result = await context.SaveChangesAsync();
        return result == 0
           ? new Response<string>(HttpStatusCode.InternalServerError, "Server error")
            : new Response<string>(HttpStatusCode.OK, "Deleted");
    }
}