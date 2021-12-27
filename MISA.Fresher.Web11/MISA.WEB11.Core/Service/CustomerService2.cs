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
    public class CustomerService2 : ICustomerService
    {
        #region Fields
        ICustomerRepository _customerRepository;
        #endregion

        #region Constructor

        #endregion

        #region Methods
        public int DeleteService(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public int InsertService(Customer customer)
        {
            return 1;

        }

        public int UpdateService(Customer customer, Guid customerId)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
