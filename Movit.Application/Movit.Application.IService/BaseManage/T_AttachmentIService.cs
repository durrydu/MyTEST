using Movit.Application.Entity;
using Movit.Application.Entity.BaseManage;
using Movit.Data.Repository;
using Movit.Util.WebControl;
using System.Collections.Generic;

namespace Movit.Application.IService.BaseManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-06-03 18:38
    /// 描 述：T_Attachment
    /// </summary>
    public interface T_AttachmentIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<T_AttachmentEntity> GetList(string queryJson);

        /// <summary>
        /// 作者:姚栋
        /// 日期:2018.06.03
        /// 描述:根据主键以及附件表单类型获取数据
        /// </summary>
        /// <param name="KeyValue">表单主键</param>
        /// <param name="ObjectType">表单分组类型</param>
        /// <returns>附件列表</returns>
        List<T_AttachmentEntity> GetFormList(string KeyValue, string ObjectType);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        T_AttachmentEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, T_AttachmentEntity entity);

        /// <summary>
        /// 作者:姚栋
        /// 日期:2018.06.03
        /// 描述：建立表单与附件的关系
        /// </summary>
        /// <param name="formKey">表单主键</param>
        /// <param name="fileList">建立关系附件列表</param>
        void MapingFiles(string formKey, List<FileModel> fileLis, IRepository db = null);
        #endregion
    }
}