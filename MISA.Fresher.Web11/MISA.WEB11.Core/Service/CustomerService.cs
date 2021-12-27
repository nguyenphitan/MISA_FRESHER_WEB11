using MISA.WEB11.Core.Entities;
using MISA.WEB11.Core.Exceptions;
using MISA.WEB11.Core.Interface.Infrastructure;
using MISA.WEB11.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB11.Core.Service
{
    public class CustomerService : ICustomerService
    {
        #region Fields
        // Khai báo thông tin lỗi:
        List<string> errorMsgs = new List<string>();
        ICustomerRepository _customerRepository;
        #endregion

        #region Constructor
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        #endregion

        #region Methods
        public int DeleteService(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public int InsertService(Customer customer)
        {
            var isValid = true;

            // Validate dữ liệu:
            // 1. Kiểm tra mã khách hàng có trùng hay không?
            var isDuplicate = _customerRepository.CheckCustomerCodeDuplicate(customer.CustomerCode);
            if (isDuplicate == true)
            {
                errorMsgs.Add("Mã khách hàng không được phép trùng!");
            }

            // 2. Mã khách hàng không được phép để trống:
            //if (string.IsNullOrEmpty(customerCode))
            //{
            //    errorMsgs.Add("Mã khách hàng không được phép để trống!");
            //}

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
                throw new MISAValidateException(result);
            }

            // Thêm mới dữ liệu vào database:
            if (isValid)
            {
                return _customerRepository.Insert(customer);
            }
            else
            {
                // Trả về thông tin lỗi nghiệp vụ:
                throw new MISAValidateException(null);
            }

        }

        public int UpdateService(Customer customer, Guid customerId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
