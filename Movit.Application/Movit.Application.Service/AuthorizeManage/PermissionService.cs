using Movit.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System;
using Movit.Application.IService.AuthorizeManage;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Entity.AuthorizeManage;
using Movit.Application.Code;
using System.Data.Common;
using Movit.Data;
using System.Text;
using Movit.Application.Entity;
using Movit.Util.Extension;
using Movit.Util;
using Movit.Util.WebControl;
using Movit.Application.Service.AuthorizeManage;

namespace Movit.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.11.5 22:35
    /// 描 述：权限配置管理（角色、岗位、职位、用户组、用户）
    /// </summary>
    public class PermissionService : RepositoryFactory, IPermissionService
    {
        #region 获取数据
        /// <summary>
        /// 获取成员列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetMemberList(string objectId)
        {
            return this.BaseRepository().IQueryable<UserRelationEntity>(t => t.ObjectId == objectId).OrderByDescending(t => t.CreateDate).ToList();
        }
        public IEnumerable<UserRelation_View> GetPageList(Pagination pagination, string queryJson)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select buser.UserId,ur.ObjectId,
                        ur.CreateDate,ur.CreateUserName,
                        buser.NickName,buser.Account ,buser.Mobile,
                        buser.DepartmentId,dep.FullName DepartmentName
                        from Base_UserRelation ur
                        inner join Base_User buser
                        on ur.UserId=buser.UserId
                        inner join Base_Department dep
                        on dep.DepartmentId=buser.DepartmentId
                        where buser.DeleteMark=0 ");

            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();

            //查询条件
            if (!queryParam["ObjectId"].IsEmpty())
            {
                strSql.Append(" and ObjectId = @ObjectId");
                parameter.Add(DbParameters.CreateDbParameter("@ObjectId", queryParam["ObjectId"].ToString()));
            }
            if (!queryParam["queryJson"].IsEmpty())
            {
                strSql.Append(" and  (dep.FullName like @queryJson  or buser.NickName like @queryJson or buser.Account like @queryJson or buser.Mobile like @queryJson )");
                parameter.Add(DbParameters.CreateDbParameter("@queryJson", '%' + queryParam["queryJson"].ToString() + '%'));
            }

            return this.BaseRepository().FindList<UserRelation_View>(strSql.ToString(), parameter.ToArray(), pagination);



        }
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetObjectList(string userId)
        {
            return this.BaseRepository().IQueryable<UserRelationEntity>(t => t.UserId == userId).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleList(string objectId)
        {
            return this.BaseRepository().IQueryable<AuthorizeEntity>(t => t.ObjectId == objectId && t.ItemType == 1).ToList();
        }
        /// <summary>
        /// 获取按钮列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleButtonList(string objectId)
        {
            return this.BaseRepository().IQueryable<AuthorizeEntity>(t => t.ObjectId == objectId && t.ItemType == 2).ToList();
        }
        /// <summary>
        /// 获取视图列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleColumnList(string objectId)
        {
            return this.BaseRepository().IQueryable<AuthorizeEntity>(t => t.ObjectId == objectId && t.ItemType == 3).ToList();
        }
        /// <summary>
        /// 获取数据权限列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeDataEntity> GetAuthorizeDataList(string objectId)
        {
            return this.BaseRepository().IQueryable<AuthorizeDataEntity>(t => t.ObjectId == objectId).OrderBy(t => t.SortCode).ToList();
        }
        public bool IsAdded(string key,string objectId,int auto)
        {
            var expression = LinqExtensions.True<UserRelationEntity>();
            expression = expression.And(t => t.ObjectId == objectId && t.Category == auto&&t.UserId==key);
            var data= this.BaseRepository().FindEntity<UserRelationEntity>(expression);
            if(data!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="authorizeType">权限分类</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="userIds">成员Id</param>
        public void SaveMember(AuthorizeTypeEnum authorizeType, string objectId, string[] userIds, string[] deleteUserIds)
        {
            List<UserRelationEntity> plist = new List<UserRelationEntity>();
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                foreach (var item in deleteUserIds)
                {
                    var expression = LinqExtensions.True<UserRelationEntity>();
                    expression = expression.And(t => t.ObjectId == objectId && t.UserId == item);
                    db.Delete<UserRelationEntity>(expression);
                }

                int SortCode = 1;
                UserRelationEntity userRelationEntity = null;

                foreach (string item in userIds)
                {
                    userRelationEntity = new UserRelationEntity();
                    userRelationEntity.Create();
                    userRelationEntity.Category = (int)authorizeType;
                    userRelationEntity.ObjectId = objectId;
                    userRelationEntity.UserId = item;
                    userRelationEntity.SortCode = SortCode++;
                    bool isAdded = IsAdded(item, objectId, (int)authorizeType);
                    if(!isAdded)
                    {
                    plist.Add(userRelationEntity);
                    }
                    userRelationEntity = null;
                }
                if (plist.Count > 0)
                {
                    db.Insert(plist);

                }

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
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

        public void SaveAuthorizeAndPost(AuthorizeTypeEnum authorizeType, string postId, string[] projectColumnIds, string[] arryprojectNames)
        {
            var projectList = this.BaseRepository().IQueryable<Base_ProjectInfoEntity>().ToList();

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

            try
            {

                if (projectColumnIds.Length != arryprojectNames.Length)
                {
                    throw new Exception("授权数据异常!");
                }


                #region 数据权限

                int SortCode = 1;
                Base_ProjectInfoEntity currentProject = null;
                for (int i = 0; i < projectColumnIds.Length; i++)
                {
                    currentProject = projectList.FirstOrDefault(p => p.ProjectID == projectColumnIds[i]);
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.Create();
                    authorizeEntity.Category = (int)authorizeType;
                    authorizeEntity.ObjectId = postId;
                    authorizeEntity.ItemType = (int)AuthorizeItmeTypeEnum.ProjectInfo;
                    authorizeEntity.ItemId = projectColumnIds[i];
                    authorizeEntity.ItemName = arryprojectNames[i];
                    authorizeEntity.SortCode = SortCode++;
                    if (currentProject != null)
                    {
                        authorizeEntity.Remark1 = currentProject.CompanyName;
                        authorizeEntity.Remark2 = currentProject.CityName;
                    }

                    db.Insert(authorizeEntity);
                }
                //foreach (string item in projectColumnIds)
                //{
                //    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                //    authorizeEntity.Create();
                //    authorizeEntity.Category = (int)authorizeType;
                //    authorizeEntity.ObjectId = postId;
                //    authorizeEntity.ItemType = (int)AuthorizeItmeTypeEnum.ProjectInfo;
                //    authorizeEntity.ItemId = item;
                //    authorizeEntity.SortCode = SortCode++;
                //    db.Insert(authorizeEntity);
                //}
                #endregion
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"update Base_Role set AuthorizationMethod=@AuthorizationMethod where RoleId=@PostID");
                var parameter = new List<DbParameter>();
                parameter.Add(DbParameters.CreateDbParameter("@PostID", postId));
                parameter.Add(DbParameters.CreateDbParameter("@AuthorizationMethod", (int)AuthorizationMethodEnum.CustomizeProject));
                db.ExecuteBySql(strSql.ToString(), parameter.ToArray());
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw new Exception(ex.Message);
            }

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

            string sqlInsert = @"insert into Base_Authorize (AuthorizeId, 
                    Category,
                     ObjectId ,
                     ItemType,
                     ItemId,
                     SortCode,
                     CreateDate,
                     CreateUserId,
                     CreateUserName,
                     ItemName,
                     Remark1,Remark2)
                    select LOWER(NEWID()),
                     @Category,@ObjectId,@ItemType,ProjectID,1,GETDATE(),@UserID,@UserName,ProjecName,CompanyName,CityName from Base_ProjectInfo;";
            var parameter = new List<DbParameter>();
            var currentUser = OperatorProvider.Provider.Current();
            parameter.Add(DbParameters.CreateDbParameter("@ObjectId", keyValue));
            parameter.Add(DbParameters.CreateDbParameter("@Category", (int)AuthorizeTypeEnum.Post));
            parameter.Add(DbParameters.CreateDbParameter("@ItemType", (int)AuthorizeItmeTypeEnum.ProjectInfo));
            parameter.Add(DbParameters.CreateDbParameter("@UserID", currentUser.UserId));
            parameter.Add(DbParameters.CreateDbParameter("@UserName", currentUser.UserName));
            this.BaseRepository().ExecuteBySql(sqlInsert, parameter.ToArray());
        }
        /// <summary>
        /// 添加授权
        /// </summary>
        /// <param name="authorizeType">权限分类</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="moduleButtonIds">按钮Id</param>
        /// <param name="moduleColumnIds">视图Id</param>
        /// <param name="authorizeDataList">数据权限</param>
        public void SaveAuthorize(AuthorizeTypeEnum authorizeType, string objectId, string[] moduleIds, string[] moduleButtonIds, string[] moduleColumnIds, IEnumerable<AuthorizeDataEntity> authorizeDataList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<AuthorizeEntity>(t => t.ObjectId == objectId);

                #region 功能
                int SortCode = 1;
                foreach (string item in moduleIds)
                {
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.Create();
                    authorizeEntity.Category = (int)authorizeType;
                    authorizeEntity.ObjectId = objectId;
                    authorizeEntity.ItemType = 1;
                    authorizeEntity.ItemId = item;
                    authorizeEntity.SortCode = SortCode++;
                    db.Insert(authorizeEntity);
                }
                #endregion

                #region 按钮
                SortCode = 1;
                foreach (string item in moduleButtonIds)
                {
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.Create();
                    authorizeEntity.Category = (int)authorizeType;
                    authorizeEntity.ObjectId = objectId;
                    authorizeEntity.ItemType = 2;
                    authorizeEntity.ItemId = item;
                    authorizeEntity.SortCode = SortCode++;
                    db.Insert(authorizeEntity);
                }
                #endregion

                //#region 视图
                //SortCode = 1;
                //foreach (string item in moduleColumnIds)
                //{
                //    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                //    authorizeEntity.Create();
                //    authorizeEntity.Category = (int)authorizeType;
                //    authorizeEntity.ObjectId = objectId;
                //    authorizeEntity.ItemType = 3;
                //    authorizeEntity.ItemId = item;
                //    authorizeEntity.SortCode = SortCode++;
                //    db.Insert(authorizeEntity);
                //}
                //#endregion

                //#region 数据权限
                //SortCode = 1;
                //db.Delete<AuthorizeDataEntity>(objectId, "ObjectId");
                //int index = 0;
                //foreach (AuthorizeDataEntity authorizeDataEntity in authorizeDataList)
                //{
                //    authorizeDataEntity.Create();
                //    authorizeDataEntity.Category = (int)authorizeType;
                //    authorizeDataEntity.ObjectId = objectId;
                //    // authorizeDataEntity.Module = "Department";
                //    authorizeDataEntity.SortCode = SortCode++;
                //    db.Insert(authorizeDataEntity);
                //    index++;
                //}
                //#endregion

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
