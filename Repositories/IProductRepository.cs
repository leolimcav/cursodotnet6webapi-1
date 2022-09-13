using ApiCursoDotnet.Models;

namespace ApiCursoDotnet.Repositories;
public interface IProductRepository
{
  Task<Product?> getByIdAsync(int id);
  Task<Product> addAsync(ProductRequest productRequest);
  Task updateAsync(int id, ProductRequest productRequest);
  Task deleteAsync(int id);
}