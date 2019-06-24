using System;
using Movit.Application.Code;

using SqlSugar;
namespace Movit.Application.Entity
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2018 �������(����)
    /// �� ������������Ա
    /// �� �ڣ�2018-07-10 09:16
    /// �� ������Ź����
    /// </summary>
     [SugarTable(��Ź����)]
    public class Base_CodeRuleEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public string RuleId { get; set; }
        /// <summary>
        /// ϵͳ����Id
        /// </summary>
        /// <returns></returns>
        public string ModuleId { get; set; }
        /// <summary>
        /// ϵͳ����
        /// </summary>
        /// <returns></returns>
        public string Module { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string EnCode { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// ��ʽ���ɱ༭���Զ���
        /// </summary>
        /// <returns></returns>
        public int? Mode { get; set; }
        /// <summary>
        /// ��ǰ��ˮ��
        /// </summary>
        /// <returns></returns>
        public string CurrentNumber { get; set; }
        /// <summary>
        /// �����ʽJson
        /// </summary>
        /// <returns></returns>
        public string RuleFormatJson { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ��Ч��־
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public  void Create()
        {
            this.RuleId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public  void Modify(string keyValue)
        {
            this.RuleId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}