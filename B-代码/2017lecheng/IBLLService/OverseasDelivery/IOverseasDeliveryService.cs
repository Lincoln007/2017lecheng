using DBModel.OverseasDelivery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.OverseasDelivery
{
    public interface IOverseasDeliveryService
    {
        List<OverseasDeliveryModel> GetOverseasDeliveryList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg,
     int? express_id, DateTime? start_time, DateTime? end_time);

        OverseasDeliveryResult GetSave(Int64? id);
    }
}
