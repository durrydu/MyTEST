using Movit.Util;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movit.Util.Extension;

namespace BaoLi.Application.Web.Areas.Demo.Controllers
{
    public class DemoController : MvcControllerBase
    {
        #region 视图功能
        // GET: Demo/Demo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult treeIndex()
        {
            return View();
        }
        public ActionResult Form()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
        #endregion
        #region 获取数据

        /// <summary>
        /// 部门列表 
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson()
        {
            #region 构建数据
            List<treeModel> newPlist = new List<treeModel>();
            List<treeModel> plistHead = new List<treeModel>();
            //拼接头
            plistHead.Add(new treeModel()
            {

                id = "temp0",
                ssqy = "上海区域",
                xmmc = "汇总",
                dsjc = "",
                wskzpje = "100",
                parentId = "0"

            });

            List<treeModel> plistHeTong = new List<treeModel>();

            plistHeTong.Add(new treeModel()
            {

                id = "1",
                ssqy = "上海区域",
                xmmc = "阳光城虹越",
                dsjc = "链家",
                wskzpje = "50",
              

            });
            plistHeTong.Add(new treeModel()
            {

                id = "2",
                ssqy = "上海区域",
                xmmc = "阳光城虹越",
                dsjc = "我爱我家",
                wskzpje = "50",
               
            });

            var parentId = "0";
            foreach (var headItem in plistHead)
            {
                newPlist.Add(new treeModel()
                {

                    id = headItem.id,
                    ssqy = headItem.ssqy,
                    xmmc = "汇总",
                    dsjc = "",
                    wskzpje = headItem.wskzpje,
                    parentId = parentId

                });
                var subPlist = plistHeTong.Where(p => p.ssqy == headItem.ssqy).ToList();
                for (int i = 0; i < subPlist.Count; i++)
                {
                    if (i == 0)
                    {
                        parentId = subPlist[i].id;
                        subPlist[i].ssqy = "";
                        subPlist[i].parentId = headItem.parentId;
                       
                    }
                    else
                    {
                        subPlist[i].ssqy = "";
                        subPlist[i].xmmc = "";
                        subPlist[i].parentId = parentId;

                    }
                  
                }
                newPlist.AddRange(subPlist);
                parentId = "0";
            }
          


          
            #endregion

            var treeList = new List<TreeGridEntity>();

            foreach (treeModel item in newPlist)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = newPlist.Count(t => t.parentId == item.id) == 0 ? false : true;
                tree.id = item.id;
                if (item.parentId == "0")
                {
                    tree.parentId = "0";
                }
                else
                {
                    tree.parentId = item.parentId;
                }
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                string itemJson = item.ToJson();
                itemJson = itemJson.Insert(1, "\"Sort\":\"xmmc\",");
                tree.entityJson = itemJson;
                treeList.Add(tree);
            }
            return Content(treeList.TreeJson());
        }
        [HttpGet]
        public ActionResult GetAutocomplete(string query)
        {
            AutocompleteResult tResult = new AutocompleteResult();
            var tempresult = new DemoBll().GetDepartmentList();
            var temp = (from a in tempresult
                        where a.DepartpmentName.Contains(query)
                        select new AutocompleteEntity
                        {
                            value = a.DepartpmentName,
                            data = a.DepartpmentId
                        }).ToList();
            tResult.suggestions = temp;
            tResult.query = query;
            return ToJsonResult(tResult);
        }
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = new DemoBll().GetEmpList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleList()
        {
            var data = new DemoBll().GetRoleList();
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取部门树形列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDepartmentTreeJson()
        {
            var treeList = new List<TreeEntity>();
            var data = new DemoBll().GetDepartmentList();
            foreach (var item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.DepartpmentId) == 0 ? false : true;
                tree.id = item.DepartpmentId;
                tree.text = item.DepartpmentName;
                tree.value = item.DepartpmentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleJson()
        {
            var data = new DemoBll().GetRoleList();
            return Content(data.ToJson());
        }
        #endregion
        #region 提交数据
        #endregion
        #region 验证数据
        #endregion

    }

    public class treeModel
    {
        public string id { get; set; }
        public string ssqy { get; set; }
        public string xmmc { get; set; }
        public string dsjc { get; set; }
        public string wskzpje { get; set; }
        public string parentId { get; set; }
    }

    public class DemoBll
    {
        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<EmpEntity> GetEmpList(Pagination pagination, string queryJson)
        {
            List<EmpEntity> result = new List<EmpEntity>();
            for (int i = 0; i < 40; i++)
            {

                result.Add(new EmpEntity() { EmpId = "s" + i, EmpName = "name" + i, EmpAge = i, Address = "address" + i, DepartmentId = ((i / 6) + 1).ToString(), RoleId = ((i / 21) + 1).ToString() });
            }
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
                result = result.Where(t => t.EmpName.Contains(keyord)).ToList();
            }
            if (!queryParam["departmentId"].IsEmpty())
            {
                string DepartmentId = queryParam["departmentId"].ToString();
                result = result.Where(t => t.DepartmentId == DepartmentId).ToList();
            }
            if (!queryParam["roleId"].IsEmpty())
            {
                string RoleId = queryParam["roleId"].ToString();
                result = result.Where(t => t.RoleId == RoleId).ToList();
            }
            pagination.records = result.Count();
            var tempresult = result.Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);

            return tempresult;
        }
        public IEnumerable<DepartpmentEntity> GetDepartmentList()
        {

            var result = new List<DepartpmentEntity>();

            result.Add(new DepartpmentEntity() { DepartpmentId = "1", DepartpmentName = "光明集团", ParentId = "0" });
            result.Add(new DepartpmentEntity() { DepartpmentId = "2", DepartpmentName = "光明领导", ParentId = "1" });
            result.Add(new DepartpmentEntity() { DepartpmentId = "3", DepartpmentName = "总师部", ParentId = "2" });
            result.Add(new DepartpmentEntity() { DepartpmentId = "4", DepartpmentName = "监察部", ParentId = "2" });
            result.Add(new DepartpmentEntity() { DepartpmentId = "5", DepartpmentName = "子公司", ParentId = "2" });
            result.Add(new DepartpmentEntity() { DepartpmentId = "6", DepartpmentName = "建设方A", ParentId = "4" });
            result.Add(new DepartpmentEntity() { DepartpmentId = "7", DepartpmentName = "建设方B", ParentId = "4" });
            return result;
        }
        public IEnumerable<RoleEntity> GetRoleList()
        {
            var result = new List<RoleEntity>();

            result.Add(new RoleEntity() { RoleId = "1", RoleName = "超级管理员" });
            result.Add(new RoleEntity() { RoleId = "2", RoleName = "管理员" });

            return result;
        }
        public EmpEntity GetEmpModel()
        {

            return null;
        }
    }

    /// <summary>
    /// 员工实体
    /// </summary>
    public class EmpEntity
    {

        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public int EmpAge { get; set; }
        public string Address { get; set; }
        public string DepartmentId { get; set; }
        public string RoleId { get; set; }
    }

    /// <summary>
    /// 部门实体
    /// </summary>
    public class DepartpmentEntity
    {
        public string DepartpmentId { get; set; }
        public string DepartpmentName { get; set; }
        public string ParentId { get; set; }
    }
    /// <summary>
    /// 角色实体
    /// </summary>
    public class RoleEntity
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}