using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CardapioDigital.Persistencia.InfraNH
{
    // CustomTableNameConvention
    public class CustomTableNameConvention : IClassConvention, IClassConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IClassInspector> criteria)
        {
            criteria.Expect(x => x.TableName, Is.Not.Set);
        }

        public void Apply(IClassInstance instance)
        {
            instance.Table(instance.EntityType.Name);
        }
    }

    // CustomPrimaryKeyConvention
    public class CustomPrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            //instance.Column("Codigo");
            
            instance.Column("Codigo" + instance.EntityType.Name);
            
            //instance.GeneratedBy.HiLo(HiLoRange.ToString());
            //instance.GeneratedBy.HiLo("NH_HiLo", "NextHi", "1000", "TableKey = ''Product''");
        }
    }

    // CustomForeignKeyConvention
    public class CustomForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member member, Type type)
        {
            if (member == null)
                return "Codigo" + type.Name;  // many-to-many, one-to-many, join

            return "Codigo" + member.Name; // many-to-one
        }
    }

    // JoinedSubclassNameConvention
    public class CustomJoinedSubclassConvention : IJoinedSubclassConvention
    {
        public void Apply(IJoinedSubclassInstance instance)
        {
            if (instance.Key.EntityType != null)
                instance.Key.Column("Codigo" + instance.Key.EntityType.Name);

            instance.Key.Column("Codigo" + instance.Type.Name);
        }
    }

    // ForeignKeyConstraintNameConvention
    public class CustomForeignKeyConstraintOneToManyConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.ForeignKey(string.Format("FK_{0}_{1}", instance.OtherSide.EntityType.Name, instance.EntityType.Name));
        }
    }

    // CustomManyToManyTableNameConvention
    public class CustomManyToManyTableNameConvention : ManyToManyTableNameConvention
    {
        protected override string GetBiDirectionalTableName(IManyToManyCollectionInspector collection, IManyToManyCollectionInspector otherSide)
        {
            return otherSide.EntityType.Name + collection.EntityType.Name;
        }

        protected override string GetUniDirectionalTableName(IManyToManyCollectionInspector collection)
        {
            return collection.EntityType.Name + collection.ChildType.Name;
        }
    }

    // StringColumnLengthConvention
    public class StringColumnLengthConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Type == typeof(string))
                    .Expect(x => x.Length == 0);
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.Length(100);
        }
    }

    // ColumnNullabilityConvention
    public class ColumnNullabilityConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Nullable, Is.Not.Set);
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.Not.Nullable();
        }
    }
}
