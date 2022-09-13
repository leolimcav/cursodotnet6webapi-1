using ApiCursoDotnet.Models;

namespace ApiCursoDotnet.Repositories;
public interface ICategoryRepository
{
  Task<Category?> getByIdAsync(int Id);
  Task<Category> addAsync(Category category);
}