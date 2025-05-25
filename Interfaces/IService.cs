using System.Data;
using System.Threading.Tasks;

namespace Interfaces
{
  interface IService<T, U, C>
  {
    protected Task<T[]> GetAll();
    protected Task<T?> GetOne(string id);
    protected Task<T> Update(string id, U dto);
    protected Task<T> Create(C dto);
    protected void Delete(string id);
    protected T MapDataRow(DataRow dataRow);
    protected T[] MapData(DataTable data);
  }
}