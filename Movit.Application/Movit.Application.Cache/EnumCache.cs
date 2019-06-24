using Movit.Application.Code;
using Movit.Cache.Factory;
using Movit.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Cache
{
    /// <summary> 
    /// 创建人：姚栋 
    /// 日 期：2018.06.20
    /// 描 述：枚举缓存
    public class EnumCache
    { 
        public Dictionary<string, object> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<Dictionary<string, object>>("EnumKey");
            if (cacheList == null)
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                var AuthorizeTypeEnumDes = EnumHelper.ToDescriptionDictionary<AuthorizeTypeEnum>();
                data.Add("AuthorizeTypeEnumArray", AuthorizeTypeEnumDes);
                var AuthorizationMethodEnumDes = EnumHelper.ToDescriptionDictionary<AuthorizationMethodEnum>();
                data.Add("AuthorizationMethodEnumArray", AuthorizationMethodEnumDes);
                var ApprovalStateEnumDes = EnumHelper.ToDescriptionDictionary<ApproveStatus>();
                data.Add("ApprovalStateEnumArray", ApprovalStateEnumDes);
                var BiddingMethodEnumDes = EnumHelper.ToDescriptionDictionary<BiddingMethodEnum>();
                data.Add("BiddingMethodEnumArray", BiddingMethodEnumDes);
                var ProjectTypeEnumDes = EnumHelper.ToDescriptionDictionary<ProjectTypeEnum>();
                data.Add("ProjectTypeEnumArray", ProjectTypeEnumDes);
                CacheFactory.Cache().WriteCache(data, "EnumKey");
                return data;
            }
            else
            {
                return cacheList;
            }
        }
    }
}
