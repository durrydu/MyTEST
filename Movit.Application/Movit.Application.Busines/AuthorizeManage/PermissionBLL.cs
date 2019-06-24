using Movit.Application.Busines.BaseManage;
using Movit.Application.Code;
using Movit.Application.Entity.AuthorizeManage;
using Movit.Application.Entity.BaseManage;
using Movit.Application.IService.AuthorizeManage;
using Movit.Application.Service.BaseManage;
using Movit.Util;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Movit.Util.WebControl;
using Movit.Application.Service.AuthorizeManage;

namespace Movit.Application.Busines.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.11.5 22:35
    /// 描 述：权限配置管理（角色、岗位、职位、用户组、用户）
    /// </summary>
    public class PermissionBLL
    {
        private IPermissionService service = new PermissionService();
        private UserBLL userBLL = new UserBLL();

        #region 获取数据
        /// <summary>
        /// 获取成员列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetMemberList(string objectId)
        {
            return service.GetMemberList(objectId);
        }
        public IEnumerable<UserRelation_View> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetObjectList(string userId)
        {
            return service.GetObjectList(userId);
        }
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetObjectStr(string userId)
        {
            StringBuilder sbId = new StringBuilder();
            List<UserRelationEntity> list = service.GetObjectList(userId).ToList();
            if (list.Count > 0)
            {
                foreach (UserRelationEntity item in list)
                {
                    sbId.Append(item.ObjectId + ",");
                }
                sbId.Append(userId);
            }
            else
            {
                sbId.Append(userId + ",");
            }
            return sbId.ToString();
        }
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleList(string objectId)
        {
            return service.GetModuleList(objectId);
        }
        /// <summary>
        /// 获取按钮列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleButtonList(string objectId)
        {
            return service.GetModuleButtonList(objectId);
        }
        /// <summary>
        /// 获取视图列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleColumnList(string objectId)
        {
            return service.GetModuleColumnList(objectId);
        }
        /// <summary>
        /// 获取数据权限列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeDataEntity> GetAuthorizeDataList(string objectId)
        {
            return service.GetAuthorizeDataList(objectId);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="authorizeType">权限分类</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="userIds">成员Id：1,2,3,4</param>
        public void SaveMember(AuthorizeTypeEnum authorizeType, string objectId, string userIds, string deleteUserIds)
        {
            try
            {
                string[] arrayUserId = userIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] arraydeleteUserIds = deleteUserIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                service.SaveMember(authorizeType, objectId, arrayUserId, arraydeleteUserIds);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 作者:姚栋
        /// 日期:2018-05-30
        /// 描述:给某个岗位进行项目授权
        /// </summary>
        ///  <param name="authorizeType">授权类型</param>
        /// <param name="postId">岗位Id</param>
        /// <param name="moduleIds">项目Id集合</param>·
        /// <returns></returns>

        public void SaveAuthorizeAndPost(AuthorizeTypeEnum authorizeType, string postId, string projectColumnIds, string projectColumnNames)
        {
            string[] arrayprojectId = projectColumnIds.Split(',');
            string[] arryprojectName = projectColumnNames.Split(',');
            service.SaveAuthorizeAndPost(authorizeType, postId, arrayprojectId, arryprojectName);
        }
        /// <summary>
        /// 作者:姚栋
        /// 日期:2018-05-31
        /// 描述:给某个岗位进行全有项目授权
        /// </summary>
        /// <param name="keyValue">岗位Id</param>
        /// <returns></returns>

        public void SaveAuthorizeAndPostAll(string keyValue)
        {
            service.SaveAuthorizeAndPostAll(keyValue);
        }
        /// <summary>
        /// 保存授权
        /// </summary>
        /// <param name="authorizeType">权限分类</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="moduleButtonIds">按钮Id</param>
        /// <param name="moduleColumnIds">视图Id</param>
        /// <param name="authorizeDataJson">数据权限</param>
        /// <returns></returns>
        public void SaveAuthorize(AuthorizeTypeEnum authorizeType, string objectId, string moduleIds, string moduleButtonIds, string moduleColumnIds, string authorizeDataJson)
        {
            try
            {
                string[] arrayModuleId = moduleIds.Split(',');
                string[] arrayModuleButtonId = moduleButtonIds.Split(',');
                string[] arrayModuleColumnId = moduleColumnIds.Split(',');
                IEnumerable<AuthorizeDataEntity> authorizeDataList = authorizeDataJson.ToList<AuthorizeDataEntity>();
                service.SaveAuthorize(authorizeType, objectId, arrayModuleId, arrayModuleButtonId, arrayModuleColumnId, authorizeDataList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
