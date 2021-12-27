using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB11.Core.Entities
{
    /// <summary>
    /// Thông tin khách hàng:
    /// Create by; NPTAN (27/12/2021)
    /// </summary>
    public class Customer
    {
        #region Property
        /// <summary>
        /// Khóa chính (Id khách hàng)
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ và tên khách hàng
        /// </summary>
        public string Fullname { get; set; }

        /// <summary>
        /// Số điện thoại khách hàng
        /// </summary>
        public string PhoneNumber { get; set; }

        #endregion

    }
}
