# ChenTsaiWei_BackendTest_MidLevel

## 專案說明
本專案為後端工程師技術測試，使用 .NET 8 Web API 搭配 SQL Server，
針對 MyOffice_ACPD 資料表實作完整 RESTful CRUD 功能。

---

## 使用技術
- .NET 8 Web API
- Dapper
- SQL Server 2019+
- Swagger / OpenAPI

---

## 專案架構

Controllers API 路由控制
Repositories 資料存取邏輯
Models 資料模型（Entities / Requests）
Data 資料庫連線
Middleware 全域錯誤處理
---

## 執行方式

1.還原資料庫或執行 SQL Script  
2.修改  appsettings.json 連線字串

 
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=BackendExamHub;Trusted_Connection=True;TrustServerCertificate=True;"
}


3.使用 Visual Studio 2022 開啟專案
4.按 F5 執行
5.開啟 Swagger：

https://localhost:{port}/swagger
---

#API 清單
Method	URL	說明
GET	/api/myofficeacpd	查詢全部
GET	/api/myofficeacpd/{sid}	查詢單筆
POST	/api/myofficeacpd	新增
PUT	/api/myofficeacpd/{sid}	更新
DELETE	/api/myofficeacpd/{sid}	刪除

---

#測試 JSON

{
  "acpd_Cname": "測試使用者01",
  "acpd_Ename": "Test01",
  "acpd_Sname": "T01",
  "acpd_Email": "test01@email.com",
  "acpd_LoginID": "test01",
  "acpd_LoginPWD": "123456",
  "acpd_Memo": "正常使用者",
  "acpd_NowID": "ADMIN"
},
{
  "acpd_Cname": "更新後使用者",
  "acpd_Ename": "Updated",
  "acpd_Sname": "UPD",
  "acpd_Email": "updated@email.com",
  "acpd_Status": 99,
  "acpd_Stop": true,
  "acpd_StopMemo": "測試停用",
  "acpd_LoginID": "test01",
  "acpd_LoginPWD": "654321",
  "acpd_Memo": "更新測試",
  "acpd_UPDID": "ADMIN02"
}







