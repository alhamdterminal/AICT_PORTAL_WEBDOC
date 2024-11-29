using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface ICreditToCustomerRepository: IRepo<CreditToCustomer>
    {
        CreditToCustomerViewModel GetCreditToCustomer(string GDNumber);

        CreditToCustomer GetcreditToCustomerByInvoiceId(long Id);


    }
}
