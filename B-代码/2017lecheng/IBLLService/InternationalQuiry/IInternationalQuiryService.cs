using DBModel.InternationalQuiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.InternationalQuiry
{
    public interface IInternationalQuiryService
    {
        InternationalQuiryResult GetExpressList();

        List<InternationalQuiryModel> GetInternationalQuiryList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg,
       int? express_id, string express_code);


    }
}
