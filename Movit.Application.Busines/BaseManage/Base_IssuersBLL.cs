using Movit.Application.Entity;
using Movit.Application.IService;
using Movit.Application.Service;
using Movit.Util.WebControl;
using System.Collections.Generic;
using System;

namespace Movit.Application.Busines
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201�������(����)
    /// �� ��������
    /// �� �ڣ�2018-05-30 10:23
    /// �� ����Base_Issuers
    /// </summary>
    public class Base_IssuersBLL
    {
        private Base_IssuersIService service = new Base_IssuersService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Base_IssuersEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Base_IssuersEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Base_IssuersEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
