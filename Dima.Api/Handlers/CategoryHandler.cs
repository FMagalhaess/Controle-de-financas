using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests;
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
            
            return new Response<Category?>(category, 201, "Categoria criada com sucesso");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Erro ao criar categoria.");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x=> x.Id == request.Id && x.UserId == request.UserId);
            if (category is null)
            {
                return new Response<Category?>(null, 404, "Categoria não encontrada");
            }
            category.Title = request.Title;
            category.Description = request.Description;
            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(category);
        }
        catch
        {
            return new Response<Category?>(null, 500, "Erro ao atualizar categoria.");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x=> x.Id == request.Id && x.UserId == request.UserId);
            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(category, message:"Categoria excluida com sucesso");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Erro ao excluir categoria.");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x=> x.Id == request.Id && x.UserId == request.UserId);
            return category is null ? new Response<Category?>(null, 404, "Categoria não encontrada") : new Response<Category?>(category);
        }
        catch
        {
            return new Response<Category?>(null, 500, "Não foi possivel encontrar uma categoria.");
        }
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
    {
        try
        {
            var query = context
                .Categories
                .AsNoTracking()
                .Where(x=> x.UserId == request.UserId);
            
            var categories = await query
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query
                .CountAsync();
            return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);

        }
        catch
        {
            return new PagedResponse<List<Category>>(null, 500, "Erro ao obter os categorias");
        }
    }
}