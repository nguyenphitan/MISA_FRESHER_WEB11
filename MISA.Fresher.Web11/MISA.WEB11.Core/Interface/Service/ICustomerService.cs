using MISA.WEB11.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB11.Core.Interface.Service
{
    // interface: chứa các chức năng (phương thức)
    public interface ICustomerService
    {
        /// <summary>
        /// Xử lý nghiệp vụ thêm mới dữ liệu
        /// </summary>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <returns>Số bản ghi thêm mới thành công</returns>
        /// CreatedBy: NPTAN (27/12/2021)
        int InsertService(Customer customer);
        int UpdateService(Customer customer, Guid customerId);
        int DeleteService(Guid customerId);
    }
}
