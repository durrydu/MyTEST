using Movit.Application.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2018 ����
    /// �� ��������
    /// �� �ڣ�2018-05-30 10:23
    /// �� ����Base_Issuers
    /// </summary>
    public class Base_IssuersMap : EntityTypeConfiguration<Base_IssuersEntity>
    {
        public Base_IssuersMap()
        {
            #region ������
            //��
            this.ToTable("Base_Issuers");
            //����
            this.HasKey(t => t.IssuersId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
