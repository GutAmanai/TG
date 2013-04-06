﻿using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace br.persistencia.tg.Infra
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
            instance.Column("Id" + instance.EntityType.Name);
        }
    }

    public class OraclePrimaryKeySequenceConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            var sequenceName = string.Format("SEQ_{0}", instance.EntityType.Name);
            sequenceName = sequenceName.Length > 30 ? sequenceName.Substring(0, 30) : sequenceName;
            instance.GeneratedBy.Sequence(sequenceName);
        }
    }

    // CustomForeignKeyConvention
    public class CustomForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member member, Type type)
        {
            if (member == null)
                return "Id" + type.Name;  // many-to-many, one-to-many, join

            return "Id" + type.Name; // many-to-one
        }
    }

    // JoinedSubclassNameConvention
    public class CustomJoinedSubclassConvention : IJoinedSubclassConvention
    {
        public void Apply(IJoinedSubclassInstance instance)
        {
            if (instance.Key.EntityType != null)
                instance.Key.Column("Id" + instance.Key.EntityType.Name);

            instance.Key.Column("Id" + instance.Type.Name);
        }
    }

    // ForeignKeyConstraintNameConvention
    public class CustomForeignKeyConstraintOneToManyConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            if (instance.OtherSide != null)
                if (string.IsNullOrWhiteSpace(((IKeyInspector)instance.Key).ForeignKey))
                    instance.Key.ForeignKey(string.Format("FK_{0}_{1}", instance.OtherSide.EntityType.Name, instance.EntityType.Name));
        }
    }

    //// ForeignKeyConstraintNameConvention HasManyToMany
    //public class CustomForeignKeyConstraintReferencesConvention : IReferenceConvention
    //{
    //    public void Apply(IManyToOneInstance instance)
    //    {
    //            if (string.IsNullOrWhiteSpace(((IManyToOneInspector)instance).ForeignKey))
    //                instance.ForeignKey(string.Format("FK_{0}_{1}", instance.EntityType.Name, instance..EntityType.Name));
    //    }
    //}


    // ForeignKeyConstraintNameConvention HasManyToMany
    public class CustomForeignKeyConstraintHasManyToManyConvention : IHasManyToManyConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {
            if (instance.OtherSide != null)
                if (string.IsNullOrWhiteSpace(((IKeyInspector)instance.Key).ForeignKey))
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

    // StringPropertyConvention
    public class StringPropertyConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            if (instance.Property.PropertyType == typeof(string))
                instance.CustomType("AnsiString");
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
