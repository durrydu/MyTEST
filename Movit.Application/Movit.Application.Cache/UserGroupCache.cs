﻿using Movit.Application.Busines.BaseManage;
using Movit.Application.Entity.BaseManage;
using Movit.Cache.Factory;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Movit.Application.Cache
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2016.3.4 9:56
    /// 描 述：用户组信息缓存
    /// </summary>
    public class UserGroupCache
    {
        private UserGroupBLL busines = new UserGroupBLL();

        /// <summary>
        /// 用户组列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<RoleEntity>>(busines.cacheKey);
            if (cacheList == null)
            {
                var data = busines.GetList();
                CacheFactory.Cache().WriteCache(data, busines.cacheKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }
        /// <summary>
        /// 用户组列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList(string organizeId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(organizeId))
            {
                data = data.Where(t => t.OrganizeId == organizeId);
            }
            return data;
        }
    }
}
