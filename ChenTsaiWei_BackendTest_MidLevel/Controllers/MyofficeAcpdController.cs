using Microsoft.AspNetCore.Mvc;
using ChenTsaiWei_BackendTest_MidLevel.Repositories.Interfaces;
using ChenTsaiWei_BackendTest_MidLevel.Models.Requests;


namespace ChenTsaiWei_BackendTest_MidLevel.Controllers {
    [ApiController]
    [Route("api/myofficeacpd")]
    public class MyofficeAcpdController : ControllerBase {
        private readonly IMyofficeAcpdRepository _repository;

        public MyofficeAcpdController(IMyofficeAcpdRepository repository) {
            _repository = repository;
        }

        // 查詢全部使用者資料。
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                var result = await _repository.GetAllAsync();
                return Ok(result);
            }
            catch (Exception) {
                throw;
            }
        }

        // 依照 SID 查詢單筆使用者資料。
        [HttpGet("{sid}")]
        public async Task<IActionResult> GetById(string sid) {
            try {
                var result = await _repository.GetByIdAsync(sid);

                if (result == null) {
                    return NotFound(new {
                        message = $"Data with sid '{sid}' was not found."
                    });
                }

                return Ok(result);
            }
            catch (Exception) {
                throw;
            }
        }


        // 新增使用者資料並回傳新建立的 SID。
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMyofficeAcpdRequest request) {
            try {
                if (request == null) {
                    return BadRequest(new {
                        message = "Request body is required."
                    });
                }

                if (string.IsNullOrWhiteSpace(request.ACPD_LoginID)) {
                    return BadRequest(new {
                        message = "ACPD_LoginID is required."
                    });
                }

                if (string.IsNullOrWhiteSpace(request.ACPD_LoginPWD)) {
                    return BadRequest(new {
                        message = "ACPD_LoginPWD is required."
                    });
                }

                var sid = await _repository.CreateAsync(request);

                return Created($"/api/myofficeacpd/{sid}", new {
                    ACPD_SID = sid
                });
            }
            catch (Exception) {
                throw;
            }
        }


        // 依照 SID 更新使用者資料。
        [HttpPut("{sid}")]
        public async Task<IActionResult> Update(string sid, [FromBody] UpdateMyofficeAcpdRequest request) {
            try {
                if (request == null) {
                    return BadRequest(new {
                        message = "Request body is required."
                    });
                }

                var existing = await _repository.GetByIdAsync(sid);

                if (existing == null) {
                    return NotFound(new {
                        message = $"Data with sid '{sid}' was not found."
                    });
                }

                var affectedRows = await _repository.UpdateAsync(sid, request);

                if (affectedRows <= 0) {
                    return StatusCode(500, new {
                        message = "Update failed."
                    });
                }

                return Ok(new {
                    message = "Update success."
                });
            }
            catch (Exception) {
                throw;
            }
        }


        // 依照 SID 刪除使用者資料。
        [HttpDelete("{sid}")]
        public async Task<IActionResult> Delete(string sid) {
            try {
                var existing = await _repository.GetByIdAsync(sid);

                if (existing == null) {
                    return NotFound(new {
                        message = $"Data with sid '{sid}' was not found."
                    });
                }

                var affectedRows = await _repository.DeleteAsync(sid);

                if (affectedRows <= 0) {
                    return StatusCode(500, new {
                        message = "Delete failed."
                    });
                }

                return NoContent();
            }
            catch (Exception) {
                throw;
            }
        }




    }
}