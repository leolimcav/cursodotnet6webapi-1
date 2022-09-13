using ApiCursoDotnet.Data;
using ApiCursoDotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCursoDotnet.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
  private readonly ApplicationDbContext dbContext;

  public CategoryRepository(ApplicationDbContext dbContext)
  {
    this.dbContext = dbContext;
  }

  public async Task<Category> addAsync(Category category)
  {
    var createdCategory = await this.dbContext.AddAsync<Category>(category);
    await this.dbContext.SaveChangesAsync();

    return createdCategory.Entity;
  }

  public async Task<Category?> getByIdAsync(int Id)
  {
    var category = await this.dbContext.Categories!.FirstOrDefaultAsync(c => c.Id == Id);
    return category;
  }
}