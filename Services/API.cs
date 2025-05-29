using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace SportShop.Services
{
  public class APIService
  {
    private string _databaseUrl = "sportshop-db.cxg486ke2sxx.eu-north-1.rds.amazonaws.com";
    private string _databasePort = "5432";
    private string _databaseName = "sportshop_db";
    private string _databaseUser = "postgres";
    private string _databasePassword = "ZAQ!2wsx";
    private NpgsqlDataSource _dataSource;

    public APIService()
    {
      string connectionString = CreateConnectionString();
      _dataSource = NpgsqlDataSource.Create(connectionString);
    }

    private string CreateConnectionString()
    {
      return $"Host={_databaseUrl};Port={_databasePort};Username={_databaseUser};Password={_databasePassword};Database={_databaseName}";
    }

    public void Dispose()
    {
      _dataSource?.Dispose();
    }

    async public Task<DataTable> GetData(string query)
    {
      await using var connection = await _dataSource.OpenConnectionAsync();
      await using var command = new NpgsqlCommand(query, connection);
      await using var reader = await command.ExecuteReaderAsync();

      var dataTable = new DataTable();
      dataTable.Load(reader);

      return dataTable;
    }
  }
}