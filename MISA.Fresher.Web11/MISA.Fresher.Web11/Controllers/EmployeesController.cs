using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web11.Model;
using Dapper;
using MySqlConnector;
using System.Data;

namespace MISA.Fresher.Web11.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string? name, int? number)
        {
            // Kết nối với database:
            // 1. Khai áo thông tin database:
            var connectionString = "" +
                "Server=47.241.69.179;" +
                "Port=3306;" +
                "Database=MISA.CukCuk_Demo_NVMANH_copy;" +
                "User Id=dev; " +
                "Password=manhmisa";
            // 2. Khởi tạo kết nối với database:
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);
            
            // 3. Thực hiện lấy dữ liệu trong database:

            // 4. Trả dữ liệu về cho client:
            // - Nếu có dữ liệu thì trả về mã 200 (kèm theo dữ liệu)
            // - Nếu không có dữ liệu thì trả về 204 (No content)
            // - Nếu có lỗi (exception) thì trả về mã 500 (kèm thông tin lỗi)

            return StatusCode(200);
        }


    }
}
