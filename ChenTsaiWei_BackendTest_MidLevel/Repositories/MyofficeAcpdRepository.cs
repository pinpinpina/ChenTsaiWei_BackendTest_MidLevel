using Dapper;
using ChenTsaiWei_BackendTest_MidLevel.Data;
using ChenTsaiWei_BackendTest_MidLevel.Models.Entities;
using ChenTsaiWei_BackendTest_MidLevel.Repositories.Interfaces;
using ChenTsaiWei_BackendTest_MidLevel.Models.Requests;
using Dapper;
using System.Data;


namespace ChenTsaiWei_BackendTest_MidLevel.Repositories {
    public class MyofficeAcpdRepository : IMyofficeAcpdRepository {
        private readonly SqlConnectionFactory _connectionFactory;


        public MyofficeAcpdRepository(SqlConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }


        // 查詢 MyOffice_ACPD 全部資料，提供列表顯示。
        public async Task<IEnumerable<MyofficeAcpd>> GetAllAsync() {
            try {
                using var connection = _connectionFactory.CreateConnection();

                var sql = @"
SELECT
    ACPD_SID,
    ACPD_Cname,
    ACPD_Ename,
    ACPD_Sname,
    ACPD_Email,
    ACPD_Status,
    ACPD_Stop,
    ACPD_StopMemo,
    ACPD_LoginID,
    ACPD_LoginPWD,
    ACPD_Memo,
    ACPD_NowDateTime,
    ACPD_NowID,
    ACPD_UPDDateTime,
    ACPD_UPDID
FROM dbo.MyOffice_ACPD
ORDER BY ACPD_NowDateTime DESC;";

                var result = await connection.QueryAsync<MyofficeAcpd>(sql);
                return result;
            }
            catch (Exception) {
                throw;
            }
        }


        // 依照 SID 查詢單筆使用者資料。
        public async Task<MyofficeAcpd?> GetByIdAsync(string sid) {
            try {
                using var connection = _connectionFactory.CreateConnection();

                var sql = @"
SELECT
    ACPD_SID,
    ACPD_Cname,
    ACPD_Ename,
    ACPD_Sname,
    ACPD_Email,
    ACPD_Status,
    ACPD_Stop,
    ACPD_StopMemo,
    ACPD_LoginID,
    ACPD_LoginPWD,
    ACPD_Memo,
    ACPD_NowDateTime,
    ACPD_NowID,
    ACPD_UPDDateTime,
    ACPD_UPDID
FROM dbo.MyOffice_ACPD
WHERE ACPD_SID = @ACPD_SID;";

                var result = await connection.QueryFirstOrDefaultAsync<MyofficeAcpd>(sql, new {
                    ACPD_SID = sid
                });

                return result;
            }
            catch (Exception) {
                throw;
            }
        }


        // 新增一筆資料並透過 NEWSID 預存程序產生唯一主鍵。
        public async Task<string> CreateAsync(CreateMyofficeAcpdRequest request) {
            try {
                using var connection = _connectionFactory.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("@TableName", "MyOffice_ACPD");
                parameters.Add("@ReturnSID", dbType: DbType.String, size: 20, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("NEWSID", parameters, commandType: CommandType.StoredProcedure);

                var sid = parameters.Get<string>("@ReturnSID");

                if (string.IsNullOrWhiteSpace(sid)) {
                    throw new Exception("NEWSID did not return a valid SID.");
                }

                var sql = @"
INSERT INTO dbo.MyOffice_ACPD
(
    ACPD_SID,
    ACPD_Cname,
    ACPD_Ename,
    ACPD_Sname,
    ACPD_Email,
    ACPD_LoginID,
    ACPD_LoginPWD,
    ACPD_Memo,
    ACPD_NowID
)
VALUES
(
    @ACPD_SID,
    @ACPD_Cname,
    @ACPD_Ename,
    @ACPD_Sname,
    @ACPD_Email,
    @ACPD_LoginID,
    @ACPD_LoginPWD,
    @ACPD_Memo,
    @ACPD_NowID
);";

                await connection.ExecuteAsync(sql, new {
                    ACPD_SID = sid,
                    request.ACPD_Cname,
                    request.ACPD_Ename,
                    request.ACPD_Sname,
                    request.ACPD_Email,
                    request.ACPD_LoginID,
                    request.ACPD_LoginPWD,
                    request.ACPD_Memo,
                    request.ACPD_NowID
                });

                return sid;
            }
            catch (Exception) {
                throw;
            }
        }


        // 依照 SID 更新一筆使用者資料，並回傳受影響筆數。
        public async Task<int> UpdateAsync(string sid, UpdateMyofficeAcpdRequest request) {
            try {
                using var connection = _connectionFactory.CreateConnection();

                var sql = @"
UPDATE dbo.MyOffice_ACPD
SET
    ACPD_Cname = @ACPD_Cname,
    ACPD_Ename = @ACPD_Ename,
    ACPD_Sname = @ACPD_Sname,
    ACPD_Email = @ACPD_Email,
    ACPD_Status = @ACPD_Status,
    ACPD_Stop = @ACPD_Stop,
    ACPD_StopMemo = @ACPD_StopMemo,
    ACPD_LoginID = @ACPD_LoginID,
    ACPD_LoginPWD = @ACPD_LoginPWD,
    ACPD_Memo = @ACPD_Memo,
    ACPD_UPDDateTime = GETDATE(),
    ACPD_UPDID = @ACPD_UPDID
WHERE ACPD_SID = @ACPD_SID;";

                var affectedRows = await connection.ExecuteAsync(sql, new {
                    ACPD_SID = sid,
                    request.ACPD_Cname,
                    request.ACPD_Ename,
                    request.ACPD_Sname,
                    request.ACPD_Email,
                    request.ACPD_Status,
                    request.ACPD_Stop,
                    request.ACPD_StopMemo,
                    request.ACPD_LoginID,
                    request.ACPD_LoginPWD,
                    request.ACPD_Memo,
                    request.ACPD_UPDID
                });

                return affectedRows;
            }
            catch (Exception) {
                throw;
            }
        }


        // 依照 SID 刪除一筆使用者資料，並回傳受影響筆數。
        public async Task<int> DeleteAsync(string sid) {
            try {
                using var connection = _connectionFactory.CreateConnection();

                var sql = @"
DELETE FROM dbo.MyOffice_ACPD
WHERE ACPD_SID = @ACPD_SID;";

                var affectedRows = await connection.ExecuteAsync(sql, new {
                    ACPD_SID = sid
                });

                return affectedRows;
            }
            catch (Exception) {
                throw;
            }
        }




    }
}