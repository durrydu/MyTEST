using Movit.Application.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Movit.Application.Mapping
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2018 ����
    /// �� ������������Ա
    /// �� �ڣ�2018-07-10 09:16
    /// �� ������Ź����
    /// </summary>
    public class Base_CodeRuleMap : EntityTypeConfiguration<Base_CodeRuleEntity>
    {
        public Base_CodeRuleMap()
        {
            #region ������
            //��
            this.ToTable("Base_CodeRule");
            //����
            this.HasKey(t => t.RuleId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
