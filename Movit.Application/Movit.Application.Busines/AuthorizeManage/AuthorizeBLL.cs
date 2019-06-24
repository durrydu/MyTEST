using Movit.Application.Code;
using Movit.Application.Entity;
using Movit.Application.Entity.AuthorizeManage;
using Movit.Application.Entity.AuthorizeManage.ViewModel;
using Movit.Application.IService.AuthorizeManage;
using Movit.Application.Service.AuthorizeManage;
using Movit.Cache.Factory;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Busines.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.12.5 22:35
    /// 描 述：授权认证
    /// </summary>
    public class AuthorizeBLL
    {
        private IAuthorizeService service = new AuthorizeService();
        private ModuleBLL moduleBLL = new ModuleBLL();
        private ModuleButtonBLL moduleButtonBLL = new ModuleButtonBLL();
        private ModuleColumnBLL moduleColumnBLL = new ModuleColumnBLL();
        private IAuthorizeService<ModuleEntity> serviceT = new AuthorizeService<ModuleEntity>();
        
        /// <summary>
        /// 批量删除岗位项目授权数据
        /// </summary>
        /// <param name="authorizeIds">授权ID主键集合</param>
        public void BatchRemoveForm(string authorizeIds)
        {
            try
            {
                string[] arrayUserId = authorizeIds.Split(',');
                service.BatchRemoveForm(arrayUserId);
            }
            catch (Exception)
            {
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

                service.BatchRemoveFormAll(postId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取授权功能
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetModuleList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleBLL.GetList().FindAll(t => t.EnabledMark.Equals(1) && t.IsPublic == 1);
            }
            else
            {
                return service.GetModuleList(userId).Where(t => t.IsPublic == 1);
            }
        }
        /// <summary>
        /// 描述:根据岗位ID获取已经授权过的项目信息
        /// 作者:姚栋
        /// 日期:2018-05-30
        /// </summary>
        /// <param name="keyValue">角色ID</param>
        public IEnumerable<AuthorizeEntity> GetProjectListByPostId(string keyValue, string queryJson, Pagination pagination)
        {
            return service.GetProjectListByPostId(keyValue, queryJson, pagination);

        }
        /// <summary>
        /// 描述:根据岗位ID获取未授权过的项目信息
        /// 作者:姚栋
        /// 日期:2018-05-30
        /// </summary>
        /// <param name="keyValue">角色ID</param>
        public IEnumerable<Base_ProjectInfoEntity> GetProjectExcept(string keyValue, Pagination pagination, string queryJson)
        {
            return service.GetProjectExcept(keyValue, pagination, queryJson);

        }

        /// <summary>
        /// 获取授权功能按钮
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleButtonEntity> GetModuleButtonList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleButtonBLL.GetList();
            }
            else
            {
                return service.GetModuleButtonList(userId);
            }
        }
        /// <summary>
        /// 获取授权功能视图
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleColumnEntity> GetModuleColumnList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleColumnBLL.GetList();
            }
            else
            {
                return service.GetModuleColumnList(userId);
            }
        }
        /// <summary>
        /// 获取授权功能Url、操作Url
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeUrlModel> GetUrlList(string userId)
        {
            return service.GetUrlList(userId);
        }
        /// <summary>
        /// Action执行权限认证
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="moduleId">模块Id</param>
        /// <param name="action">请求地址</param>
        /// <returns></returns>
        public bool ActionAuthorize(string userId, string moduleId, string action)
        {
            List<AuthorizeUrlModel> authorizeUrlList = new List<AuthorizeUrlModel>();
            var cacheList = CacheFactory.Cache().GetCache<List<AuthorizeUrlModel>>("AuthorizeUrl_" + userId);
            if (cacheList == null)
            {
                authorizeUrlList = this.GetUrlList(userId).ToList();
                CacheFactory.Cache().WriteCache(authorizeUrlList, "AuthorizeUrl_" + userId, DateTime.Now.AddMinutes(1));
            }
            else
            {
                authorizeUrlList = cacheList;
            }
            authorizeUrlList = authorizeUrlList.FindAll(t => t.ModuleId.Equals(moduleId));
            foreach (AuthorizeUrlModel item in authorizeUrlList)
            {
                if (!string.IsNullOrEmpty(item.UrlAddress))
                {
                    string[] url = item.UrlAddress.Split('?');
                    if (item.ModuleId == moduleId && url[0] == action)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 获得权限范围用户ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthorUserId(Operator operators, bool isWrite = false)
        {
            return service.GetDataAuthorUserId(operators, isWrite);
        }
        /// <summary>
        /// 获得权限范围项目ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetReadProjectId(Operator operators, bool isWrite = false)
        {
            return service.GetReadProjectId(operators, isWrite);
        }
        /// <summary>
        /// 获得可读数据权限范围SQL
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthor(Operator operators, bool isWrite = false)
        {
            return service.GetDataAuthor(operators, isWrite);
        }
        public bool LookAll()
        {

            return serviceT.LookAll();
        }
    }
}
