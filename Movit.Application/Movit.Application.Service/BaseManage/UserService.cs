using Movit.Data;
using Movit.Data.Repository;
using Movit.Util;
using Movit.Util.WebControl;
using Movit.Util.Extension;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Linq;
using Movit.Application.Entity.BaseManage;
using Movit.Application.IService.BaseManage;
using Movit.Application.IService.AuthorizeManage;
using Movit.Application.Service.AuthorizeManage;
using System;
using Movit.Application.Code;
using Movit.Application.Entity.SystemManage;
using Movit.Util.Attributes;
using Movit.Application.Service.SystemManage;
using Movit.Application.Entity;
using Movit.Application.Entity.BaseManage.ViewModel;

namespace Movit.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.11.03 10:58
    /// 描 述：用户管理
    /// </summary>
    public class UserService : RepositoryFactory, IUserService
    {
        private LogService logServer = new LogService();

        private IAuthorizeService<UserEntity> iauthorizeservice = new AuthorizeService<UserEntity>();

        #region 获取数据
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable(Pagination pagination, string orgId = null, string keyQueryValue = null)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  u.*,
                                    d.FullName AS DepartmentName 
                            FROM    Base_User u
                                    LEFT JOIN Base_Department d ON d.DepartmentId = u.DepartmentId
                            WHERE   1=1");
            if (!string.IsNullOrEmpty(orgId))
            {

                strSql.Append(" AND d.DepartmentId='" + orgId + "'");
            }
            if (!string.IsNullOrEmpty(keyQueryValue))
            {
                strSql.Append(" and ");
                strSql.Append(" ( d.FullName like '%" + keyQueryValue + "%'");
                strSql.Append(" or u.Account like '%" + keyQueryValue + "%'");
                strSql.Append(" or d.EnCode like '%" + keyQueryValue + "%'");
                strSql.Append(" or u.RealName like '%" + keyQueryValue + "%'");
                strSql.Append(" or u.NickName like '%" + keyQueryValue + "%')");
            }
            strSql.Append(" AND u.UserId <> 'System' AND u.EnabledMark = 1 AND u.DeleteMark=0 ");
            return this.BaseRepository().FindTable(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetList()
        {
            var expression = LinqExtensions.True<UserEntity>();
            expression = expression.And(t => t.UserId != "System").And(t => t.EnabledMark == 1).And(t => t.DeleteMark == 0);
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<UserEntity>();
            var queryParam = queryJson.ToJObject();
            //账户
            if (!queryParam["Account"].IsEmpty())
            {
                string Account = queryParam["Account"].ToString();
                expression = expression.And(t => t.Account.Contains(Account));
            }
            //姓名
            if (!queryParam["RealName"].IsEmpty())
            {
                string RealName = queryParam["RealName"].ToString();
                expression = expression.And(t => t.RealName.Contains(RealName));
            }
            //手机
            if (!queryParam["Mobile"].IsEmpty())
            {
                string Mobile = queryParam["Mobile"].ToString();
                expression = expression.And(t => t.Mobile.Contains(Mobile));
            }
            expression = expression.And(t => t.Account != "System");
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 用户列表（包括用户的岗位及角色）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> GetUserModelPageList(Pagination pagination, string queryJson)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select #temp.UserId,#temp.Account,#temp.RealName,#temp.Mobile,#temp.OrganizeId,#temp.CreateDate,STUFF(( SELECT  ',' + RoleName
                FROM    (select distinct buser.UserId,role.FullName RoleName from Base_User buser 
left join Base_UserRelation relation on buser.UserId = relation.UserId and relation.Category=2
left join Base_Role role on role.RoleId=relation.ObjectId
left join Base_UserRelation dept on buser.UserId = dept.UserId and dept.Category=3
left join Base_Role deptRole on deptRole.RoleId = dept.ObjectId) b
                WHERE   #temp.UserId = b.UserId
              FOR
                XML PATH('')
              ), 1, 1, '') AS RoleName,STUFF(( SELECT  ',' + DepartmentName
                FROM    (select distinct deptRole.FullName DepartmentName, buser.UserId  from Base_User buser 
left join Base_UserRelation relation on buser.UserId = relation.UserId and relation.Category=2
left join Base_Role role on role.RoleId=relation.ObjectId
left join Base_UserRelation dept on buser.UserId = dept.UserId and dept.Category=3
left join Base_Role deptRole on deptRole.RoleId = dept.ObjectId) b
                WHERE   #temp.UserId = b.UserId
              FOR
                XML PATH('')
              ), 1, 1, '') AS DepartmentName from( select buser.*,role.RoleId as RoleIds,deptRole.RoleId as DepartmentIds,role.FullName RoleName,deptRole.FullName DepartmentName from Base_User buser 
left join Base_UserRelation relation on buser.UserId = relation.UserId and relation.Category=2
left join Base_Role role on role.RoleId=relation.ObjectId
left join Base_UserRelation dept on buser.UserId = dept.UserId and dept.Category=3
left join Base_Role deptRole on deptRole.RoleId = dept.ObjectId) #temp where 1=1 ");

            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();

            //查询条件
            if (!queryParam["Account"].IsEmpty())
            {
                strSql.Append(" and #temp.Account like @Account");
                parameter.Add(DbParameters.CreateDbParameter("@Account", '%' + queryParam["Account"].ToString() + '%'));
            }
            if (!queryParam["RealName"].IsEmpty())
            {
                strSql.Append(" and #temp.RealName like @RealName");
                parameter.Add(DbParameters.CreateDbParameter("@RealName", '%' + queryParam["RealName"].ToString() + '%'));
            }
            if (!queryParam["Mobile"].IsEmpty())
            {
                strSql.Append(" and #temp.Mobile like @Mobile");
                parameter.Add(DbParameters.CreateDbParameter("@Mobile", '%' + queryParam["Mobile"].ToString() + '%'));
            }
            if (!queryParam["RoleId"].IsEmpty())
            {
                strSql.Append(" and #temp.RoleIds = '" + queryParam["RoleId"].ToString() + "'");
                //parameter.Add(DbParameters.CreateDbParameter("@RealName", '%' + queryParam["RealName"].ToString() + '%'));
            }
            if (!queryParam["DepartmentId"].IsEmpty())
            {
                strSql.Append(" and #temp.DepartmentIds = '" + queryParam["DepartmentId"].ToString() + "'");
                //parameter.Add(DbParameters.CreateDbParameter("@RealName", '%' + queryParam["RealName"].ToString() + '%'));
            }
            strSql.Append(" group by #temp.UserId,#temp.Account,#temp.RealName,#temp.Mobile,#temp.OrganizeId,#temp.CreateDate");
            return this.BaseRepository().FindList<UserModel>(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 用户列表（ALL）
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTable()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  u.UserId ,
                                    u.EnCode ,
                                    u.Account ,
                                    u.RealName ,
                                    u.Gender ,
                                    u.Birthday ,
                                    u.Mobile ,
                                    u.Manager ,
                                    u.OrganizeId,
                                    u.DepartmentId,
                                    o.FullName AS OrganizeName ,
                                    d.FullName AS DepartmentName ,
                                    u.RoleId ,
                                    u.DutyName ,
                                    u.PostName ,
                                    u.EnabledMark ,
                                    u.CreateDate,
                                    u.Description
                            FROM    Base_User u
                                    LEFT JOIN Base_Organize o ON o.OrganizeId = u.OrganizeId
                                    LEFT JOIN Base_Department d ON d.DepartmentId = u.DepartmentId
                            WHERE   1=1");
            strSql.Append(" AND u.UserId <> 'System' AND u.EnabledMark = 1 AND u.DeleteMark=0 order by o.FullName,d.FullName,u.RealName");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户列表（导出Excel）
        /// </summary>
        /// <returns></returns>
        public DataTable GetExportList()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT [Account]
                                  ,[RealName]
                                  ,CASE WHEN Gender=1 THEN '男' ELSE '女' END AS Gender
                                  ,[Birthday]
                                  ,[Mobile]
                                  ,[Telephone]
                                  ,u.[Email]
                                  ,[WeChat]
                                  ,[MSN]
                                  ,u.[Manager]
                                  ,o.FullName AS Organize
                                  ,d.FullName AS Department
                                  ,u.[Description]
                                  ,u.[CreateDate]
                                  ,u.[CreateUserName]
                              FROM Base_User u
                              INNER JOIN Base_Department d ON u.DepartmentId=d.DepartmentId
                              INNER JOIN Base_Organize o ON u.OrganizeId=o.OrganizeId");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public UserEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<UserEntity>(keyValue);
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public UserEntity CheckLogin(string username)
        {
            //var plist = new AuthorizeService<Base_ProjectInfoEntity>().IQueryable("ProjectID").ToList();
            var expression = LinqExtensions.True<UserEntity>();
            expression = expression.And(t => t.Account == username);
            expression = expression.Or(t => t.Mobile == username);
            expression = expression.Or(t => t.Email == username);
            return this.BaseRepository().FindEntity(expression);
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public User_View GetUserViewEntity(string LoginName)
        {

            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@LoginName", LoginName));
            return this.BaseRepository().FindEntity<User_View>(@"select u.UserId,
                                u.EnCode, 
                                u.Account ,
                                u.RealName ,
                                u.NickName,
                                u.DepartmentId,
                                dep.FullName DepartmentName,
                                u.DeleteMark ,
                                u.EnabledMark 
                                from Base_User u
                                inner join Base_Department dep
                                on u.DepartmentId=dep.DepartmentId where Account=@LoginName", parameter.ToArray());
        }

        #endregion

        #region 验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="account">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistAccount(string account, string keyValue)
        {
            var expression = LinqExtensions.True<UserEntity>();
            expression = expression.And(t => t.Account == account);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.UserId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete<UserEntity>(keyValue);
        }
        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        public string SaveForm(string keyValue, UserEntity userEntity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                #region 基本信息
                if (!string.IsNullOrEmpty(keyValue))
                {
                    userEntity.Modify(keyValue);
                    userEntity.Password = null;
                    db.Update(userEntity);
                }
                else
                {
                    userEntity.Create();
                    keyValue = userEntity.UserId;
                    userEntity.Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
                    userEntity.Password = Md5Helper.MD5(DESEncrypt.Encrypt(Md5Helper.MD5(userEntity.Password, 32).ToLower(), userEntity.Secretkey).ToLower(), 32).ToLower();
                    db.Insert(userEntity);

                }
                #endregion

                #region 默认添加 角色、岗位、职位
                db.Delete<UserRelationEntity>(t => t.IsDefault == 1 && t.UserId == userEntity.UserId);
                List<UserRelationEntity> userRelationEntitys = new List<UserRelationEntity>();
                //角色
                if (!string.IsNullOrEmpty(userEntity.RoleId))
                {
                    userRelationEntitys.Add(new UserRelationEntity
                    {
                        Category = 2,
                        UserRelationId = Guid.NewGuid().ToString(),
                        UserId = userEntity.UserId,
                        ObjectId = userEntity.RoleId,
                        CreateDate = DateTime.Now,
                        CreateUserId = OperatorProvider.Provider.Current().UserId,
                        CreateUserName = OperatorProvider.Provider.Current().UserName,
                        IsDefault = 1,
                    });
                }
                //岗位
                if (!string.IsNullOrEmpty(userEntity.DutyId))
                {
                    userRelationEntitys.Add(new UserRelationEntity
                    {
                        Category = 3,
                        UserRelationId = Guid.NewGuid().ToString(),
                        UserId = userEntity.UserId,
                        ObjectId = userEntity.DutyId,
                        CreateDate = DateTime.Now,
                        CreateUserId = OperatorProvider.Provider.Current().UserId,
                        CreateUserName = OperatorProvider.Provider.Current().UserName,
                        IsDefault = 1,
                    });
                }
                //职位
                if (!string.IsNullOrEmpty(userEntity.PostId))
                {
                    userRelationEntitys.Add(new UserRelationEntity
                    {
                        Category = 4,
                        UserRelationId = Guid.NewGuid().ToString(),
                        UserId = userEntity.UserId,
                        ObjectId = userEntity.PostId,
                        CreateDate = DateTime.Now,
                        CreateUserId = OperatorProvider.Provider.Current().UserId,
                        CreateUserName = OperatorProvider.Provider.Current().UserName,
                        IsDefault = 1,
                    });
                }
                db.Insert(userRelationEntitys);
                #endregion

                db.Commit();

                return keyValue;
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="Password">新密码（MD5 小写）</param>
        public void RevisePassword(string keyValue, string Password)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.UserId = keyValue;
            userEntity.Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
            userEntity.Password = Md5Helper.MD5(DESEncrypt.Encrypt(Password, userEntity.Secretkey).ToLower(), 32).ToLower();
            this.BaseRepository().Update(userEntity);
        }
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-启动；0-禁用</param>
        public void UpdateState(string keyValue, int State)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.Modify(keyValue);
            userEntity.EnabledMark = State;
            this.BaseRepository().Update(userEntity);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userEntity">实体对象</param>
        public void UpdateEntity(UserEntity userEntity)
        {
            this.BaseRepository().Update(userEntity);
        }
        #endregion
        #region 用户同步数据
        /// <summary>
        /// 描述:用户新增
        /// 作者:姚栋
        /// 日期:2018.06.04
        /// </summary>
        public void SyncNewUser()
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 3;
            logEntity.OperateTypeId = ((int)OperationType.SyncData).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.SyncData);
            logEntity.OperateAccount = "WinDbSyncSerivce";
            logEntity.OperateUserId = "WinDbSyncSerivce";
            logEntity.Module = "WinDbSyncSerivce";

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append(@"insert into Base_User(
                            UserId,--用户GUID
                            EnCode,--用户编号
                            Account,--用户登录名
                            EmployeeNum,--员工工号
                            RealName,
                            NickName,--用户显示名称
                            FirstName,--姓
                            LastName,--名
                            UserType,--员工类型
                            Email,--邮箱
                            Mobile,--手机号码
                            Telephone,--办公电话
                            Birthday,--出生日期
                            CredentialNum,--身份证号码
                            HZNum,--护照号码
                            CreateDate,--创建时间
                            CreateUserId,--创建用户
                            ModifyUserName,--修改时间
                            ModifyUserId,--修改用户
                            SyncTime,--同步时间
                            SourceSys,--同步来源
                            SourceID,--同步来源数据主键
                            DeleteMark,       
                            EnabledMark,
                            F1,--备用字段1
                            F2,--备用字段2
                            F3--备用字段3
                            )select 
                            UserID ,--用户GUID
                            CodeName,--用户编号
                            LoginName,--用户登录名
                            EmployeeNum,--员工工号
                            FullName,--用户显示名称
                            FullName,--用户显示名称
                            FirstName,--姓
                            LastName,--名
                            UserType,--员工类型
                            Email,--邮箱
                            MobilePhone,--手机号码
                            BusinessTelephone,--办公电话
                            BirthDay,--出生日期
                            CredentialNum,--身份证号码
                            HZNum,--护照号码
                            CreateTime,--创建时间
                            CreateUser,--创建用户
                            UpdateTime,--修改时间
                            UpdateUser,--修改用户
                            getdate(),--同步时间
                            '主数据同步程序',--同步来源
                            UserId,--同步来源数据主键
                            case when DataStatus=0 then 1 when DataStatus=1 then 0 else 0 end,  
                            1,
                            F1,--备用字段1
                            F2,--备用字段2
                            F3--备用字段3
                           	from OPENQUERY(Link_MDM_MiddleBase,'select * from MDM_MiddleBase.dbo.T_User') as SyncUserTable
	                        where not exists(select 1 from Base_User where Base_User.SourceID=SyncUserTable.UserId);
	                         ");
            try
            {


                var result = this.BaseRepository().ExecuteBySql(sqlInsert.ToString());
                if (result >= 0)
                {
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.ExecuteResultJson = "同步程序SyncNewUser执行成功:" + result.ToString() + "条";
                }
                else
                {
                    //写入日志
                    logEntity.ExecuteResult = -1;
                    logEntity.ExecuteResultJson = "同步程序SyncNewUser执行失败";

                }
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = "SyncNewUser从主数据同步新增用户时出错：" + ex.Message;
                throw new Exception("SyncNewUser从主数据同步新增用户时出错：" + ex.Message);

            }
            finally
            {
                logServer.WriteLog(logEntity);
            }
        }

        /// <summary>
        /// 描述:用户更新
        /// 作者:姚栋
        /// 日期:2018.06.04
        /// </summary>
        public void SyncUpdateUser()
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 3;
            logEntity.OperateTypeId = ((int)OperationType.SyncData).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.SyncData);
            logEntity.OperateAccount = "WinDbSyncSerivce";
            logEntity.OperateUserId = "WinDbSyncSerivce";
            logEntity.Module = "WinDbSyncSerivce";
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append(@"
                        update BaoLi_User set 
                        BaoLi_User.UserId=Sync_User.UserID,--用户GUID
                        BaoLi_User.DeleteMark  =case when DataStatus=0 then 1 when DataStatus=1 then 0 else 0 end, 
                        BaoLi_User.EnCode=Sync_User.CodeName,--用户编号
                        BaoLi_User.Account=Sync_User.LoginName,--用户登录名
                        BaoLi_User.EmployeeNum=Sync_User.EmployeeNum,--员工工号
                        BaoLi_User.RealName=Sync_User.FullName,--用户显示名称
                        BaoLi_User.NickName=Sync_User.FullName,--用户显示名称
                        BaoLi_User.FirstName=Sync_User.FirstName,--姓
                        BaoLi_User.LastName=Sync_User.LastName,--名
                        BaoLi_User.UserType=Sync_User.UserType,--员工类型
                        BaoLi_User.Email=Sync_User.Email,--邮箱
                        BaoLi_User.Mobile=Sync_User.MobilePhone,--手机号码
                        BaoLi_User.Telephone=Sync_User.BusinessTelephone,--办公电话
                        BaoLi_User.Birthday=Sync_User.BirthDay,--出生日期
                        BaoLi_User.CredentialNum=Sync_User.CredentialNum,--身份证号码
                        BaoLi_User.HZNum=Sync_User.HZNum,--护照号码                          
                        BaoLi_User.ModifyUserName=getdate(),--修改时间
                        BaoLi_User.SyncTime=getdate(),--同步时间
                        BaoLi_User.SourceSys='主数据同步程序',--同步来源
                        BaoLi_User.SourceID=Sync_User.UserId,--同步来源数据主键
                        BaoLi_User.F1=Sync_User.F1,--备用字段1
                        BaoLi_User.F2=Sync_User.F2,--备用字段2
                        BaoLi_User.F3=Sync_User.F3--备用字段3
                        from OPENQUERY(Link_MDM_MiddleBase,'select * from MDM_MiddleBase.dbo.T_User')  Sync_User 
                        inner join Base_User  BaoLi_User
                        on   BaoLi_User.UserId=Sync_User.UserID
                        and( BaoLi_User.EnCode!=Sync_User.CodeName--用户编号
                        or  BaoLi_User.Account!=Sync_User.LoginName--用户登录名
                        or  BaoLi_User.EmployeeNum!=Sync_User.EmployeeNum--员工工号
                        or  BaoLi_User.RealName!=Sync_User.FullName--用户显示名称
                        or  BaoLi_User.NickName!=Sync_User.FullName--用户显示名称
                        or  BaoLi_User.FirstName!=Sync_User.FirstName--姓
                        or  BaoLi_User.LastName!=Sync_User.LastName--名
                        or BaoLi_User.UserType!=Sync_User.UserType--员工类型
                        or BaoLi_User.Email!=Sync_User.Email--邮箱
                        or BaoLi_User.Mobile!=Sync_User.MobilePhone--手机号码
                        or BaoLi_User.Telephone!=Sync_User.BusinessTelephone--办公电话
                        or BaoLi_User.Birthday!=Sync_User.BirthDay--出生日期
                        or BaoLi_User.CredentialNum!=Sync_User.CredentialNum--身份证号码
                        or BaoLi_User.HZNum!=Sync_User.HZNum--护照号码  
                        or BaoLi_User.DeleteMark!=(case when DataStatus=0 then 1 when DataStatus=1 then 0 else 0 end)                         
                        or BaoLi_User.F1!=Sync_User.F1--备用字段1
                        or BaoLi_User.F2!=Sync_User.F2--备用字段2
                        or BaoLi_User.F3!=Sync_User.F3);--备用字段3");
            try
            {


                var result = this.BaseRepository().ExecuteBySql(sqlInsert.ToString());
                if (result >= 0)
                {
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.ExecuteResultJson = "同步程序SyncUpdateUser执行成功:" + result.ToString() + "条";
                }
                else
                {
                    //写入日志
                    logEntity.ExecuteResult = -1;
                    logEntity.ExecuteResultJson = "同步程序SyncUpdateUser执行失败";

                }
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = "SyncUpdateUser从主数据同步更新用户时出错：" + ex.Message;
                throw new Exception("SyncUpdateUser从主数据同步更新用户时出错：" + ex.Message);

            }
            finally
            {
                logServer.WriteLog(logEntity);
            }
        }

        #endregion
    }
}
