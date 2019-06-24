using Movit.Application.Entity.BaseManage;
using Movit.Application.IService.BaseManage;
using Movit.Application.Service.BaseManage;
using System;
using System.Linq;
using System.Collections.Generic;
using Movit.Cache.Factory;

namespace Movit.Application.Busines.BaseManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.11.02 14:27
    /// 描 述：部门管理
    /// </summary>
    public class DepartmentBLL
    {
        private IDepartmentService service = new DepartmentService();
        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "DepartmentCache";

        #region 获取数据
        /// <summary>
        /// 获取当前用户可以看到的区域公司列表
        /// 作者：姚栋
        /// 日期：20180717
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetListByAuthorize()
        {
            return service.GetListByAuthorize();
        }
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 获取有用户的部门信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetListHasUser()
        {
            return service.GetListHasUser();
        }
        /// <summary>
        /// 部门实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DepartmentEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 部门编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            return service.ExistEnCode(enCode, keyValue);
        }
        /// <summary>
        /// 部门名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            return service.ExistFullName(fullName, keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存部门表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">部门实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DepartmentEntity departmentEntity)
        {
            try
            {
                service.SaveForm(keyValue, departmentEntity);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region 同步数据
        /// <summary>
        /// 描述:部门新增
        /// 作者:姚栋
        /// 日期:2018.06.05
        /// </summary>
        public void SyncNewDep()
        {
            service.SyncNewDep();
        }

        /// <summary>
        /// 描述:部门更新
        /// 作者:姚栋
        /// 日期:2018.06.05
        /// </summary>
        public void SyncUpdateDep()
        {
            service.SyncUpdateDep();
        }
        #endregion
    }
}
