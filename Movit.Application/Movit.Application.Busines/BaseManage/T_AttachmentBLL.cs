using Movit.Application.Entity;
using Movit.Application.IService;
using Movit.Application.Service;
using Movit.Util.WebControl;
using System.Collections.Generic;
using System;
using Movit.Application.Service.BaseManage;
using Movit.Application.IService.BaseManage;
using Movit.Application.Entity.BaseManage;

namespace Movit.Application.Busines.BaseManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-06-03 18:38
    /// 描 述：T_Attachment
    /// </summary>
    public class T_AttachmentBLL
    {
        private T_AttachmentIService service = new T_AttachmentService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<T_AttachmentEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
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
            return service.GetFormList(KeyValue, ObjectType);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_AttachmentEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
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
                service.RemoveForm(keyValue);
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
                service.SaveForm(keyValue, entity);
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
        /// <param name="fileList">建立关系附件列表</param>
        public void MapingFiles(string formKey, List<FileModel> fileLis)
        {
            try
            {
                service.MapingFiles(formKey, fileLis);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}