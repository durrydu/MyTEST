using Movit.Application.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2018 ����
    /// �� ��������
    /// �� �ڣ�2018-05-30 10:23
    /// �� ����Issuers
    /// </summary>
    public class IssuersMap : EntityTypeConfiguration<IssuersEntity>
    {
        public IssuersMap()
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
