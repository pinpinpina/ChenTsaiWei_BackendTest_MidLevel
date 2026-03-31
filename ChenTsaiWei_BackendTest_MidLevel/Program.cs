using ChenTsaiWei_BackendTest_MidLevel.Data;
using ChenTsaiWei_BackendTest_MidLevel.Repositories;
using ChenTsaiWei_BackendTest_MidLevel.Repositories.Interfaces;
using Microsoft.OpenApi;
using ChenTsaiWei_BackendTest_MidLevel.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 註冊 Controller 與 Swagger，提供 API 路由與測試介面。
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 設定 Swagger 文件資訊，明確指定 OpenAPI 文件版本與標題。
builder.Services.AddSwaggerGen(options => {
    try {
        options.SwaggerDoc("v1", new OpenApiInfo {
            Title = "ChenTsaiWei Backend Test API",
            Version = "v1"
        });
    }
    catch (Exception) {
        throw;
    }
});

// 註冊資料庫連線工廠與 Repository，提供資料存取功能。
builder.Services.AddScoped<SqlConnectionFactory>();
builder.Services.AddScoped<IMyofficeAcpdRepository, MyofficeAcpdRepository>();

var app = builder.Build();

// 啟用全域例外處理 Middleware，統一處理未預期錯誤。
app.UseMiddleware<ExceptionMiddleware>();

// 在開發環境啟用 Swagger，方便直接測試 API。
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        try {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "ChenTsaiWei Backend Test API v1");
        }
        catch (Exception) {
            throw;
        }
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();