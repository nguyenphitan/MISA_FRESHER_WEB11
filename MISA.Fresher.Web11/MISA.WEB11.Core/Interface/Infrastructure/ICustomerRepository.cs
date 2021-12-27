using MISA.WEB11.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB11.Core.Interface.Infrastructure
{
    /// <summary>
    /// Interface sử dụng cho khách hàng
    /// CreatedBy: NTTAN (27/12/2021)
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NPTAN (27/12/2021)
        IEnumerable<Customer> GetCustomer();
        int Insert(Customer customer);
        int Update(Customer customer, Guid customerId);
        int Delete(Guid customerId);

        /// <summary>
        /// Kiểm tra mã khách hàng đã có hay chưa
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns>true - đã tồn tại; false - không trùng</returns>
        /// CreatedBy: NPTAN (27/12/2021)
        bool CheckCustomerCodeDuplicate(string customerCode);
    }
}
