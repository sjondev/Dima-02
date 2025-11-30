using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category>(category);
        }
        catch (Exception e)
        {
            // estudo mais tarde o que é "serilog"
            Console.WriteLine(e);
            throw new Exception("failed to create category");
        }
    }

    public Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
    {
        throw new NotImplementedException();
    }
}