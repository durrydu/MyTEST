using Movit.Application.Code;
using Movit.Application.Entity;
using Movit.Application.Entity.AuthorizeManage;
using Movit.Application.Entity.AuthorizeManage.ViewModel;
using Movit.Application.Entity.BaseManage;
using Movit.Application.IService.AuthorizeManage;
using Movit.Data;
using Movit.Data.Repository;
using Movit.Util.Extension;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movit.Util;

namespace Movit.Application.Service.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2018-2016 
    /// 创建人： 姚栋
    /// 日 期：2015.12.5 22:35
    /// 描 述：授权认证
    /// </summary>
    public class AuthorizeService : RepositoryFactory, IAuthorizeService
    {

        /// <summary>
        /// 批量删除岗位项目授权数据
        /// </summary>
        /// <param name="authorizeIds">授权ID主键集合</param>
        public void BatchRemoveForm(string[] authorizeIds)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {

                foreach (string item in authorizeIds)
                {
                    db.Delete<AuthorizeEntity>(item);
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
        /// 移除对应岗位项目授权数据
        /// </summary>
        /// <param name="postId">岗位ID</param>
        public void BatchRemoveFormAll(string postId)
        {
            try
            {
                string sqlstring = string.Format("delete Base_Authorize where ObjectId=@postId");
                var parameter = new List<DbParameter>();
                parameter.Add(DbParameters.CreateDbParameter("@postId", postId));
                this.BaseRepository().ExecuteBySql(sqlstring, parameter.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 描述:根据岗位ID获取已经授权过的项目信息
        /// 作者:姚栋
        /// 日期:2018-05-30
        /// </summary>
        /// <param name="keyValue">岗位ID</param>
        /// <param name="condition">项目名称D</param>
        public IEnumerable<AuthorizeEntity> GetProjectListByPostId(string keyValue, string queryJson, Pagination pagination)
        {


            var expression = LinqExtensions.True<AuthorizeEntity>();
            expression = expression.And(t => t.Category == (int)AuthorizeTypeEnum.Post)
                .And(p => p.ObjectId == keyValue).And(p => p.ItemType == (int)AuthorizeItmeTypeEnum.ProjectInfo);
            var queryParam = queryJson.ToJObject();

            if (!queryParam["projectName"].IsEmpty())
            {
                string projectName = queryParam["projectName"].ToString();
                expression = expression.And(p => p.ItemName.Contains(projectName));
            }
            return this.BaseRepository().FindList<AuthorizeEntity>(expression, pagination);

        }
        /// <summary>
        /// 描述:根据岗位ID获取未授权过的项目信息
        /// 作者:姚栋
        /// 日期:2018-05-30
        /// </summary>
        /// <param name="keyValue">角色ID</param>
        /// <param name="pagination">分页对象</param>
        ///  <param name="queryJson">查询条件</param>
        public IEnumerable<Base_ProjectInfoEntity> GetProjectExcept(string keyValue, Pagination pagination, string queryJson)
        {


            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"    select  *
                         from  Base_ProjectInfo pinfo
                        where  not exists(select 1 from Base_Authorize db
                         where db.ItemId=pinfo.ProjectID and db.ObjectId='" + keyValue + "') ");
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            var condition = queryParam["condition"];
            //查询条件
            if (!condition.IsEmpty())
            {

                strSql.Append(" and pinfo.ProjecName like  @ProjecName or pinfo.CompanyName like  @ProjecName or pinfo.CityName like  @ProjecName");
                parameter.Add(DbParameters.CreateDbParameter("@ProjecName", '%' + condition.ToString() + '%'));


            }
            return this.BaseRepository().FindList<Base_ProjectInfoEntity>(strSql.ToString(), parameter.ToArray(), pagination);




        }

        /// <summary>
        /// 获取授权功能
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetModuleList(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    Base_Module
                            WHERE   ModuleId IN (
                                    SELECT  ItemId
                                    FROM    Base_Authorize
                                    WHERE   ItemType = 1
                                            AND ( ObjectId IN (
                                                  SELECT    ObjectId
                                                  FROM      Base_UserRelation
                                                  WHERE     UserId = @UserId ) 
                                            OR ObjectId = @UserId 
                                            ))
                            AND EnabledMark = 1  AND DeleteMark = 0 Order By SortCode");
            DbParameter[] parameter = 
            {
                DbParameters.CreateDbParameter("@UserId",userId)
            };
            return this.BaseRepository().FindList<ModuleEntity>(strSql.ToString(), parameter);
        }
        /// <summary>
        /// 获取授权功能按钮
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleButtonEntity> GetModuleButtonList(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    Base_ModuleButton
                            WHERE   ModuleButtonId IN (
                                    SELECT  ItemId
                                    FROM    Base_Authorize
                                    WHERE   ItemType = 2
                                            AND ( ObjectId IN (
                                                  SELECT    ObjectId
                                                  FROM      Base_UserRelation
                                                  WHERE     UserId = @UserId ) )
                                            OR ObjectId = @UserId ) Order By SortCode");
            DbParameter[] parameter = 
            {
                DbParameters.CreateDbParameter("@UserId",userId)
            };
            return this.BaseRepository().FindList<ModuleButtonEntity>(strSql.ToString(), parameter);
        }
        /// <summary>
        /// 获取授权功能视图
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleColumnEntity> GetModuleColumnList(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    Base_ModuleColumn
                            WHERE   ModuleColumnId IN (
                                    SELECT  ItemId
                                    FROM    Base_Authorize
                                    WHERE   ItemType = 3
                                            AND ( ObjectId IN (
                                                  SELECT    ObjectId
                                                  FROM      Base_UserRelation
                                                  WHERE     UserId = @UserId ) )
                                            OR ObjectId = @UserId )  Order By SortCode");
            DbParameter[] parameter = 
            {
                DbParameters.CreateDbParameter("@UserId",userId)
            };
            return this.BaseRepository().FindList<ModuleColumnEntity>(strSql.ToString(), parameter);
        }
        /// <summary>
        /// 获取授权功能Url、操作Url
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeUrlModel> GetUrlList(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  ModuleId AS AuthorizeId ,
                                    ModuleId ,
                                    UrlAddress ,
                                    FullName
                            FROM    Base_Module
                            WHERE   ModuleId IN (
                                    SELECT  ItemId
                                    FROM    Base_Authorize
                                    WHERE   ItemType = 1
                                            AND ( ObjectId IN (
                                                  SELECT    ObjectId
                                                  FROM      Base_UserRelation
                                                  WHERE     UserId = @UserId ) )
                                            OR ObjectId = @UserId )
                                    AND EnabledMark = 1
                                    AND DeleteMark = 0
                           
                                    AND UrlAddress IS NOT NULL
                            UNION
                            SELECT  ModuleButtonId AS AuthorizeId ,
                                    ModuleId ,
                                    ActionAddress AS UrlAddress ,
                                    FullName
                            FROM    Base_ModuleButton
                            WHERE   ModuleButtonId IN (
                                    SELECT  ItemId
                                    FROM    Base_Authorize
                                    WHERE   ItemType = 2
                                            AND ( ObjectId IN (
                                                  SELECT    ObjectId
                                                  FROM      Base_UserRelation
                                                  WHERE     UserId = @UserId ) )
                                            OR ObjectId = @UserId )
                                    AND ActionAddress IS NOT NULL");
            DbParameter[] parameter = 
            {
                DbParameters.CreateDbParameter("@UserId",userId)
            };
            return this.BaseRepository().FindList<AuthorizeUrlModel>(strSql.ToString(), parameter);
        }
        /// <summary>
        /// 获取关联用户关系
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetUserRelationList(string userId)
        {
            return this.BaseRepository().IQueryable<UserRelationEntity>(t => t.UserId == userId);
        }
        /// <summary>
        /// 获得权限范围用户ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthorUserId(Operator operators, bool isWrite = false)
        {
            string userIdList = GetDataAuthor(operators, isWrite);
            if (userIdList == "")
            {
                return "";
            }
            IRepository db = new RepositoryFactory().BaseRepository();
            string userId = operators.UserId;
            List<UserEntity> userList = db.FindList<UserEntity>(userIdList).ToList();
            StringBuilder userSb = new StringBuilder("");
            if (userList != null)
            {
                foreach (var item in userList)
                {
                    userSb.Append(item.UserId);
                    userSb.Append(",");
                }
            }
            return userSb.ToString();
        }
        /// <summary>
        /// 获得权限范围项目ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetReadProjectId(Operator operators, bool isWrite = false)
        {

            IRepository db = new RepositoryFactory().BaseRepository();
            string userId = operators.UserId;
            List<Post_ProjectView> userList = db.FindList<Post_ProjectView>(p => p.UserId == operators.UserId).ToList();
            StringBuilder userSb = new StringBuilder("");
            if (userList != null)
            {
                foreach (var item in userList)
                {
                    userSb.Append(item.ItemId);
                    userSb.Append(",");
                }
            }
            return userSb.ToString();
        }
        /// <summary>
        /// 获得可读数据权限范围SQL
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthor(Operator operators, bool isWrite = false)
        {
            //如果是系统管理员直接给所有数据权限
            if (operators.IsSystem)
            {
                return "";
            }
            IRepository db = new RepositoryFactory().BaseRepository();
            string userId = operators.UserId;
            StringBuilder whereSb = new StringBuilder(" select UserId from Base_user where 1=1 ");
            string strAuthorData = "";
            if (isWrite)
            {
                strAuthorData = @"   SELECT    *
                                        FROM      Base_AuthorizeData
                                        WHERE     IsRead=0 AND
                                        ObjectId IN (
                                                SELECT  ObjectId
                                                FROM    Base_UserRelation
                                                WHERE   UserId =@UserId)";
            }
            else
            {
                strAuthorData = @"   SELECT    *
                                        FROM      Base_AuthorizeData
                                        WHERE     
                                        ObjectId IN (
                                                SELECT  ObjectId
                                                FROM    Base_UserRelation
                                                WHERE   UserId =@UserId)";
            }
            DbParameter[] parameter = 
            {
                DbParameters.CreateDbParameter("@UserId",userId),
            };
            whereSb.Append(string.Format("AND( UserId ='{0}'", userId));
            IEnumerable<AuthorizeDataEntity> listAuthorizeData = db.FindList<AuthorizeDataEntity>(strAuthorData, parameter);
            foreach (AuthorizeDataEntity item in listAuthorizeData)
            {
                switch (item.AuthorizeType)
                {
                    //0代表最大权限
                    case 0://
                        return "";
                    //本人及下属
                    case -2://
                        whereSb.Append("  OR ManagerId ='{0}'");
                        break;
                    case -3:
                        whereSb.Append(@"  OR DepartmentId = (  SELECT  DepartmentId
                                                                    FROM    Base_User
                                                                    WHERE   UserId ='{0}'
                                                                  )");
                        break;
                    case -4:
                        whereSb.Append(@"  OR OrganizeId = (    SELECT  OrganizeId
                                                                    FROM    Base_User
                                                                    WHERE   UserId ='{0}'
                                                                  )");
                        break;
                    case -5:
                        whereSb.Append(string.Format(@"  OR DepartmentId='{1}' OR OrganizeId='{1}'", userId, item.ResourceId));
                        break;
                }
            }
            whereSb.Append(")");
            return whereSb.ToString();
        }

    }
}
