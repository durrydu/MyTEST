using Movit.Application.Entity;
using Movit.Application.Entity.BaseManage.ViewModel;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Util.WebControl;
using System.Collections.Generic;

namespace Movit.Application.IService
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-05-30 13:49
    /// 描 述：Base_ProjectInfo
    /// </summary>
    public interface Base_ProjectInfoIService
    {
        #region 获取数据
        ///<summary>
        ///作者：durry
        ///time：2018-06-22 11:20
        ///获取区域公司下拉
        /// </summary>
        IEnumerable<Base_ProjectInfoEntity> GetCompanyName(string queryJson);
        IEnumerable<Base_ProjectInfoEntity> GetListByCompanyid(string queryJson);
        IEnumerable<Base_ProjectInfoEntity> GetListByAuthorize(string queryJson);
        ///<summary>
        ///作者：clare
        ///time：2018-06-22 11:20
        ///获取电商名字
        /// </summary>
        IEnumerable<EcommerceProjectRelationEntity> GetEcomGroupNameJson(string queryJson);
        ///作者：clare
        ///time：2018-06-22 11:20
        ///获取集团名字
        /// </summary>GetMoneyByEconmProjectJson
        IEnumerable<EcommerceProjectRelationEntity> GetEcomGroupNameByEconmJson(string queryJson);
        ///作者：clare
        ///time：2018-06-22 11:20
        ///获取可支配资金
        /// </summary>
        IEnumerable<EcommerceProjectRelationEntity> GetMoneyByEconmProjectJson(string queryJson, string queryValue);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="pagintion">分页</param>>
        /// <returns>返回列表Json</returns>
        IEnumerable<Base_ProjectInfoEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<Base_ProjectInfoEntity> GetList(string keyValue);
        /// <summary>
        /// 获取Sum实际可支配总金额
        /// 作者：durry
        /// time：2018-06-21 19：30
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
       Project_RelationView GetEntity(string keyValue);
       Base_ProjectInfoEntity GetEntityBase(string keyValue);

       Base_ProjectInfoEntity GetAreaName(string keyValue);
       IEnumerable<Base_ProjectInfoEntity> GetALL(string projectname);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);


        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, Base_ProjectInfoEntity entity);
        #endregion

        #region 同步数据
        /// <summary>
        /// 描述:项目新增
        /// 作者:姚栋
        /// 日期:2018.06.04
        /// </summary>
        void SyncNewProject();

        /// <summary>
        /// 描述:项目更新
        /// 作者:姚栋
        /// 日期:2018.06.04
        /// </summary>
        void SyncUpdateProject();

        #endregion

      
    }
}