using Movit.Application.Entity;
using Movit.Application.IService;
using Movit.Application.Service;
using Movit.Util.WebControl;
using System.Collections.Generic;
using System;
using Movit.Application.Entity.BaseManage.ViewModel;
using Movit.Application.Entity.EcommerceContractManage;

namespace Movit.Application.Busines
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-05-30 13:49
    /// 描 述：Base_ProjectInfo
    /// </summary>
    public class Base_ProjectInfoBLL
    {
        private Base_ProjectInfoIService service = new Base_ProjectInfoService();


        #region 获取数据
          public IEnumerable<Base_ProjectInfoEntity> GetListByCompanyid(string queryJson)
        {
            return service.GetListByCompanyid(queryJson);
        }
        //获取当前用户能看到的项目
        public IEnumerable<Base_ProjectInfoEntity> GetListByAuthorize(string queryJson)
        {
            return service.GetListByAuthorize(queryJson);
        }
        ///<summary>
        ///作者：durry
        ///time：2018-06-22 11:20
        ///获取区域公司下拉
        /// </summary>
        public IEnumerable<Base_ProjectInfoEntity> GetCompanyName(string queryJson)
        {
            return service.GetCompanyName(queryJson);
        }
        /// <summary>
        /// 通过项目名称获得数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<Base_ProjectInfoEntity> GetALL(string projectname)
        {
            return service.GetALL(projectname);
        }
        ///<summary>
        ///作者：clare
        ///time：2018-06-22 11:20
        ///获取电商名字
        /// </summary>
        public IEnumerable<EcommerceProjectRelationEntity> GetEcomGroupNameJson(string queryJson)
        {
            return service.GetEcomGroupNameJson(queryJson);
        }
        ///<summary>
        ///作者：clare
        ///time：2018-06-22 11:20
        ///获取集团名字
        /// </summary>
        public IEnumerable<EcommerceProjectRelationEntity> GetEcomGroupNameByEconmJson(string queryJson)
        {
            return service.GetEcomGroupNameByEconmJson(queryJson);
        }
        ///<summary>
        ///作者：clare
        ///time：2018-06-22 11:20
        ///获取可支配
        /// </summary>
        public IEnumerable<EcommerceProjectRelationEntity> GetMoneyByEconmProjectJson(string queryJson, string queryValue)
        {
            return service.GetMoneyByEconmProjectJson(queryJson,queryValue);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="pagintion">分页</param>>
        /// <returns>返回列表Json</returns>
        public IEnumerable<Base_ProjectInfoEntity> GetPageList(Pagination pagination ,string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Base_ProjectInfoEntity> GetList(string keyValue)
        {
            return service.GetList(keyValue);
        }
        /// <summary>
        /// 获取Sum实际可支配总金额
        /// 作者：durry
        /// time：2018-06-21 19：30
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Project_RelationView GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public Base_ProjectInfoEntity GetEntityBase(string keyValue)
        {
            return service.GetEntityBase(keyValue);
        }
        public Base_ProjectInfoEntity GetAreaName(string keyValue)
        {
            return service.GetAreaName(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }




        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Base_ProjectInfoEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region 同步数据
        /// <summary>
        /// 描述:项目新增
        /// 作者:姚栋
        /// 日期:2018.06.04
        /// </summary>
        public void SyncNewProject()
        {
            try
            {
                service.SyncNewProject();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 描述:项目更新
        /// 作者:姚栋
        /// 日期:2018.06.04
        /// </summary>
        public void SyncUpdateProject()
        {
            try
            {
                service.SyncUpdateProject();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}