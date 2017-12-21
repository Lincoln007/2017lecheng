using DBModel.LogisticsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.LogisticsQuery
{
    public interface ILogisticsQueryService
    {
        LogisticsQueryResult GetExpressList();
    }
}
