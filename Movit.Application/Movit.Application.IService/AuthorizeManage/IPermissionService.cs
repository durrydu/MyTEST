using Movit.Application.Code;
using Movit.Application.Entity.AuthorizeManage;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Service.AuthorizeManage;
using Movit.Util.WebControl;
using System.Collections.Generic;

namespace Movit.Application.IService.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.11.5 22:35
    /// 描 述：权限配置管理（角色、岗位、职位、用户组、用户）
    /// </summary>
    public interface IPermissionService
    {
        #region 获取数据
        /// <summary>
        /// 获取成员列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        IEnumerable<UserRelationEntity> GetMemberList(string objectId);
        IEnumerable<UserRelation_View> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IEnumerable<UserRelationEntity> GetObjectList(string userId);
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        IEnumerable<AuthorizeEntity> GetModuleList(string objectId);
        /// <summary>
        /// 获取按钮列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        IEnumerable<AuthorizeEntity> GetModuleButtonList(string objectId);
        /// <summary>
        /// 获取视图列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        IEnumerable<AuthorizeEntity> GetModuleColumnList(string objectId);
        /// <summary>
        /// 获取数据权限列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        IEnumerable<AuthorizeDataEntity> GetAuthorizeDataList(string objectId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="authorizeType">权限分类</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="userIds">成员Id</param>
        void SaveMember(AuthorizeTypeEnum authorizeType, string objectId, string[] userIds, string[] arraydeleteUserIds);

        /// <summary>
        /// 作者:姚栋
        /// 日期:2018-05-30
        /// 描述:给某个岗位进行项目授权
        /// </summary>
        ///  <param name="authorizeType">授权类型</param>
        /// <param name="postId">岗位Id</param>
        /// <param name="moduleIds">项目Id集合</param>·
        /// <returns></returns>

        void SaveAuthorizeAndPost(AuthorizeTypeEnum authorizeType, string postId, string[] projectColumnIds, string[] arryprojectNames);
        /// <summary>
        /// 作者:姚栋
        /// 日期:2018-05-31
        /// 描述:给某个岗位进行全有项目授权
        /// </summary>
        /// <param name="keyValue">岗位Id</param>
        /// <returns></returns>

        void SaveAuthorizeAndPostAll(string keyValue);
        /// <summary>
        /// 添加授权
        /// </summary>
        /// <param name="authorizeType">权限分类</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="moduleButtonIds">按钮Id</param>
        /// <param name="moduleColumnIds">视图Id</param>
        /// <param name="authorizeDataList">数据权限</param>
        void SaveAuthorize(AuthorizeTypeEnum authorizeType, string objectId, string[] moduleIds, string[] moduleButtonIds, string[] moduleColumnIds, IEnumerable<AuthorizeDataEntity> authorizeDataList);
        #endregion
    }
}
