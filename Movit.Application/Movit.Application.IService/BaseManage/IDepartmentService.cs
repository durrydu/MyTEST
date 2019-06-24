using Movit.Application.Entity.BaseManage;
using System.Collections.Generic;

namespace Movit.Application.IService.BaseManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.11.02 14:27
    /// 描 述：部门管理
    /// </summary>
    public interface IDepartmentService
    {
        #region 获取数据
        /// <summary>
        /// 获取当前用户可以看到的区域公司列表
        /// 作者：姚栋
        /// 日期：20180717
        /// </summary>
        /// <returns></returns>
        IEnumerable<DepartmentEntity> GetListByAuthorize();
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<DepartmentEntity> GetList();
        IEnumerable<DepartmentEntity> GetListHasUser();
        /// <summary>
        /// 部门实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        DepartmentEntity GetEntity(string keyValue);
        #endregion

        #region 验证数据
        /// <summary>
        /// 部门编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistEnCode(string enCode, string keyValue);
        /// <summary>
        /// 部门名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistFullName(string fullName, string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存部门表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">机构实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, DepartmentEntity departmentEntity);
        #endregion
        #region 同步数据
        /// <summary>
        /// 描述:部门新增
        /// 作者:姚栋
        /// 日期:2018.06.05
        /// </summary>
        void SyncNewDep();

        /// <summary>
        /// 描述:部门更新
        /// 作者:姚栋
        /// 日期:2018.06.05
        /// </summary>
        void SyncUpdateDep();
        #endregion
    }
}
