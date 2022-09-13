using ApiCursoDotnet.Data;
using ApiCursoDotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCursoDotnet.Repositories;

public sealed class ProductRepository : IProductRepository
{
  private readonly ApplicationDbContext dbContext;
  private readonly ICategoryRepository categoryRepository;

  public ProductRepository(ApplicationDbContext dbContext, ICategoryRepository categoryRepository)
  {
    this.dbContext = dbContext;
    this.categoryRepository = categoryRepository;
  }

  public async Task<Product> addAsync(ProductRequest productRequest) {
    var category = await this.categoryRepository.getByIdAsync(productRequest.CategoryId);

    if(category is null) {
      throw new Exception("Category does not exists!");
    }

    var product = new Product {
      Code = productRequest.Code,
      Name = productRequest.Name,
      Description = productRequest.Description,
      Category = category,
    };

    if(productRequest.Tags is not null) {
      product.Tags = new List<Tag>();

      foreach (var item in productRequest.Tags)
      {
        product.Tags.Add(new Tag { Name = item });
      }
    }

    var createdProduct = await this.dbContext.AddAsync<Product>(product);
    await this.dbContext.SaveChangesAsync();

    return createdProduct.Entity;
  }

  public async Task<Product?> getByIdAsync(int id)
  {
    return await this.dbContext.Products!.Include(p => p.Category).Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
  }

  public async Task updateAsync(int id, ProductRequest productRequest)
  {
    var product = await this.dbContext.Products!.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
    var category = await this.categoryRepository.getByIdAsync(productRequest.CategoryId);

    if(product is null) {
      throw new Exception("Product does not exists!");
    }

    if(category is null) {
      throw new Exception("Category does not exists!");
    }

    product.Code = productRequest.Code;
    product.Description = productRequest.Description;
    product.Name = productRequest.Name;
    product.Category = category;
    
    if(productRequest.Tags is not null) {
      product.Tags = new List<Tag>();
      foreach (var item in productRequest.Tags)
      {
        product.Tags.Add(new Tag { Name = item });
      }
    }

    await this.dbContext.SaveChangesAsync();
  }

  public async Task deleteAsync(int id) {
    var product = await this.dbContext.Products!.FirstOrDefaultAsync(p => p.Id == id);

    if(product is null) {
      throw new Exception("Product does not exists!");
    }

    this.dbContext.Remove<Product>(product);
    await this.dbContext.SaveChangesAsync();
  }
}