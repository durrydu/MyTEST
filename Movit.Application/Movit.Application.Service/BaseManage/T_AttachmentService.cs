using Movit.Application.Code;
using Movit.Application.Entity;
using Movit.Application.Entity.BaseManage;
using Movit.Application.IService;
using Movit.Application.IService.BaseManage;
using Movit.Data;
using Movit.Data.Repository;
using Movit.Util.Extension;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Movit.Application.Service.BaseManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-06-03 18:38
    /// 描 述：T_Attachment
    /// </summary>
    public class T_AttachmentService : RepositoryFactory, T_AttachmentIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<T_AttachmentEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable<T_AttachmentEntity>().ToList();
        }

        /// <summary>
        /// 作者:姚栋
        /// 日期:2018.06.03
        /// 描述:根据主键以及附件表单类型获取数据
        /// </summary>
        /// <param name="KeyValue">表单主键</param>
        /// <param name="ObjectType">表单分组类型</param>
        /// <returns>附件列表</returns>
        public List<T_AttachmentEntity> GetFormList(string KeyValue, string ObjectType)
        {
            if (string.IsNullOrEmpty(KeyValue) || string.IsNullOrEmpty(ObjectType))
            {
                throw new Exception("获取附件参数缺失GetFormList");
            }
            var expression = LinqExtensions.True<T_AttachmentEntity>();
            expression = expression.And(t => t.ObjectID == KeyValue)
                .And(p => p.ObjectType == ObjectType).And(p => p.DeleteMark == 0);
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.AttachmentName).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_AttachmentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<T_AttachmentEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<T_AttachmentEntity>(keyValue);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, T_AttachmentEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 作者:姚栋
        /// 日期:2018.06.03
        /// 描述：建立表单与附件的关系
        /// </summary>
        /// <param name="formKey">表单主键</param>
        /// <param name="fileLists">建立关系附件列表</param>
        public void MapingFiles(string formKey, List<FileModel> fileLists, IRepository db = null)
        {
            try
            {
                if (db == null)
                {
                    db = this.BaseRepository();
                }
                if (string.IsNullOrEmpty(formKey))
                {
                    throw new Exception("表单主键数据不正确!");
                }
                //TODO  姚栋 这里代码 很有问题 先这里处理吧
                var ObjectType = fileLists.FirstOrDefault();
                var parameter = new List<DbParameter>();
                parameter.Add(DbParameters.CreateDbParameter("@ObjectID", formKey));
                parameter.Add(DbParameters.CreateDbParameter("@ObjectType", ObjectType.ObjectType));
                db.ExecuteBySql("update T_Attachment set DeleteMark=1 where ObjectID=@ObjectID and ObjectType=@ObjectType", parameter.ToArray());
                var currentUser = OperatorProvider.Provider.Current();
                if (fileLists != null && fileLists.Count > 0)
                {
                    //var parameter = new List<DbParameter>();
                    //parameter.Add(DbParameters.CreateDbParameter("@ObjectID", formKey));
                    //parameter.Add(DbParameters.CreateDbParameter("@ObjectType", fileLists[0].ObjectType));
                    //db.ExecuteBySql("update T_Attachment set DeleteMark=1 where ObjectID=@ObjectID and ObjectType=@ObjectType", parameter.ToArray());

                    foreach (var item in fileLists)
                    {
                        if (string.IsNullOrEmpty(item.Id))
                        {

                            continue;
                        }
                        var tempModel = new T_AttachmentEntity()
                        {
                            AttachmentID = item.Id,
                            ObjectID = formKey,
                            ModifyDate = DateTime.Now,
                            ModifyUserId = currentUser.UserId,
                            DeleteMark = 0,
                            ModifyUserName = currentUser.UserName,
                        };
                        db.Update<T_AttachmentEntity>(tempModel);
                    }

                }
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw new Exception(ex.Message);
            }

        }
        #endregion
    }
}