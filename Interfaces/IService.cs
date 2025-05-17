namespace Interfaces
{
  interface IService<T, U, C>
  {
    protected T[] GetAll();
    protected T? GetOne(string id);
    protected T Update(string id, U dto);
    protected T Create(C dto);
  }
}