using Movit.Application.Code;
using Movit.Application.Entity;
using Movit.Application.Entity.AuthorizeManage;
using Movit.Application.Entity.AuthorizeManage.ViewModel;
using Movit.Application.Entity.BaseManage;
using Movit.Util.WebControl;
using System.Collections.Generic;

namespace Movit.Application.IService.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.12.5 22:35
    /// 描 述：授权认证
    /// </summary>
    public interface IAuthorizeService
    {
        /// <summary>
        /// 批量删除岗位项目授权数据
        /// </summary>
        /// <param name="authorizeIds">授权ID主键集合</param>
        void BatchRemoveForm(string[] authorizeIds);

        /// <summary>
        /// 移除对应岗位项目授权数据
        /// </summary>
        /// <param name="postId">岗位ID</param>
        void BatchRemoveFormAll(string postId);
        /// <summary>
        /// 描述:根据岗位ID获取已经授权过的项目信息
        /// 作者:姚栋
        /// 日期:2018-05-30
        /// </summary>
        /// <param name="keyValue">角色ID</param>
        IEnumerable<AuthorizeEntity> GetProjectListByPostId(string keyValue, string queryJson, Pagination pagination);
        /// <summary>
        /// 描述:根据岗位ID获取未授权过的项目信息
        /// 作者:姚栋
        /// 日期:2018-05-30
        /// </summary>
        /// <param name="keyValue">角色ID</param>
        IEnumerable<Base_ProjectInfoEntity> GetProjectExcept(string keyValue, Pagination pagination, string queryJson);

        /// <summary>
        /// 获取授权功能
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<ModuleEntity> GetModuleList(string userId);
        /// <summary>
        /// 获取授权功能按钮
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<ModuleButtonEntity> GetModuleButtonList(string userId);
        /// <summary>
        /// 获取授权功能视图
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<ModuleColumnEntity> GetModuleColumnList(string userId);
        /// <summary>
        /// 获取授权功能Url、操作Url
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<AuthorizeUrlModel> GetUrlList(string userId);
        /// <summary>
        /// 获取关联用户关系
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<UserRelationEntity> GetUserRelationList(string userId);
        /// <summary>
        /// 获得权限范围用户ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        string GetDataAuthorUserId(Operator operators, bool isWrite = false);

        string GetReadProjectId(Operator operators, bool isWrite = false);
        /// <summary>
        /// 获得可读数据权限范围SQL
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        string GetDataAuthor(Operator operators, bool isWrite = false);
    }
}
