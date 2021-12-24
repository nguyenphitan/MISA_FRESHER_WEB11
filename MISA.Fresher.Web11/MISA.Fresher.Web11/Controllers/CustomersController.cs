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
    public class CustomersController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string? name, int? number)
        {
            try
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
                var sqlCommand = "SELECT * FROM Customer";
                var customers = sqlConnection.Query<object>(sqlCommand);

                // 4. Trả dữ liệu về cho client:
                // - Nếu có dữ liệu thì trả về mã 200 (kèm theo dữ liệu)
                // - Nếu không có dữ liệu thì trả về 204 (No content)
                // - Nếu có lỗi (exception) thì trả về mã 500 (kèm thông tin lỗi)

                return Ok(customers);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vôi long liên hệ MISA để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }
        }

        [HttpGet("{customerId}")]
        public IActionResult GetById(string customerId)
        {
            try
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
                var sqlCommand = $"SELECT * FROM Customer WHERE CustomerId = @CustomerId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId);
                var customer = sqlConnection.QueryFirstOrDefault<object>(sqlCommand, param: parameters);

                // 4. Trả dữ liệu về cho client:
                // - Nếu có dữ liệu thì trả về mã 200 (kèm theo dữ liệu)
                // - Nếu không có dữ liệu thì trả về 204 (No content)
                // - Nếu có lỗi (exception) thì trả về mã 500 (kèm thông tin lỗi)

                return Ok(customer);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vôi long liên hệ MISA để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }
        }

        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            try
            {
                // Khai báo thông tin lỗi:
                List<string> errorMsgs = new List<string>();

                // Sinh id mới cho đối tượng:
                customer.CustomerId = Guid.NewGuid();
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

                // Validate dữ liệu:
                // 1. Kiểm tra mã khách hàng có trùng hay không?
                var customerCode = customer.CustomerCode;
                var sqlCheck = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode";
                DynamicParameters paramCheck = new DynamicParameters();
                paramCheck.Add("@CustomerCode", customerCode);
                var customerCodeDuplicate = sqlConnection.QueryFirstOrDefault<object>(sqlCheck, paramCheck);
                if (customerCodeDuplicate != null)
                {
                    errorMsgs.Add("Mã khách hàng không được phép trùng!");
                }

                // 2. Mã khách hàng không được phép để trống:
                if (string.IsNullOrEmpty(customerCode))
                {
                    errorMsgs.Add("Mã khách hàng không được phép để trống!");
                }

                // 3. Số điện thoại không được phép để trống:
                if (string.IsNullOrEmpty(customer.PhoneNumber))
                {
                    errorMsgs.Add("Số điện thoại không được phép để trống!");
                }

                // 4. Email không đúng định dạng:
                // 5. Ngày sinh không được lớn hơn ngày hiện tại:
                // 6. ...

                // Hiển thị các thông báo lỗi:
                if (errorMsgs.Count > 0)
                {
                    var result = new
                    {
                        userMsg = "Dữ liệu không hợp lệ, vui lòng kiểm tra lại!",
                        data = errorMsgs
                    };
                    return StatusCode(400, result);
                }


                // 3. Thực hiện lấy dữ liệu trong database:
                var sqlCommand = "" +
                    "INSERT Customer (" +
                        "CustomerId, " +
                        "CustomerCode, " +
                        "FullName, " +
                        "PhoneNumber) " +
                    "VALUE(" +
                        "@CustomerId, " +
                        "@CustomerCode, " +
                        "@FullName, " +
                        "@PhoneNumber)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customer.CustomerId);
                parameters.Add("@CustomerCode", customer.CustomerCode);
                parameters.Add("@FullName", customer.Fullname);
                parameters.Add("@PhoneNumber", customer.PhoneNumber);

                var res = sqlConnection.Execute(sqlCommand, param: parameters);

                // 4. Trả dữ liệu về cho client:
                // - Nếu có dữ liệu thì trả về mã 200 (kèm theo dữ liệu)
                // - Nếu không có dữ liệu thì trả về 204 (No content)
                // - Nếu có lỗi (exception) thì trả về mã 500 (kèm thông tin lỗi)

                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }
        }

        [HttpPut("{customerId}")]
        public IActionResult Put(string customerId, [FromBody] Customer customer)
        {
            try
            {
                // Khai báo thông tin lỗi:
                List<string> errorMsgs = new List<string>();

                // Kết nối với database, lấy ra customer có id = customerId
                // 1. Khai áo thông tin database:
                var connectionString = "" +
                    "Server=47.241.69.179;" +
                    "Port=3306;" +
                    "Database=MISA.CukCuk_Demo_NVMANH_copy;" +
                    "User Id=dev; " +
                    "Password=manhmisa";

                // 2. Khởi tạo kết nối với database:
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);

                // Validate dữ liệu:
                // 1. Kiểm tra mã khách hàng có trùng hay không?
                var customerCode = customer.CustomerCode;
                var sqlCheck = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode";
                DynamicParameters paramCheck = new DynamicParameters();
                paramCheck.Add("@CustomerCode", customerCode);
                var customerCodeDuplicate = sqlConnection.QueryFirstOrDefault<object>(sqlCheck, paramCheck);
                if (customerCodeDuplicate != null)
                {
                    errorMsgs.Add("Mã khách hàng không được phép trùng!");
                }

                // 2. Mã khách hàng không được phép để trống:
                if (string.IsNullOrEmpty(customerCode))
                {
                    errorMsgs.Add("Mã khách hàng không được phép để trống!");
                }

                // 3. Số điện thoại không được phép để trống:
                if (string.IsNullOrEmpty(customer.PhoneNumber))
                {
                    errorMsgs.Add("Số điện thoại không được phép để trống!");
                }

                // 4. Email không đúng định dạng:
                // 5. Ngày sinh không được lớn hơn ngày hiện tại:
                // 6. ...

                // Hiển thị các thông báo lỗi:
                if (errorMsgs.Count > 0)
                {
                    var result = new
                    {
                        userMsg = "Dữ liệu không hợp lệ, vui lòng kiểm tra lại!",
                        data = errorMsgs
                    };
                    return StatusCode(400, result);
                }

                // 3. Thực hiện truy vấn, lấy dữ liệu trong database:
                var sqlCommand = "UPDATE Customer SET CustomerCode = @CustomerCode, Fullname = @FullName, PhoneNumber = @PhoneNumber WHERE CustomerId = @CustomerId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId);
                parameters.Add("@CustomerCode", customer.CustomerCode);
                parameters.Add("@Fullname", customer.Fullname);
                parameters.Add("@PhoneNumber", customer.PhoneNumber);
                var res = sqlConnection.Execute(sqlCommand, param: parameters);

                // Trả về dữ liệu cho client:
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vôi long liên hệ MISA để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }

        }

        [HttpDelete("{customerId}")]
        public IActionResult Delete(string customerId)
        {
            // Kết nối với database, lấy ra customer có id = customerId
            // 1. Khai áo thông tin database:
            var connectionString = "" +
                "Server=47.241.69.179;" +
                "Port=3306;" +
                "Database=MISA.CukCuk_Demo_NVMANH_copy;" +
                "User Id=dev; " +
                "Password=manhmisa";

            // 2. khởi tạo kết nối với database:
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            // 3. Thực hiện truy vấn:
            var sqlCommand = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customerId);
            var res = sqlConnection.Execute(sqlCommand, param: parameters);

            // Trả về dữ liệu cho client:
            if (res > 0)
            {
                return StatusCode(201, res);
            }
            else
            {
                return StatusCode(200, res);
            }

        }


    }
}
