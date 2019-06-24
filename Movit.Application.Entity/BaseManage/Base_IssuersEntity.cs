using System;
namespace Movit.Application.Entity
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2018 �������(����)
    /// �� ��������
    /// �� �ڣ�2018-05-30 10:23
    /// �� ����Base_Issuers
    /// </summary>
    public class Base_IssuersEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// IssuersId
        /// </summary>
        /// <returns></returns>
        public string IssuersId { get; set; }
        /// <summary>
        /// IssuerName
        /// </summary>
        /// <returns></returns>
        public string IssuerName { get; set; }
        /// <summary>
        /// IssuersCode
        /// </summary>
        /// <returns></returns>
        public string IssuersCode { get; set; }
        /// <summary>
        /// DeleteMark
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CreateUserId
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// ModifyUserId
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// ModifyUserName
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
            this.IssuersId = Guid.NewGuid().ToString();
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
            this.IssuersId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}