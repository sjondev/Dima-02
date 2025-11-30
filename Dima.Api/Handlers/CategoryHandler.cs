using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
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

            return new Response<Category?>(category, 201 , "Category created");
        }
        catch
        {
            return new Response<Category?>(null, 500, "[FP071] not create category");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");

            category.Title = request.Title;
            category.Description = request.Description;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, message: "Categoria atualizada com sucesso");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Não foi possível alterar a categoria");
        }
    }


    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category == null)
                return new Response<Category?>(null, 404, "Category not found");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(category, message: "Category deleted");
        }
        catch 
        {
            return  new Response<Category?>(null, 500, "[FP084] not delete category");
        }
    }

    public Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
    {
        throw new NotImplementedException();
    }
}