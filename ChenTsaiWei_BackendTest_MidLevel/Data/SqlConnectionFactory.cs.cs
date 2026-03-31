using Microsoft.Data.SqlClient;

namespace ChenTsaiWei_BackendTest_MidLevel.Data {
    public class SqlConnectionFactory {
        private readonly IConfiguration _configuration;

        public SqlConnectionFactory(IConfiguration configuration) {
            _configuration = configuration;
        }

        // 建立並回傳 SQL Server 連線物件，提供 Repository 使用。
        public SqlConnection CreateConnection() {
            try {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrWhiteSpace(connectionString)) {
                    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
                }

                return new SqlConnection(connectionString);
            }
            catch (Exception) {
                throw;
            }
        }
    }
}