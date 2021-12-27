using Dapper;
using MISA.WEB11.Core.Entities;
using MISA.WEB11.Core.Interface.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB11.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public int Delete(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomer()
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
            var customers = sqlConnection.Query<Customer>(sqlCommand);

            return customers;
        }

        public int Insert(Customer customer)
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

            return res;
        }

        public int Update(Customer customer, Guid customerId)
        {
            throw new NotImplementedException();
        }

        public bool CheckCustomerCodeDuplicate(string customerCode)
        {
            // 1. Khai áo thông tin database:
            var connectionString = "" +
                "Server=47.241.69.179;" +
                "Port=3306;" +
                "Database=MISA.CukCuk_Demo_NVMANH_copy;" +
                "User Id=dev; " +
                "Password=manhmisa";
            // 2. Khởi tạo kết nối với database:
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            var sqlCheck = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode";
            DynamicParameters paramCheck = new DynamicParameters();
            paramCheck.Add("@CustomerCode", customerCode);
            var customerCodeDuplicate = sqlConnection.QueryFirstOrDefault<object>(sqlCheck, paramCheck);
            
            if(customerCodeDuplicate != null)
            {
                return true;
            }
            return false;
        }
    }
}
