using DBModel.Base;
using DBModel.DBmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.Base.Platform
{
    public interface IPlatformService
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        /// 
        List<base_platform> GetPlatformList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg);


        /// <summary>
        /// 修改 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PlatformResult Save(base_platform model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PlatformResult Del(long? id);

    }
}
