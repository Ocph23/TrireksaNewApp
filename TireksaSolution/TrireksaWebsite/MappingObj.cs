﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TrireksaWebsite
{/*
    public class MappingObj
    {
        private EntityInfo entityParent;
        private List<ColumnInfo> ReaderSchema;

        public MappingObj(EntityInfo entity)
        {
            this.entityParent = entity;
        }
        public List<T> MappingWithoutInclud<T>(IDataReader dr)
        {
            ReaderSchema = MappingCommaon.ReadColumnInfo(dr.GetSchemaTable());

            List<T> list = new List<T>();

            while (dr.Read())
            {
                T obj = default(T);
                obj = Activator.CreateInstance<T>();
                list.Add(WriteColumnMappings<T>(obj, dr));
            }
            return list;
        }
        public T WriteColumnMappings<T>(object obj, IDataReader dr)
        {
            foreach (var property in entityParent.Properties)
            {
                var columnMapping = entityParent.GetAttributDbColumn(property);

                if (columnMapping != null)
                {
                    var field = ReaderSchema.Where(O => O.ColumnName.ToString().ToUpper() == columnMapping.ToString().ToUpper()).FirstOrDefault();
                    if (field != null)
                    {
                        var value = dr.GetValue(field.Ordinal);
                        if (value is DBNull)
                            value = null;
                        property.SetValue(obj, this.GetValue(property, value));
                    }
                }
            }

            return (T)obj;
        }

        public IList MappingWithoutInclud(IDataReader dr, Type T)
        {
            List<dynamic> list = new List<dynamic>();

            while (dr.Read())
            {
                var item = Activator.CreateInstance(T);
                list.Add(WriteColumnMappings(item, dr));
            }
            return (IList)list;
        }

        public object WriteColumnMappings(object obj, IDataReader dr)
        {
            EntityInfo entitychild = new EntityInfo(obj.GetType());

            foreach (var property in entitychild.Properties)
            {
                var columnMapping = entitychild.GetAttributDbColumn(property);

                if (columnMapping != null)
                {
                    var ordinat = dr.GetOrdinal(columnMapping.ToString());
                    var value = dr.GetValue(ordinat);
                    if (value is DBNull)
                        value = null;
                    property.SetValue(obj, ConverValue(property, value), null);
                }
            }

            return obj;
        }



        private object GetValue(PropertyInfo property, object p)
        {
            object result = new object();

            if (property.PropertyType.IsEnum)
            {
                var ass = Enum.Parse(property.PropertyType, (string)p, true);

                result = Enum.ToObject(property.PropertyType, Convert.ToUInt64(ass));//
            }
            else
                result = ConverValue(property, p);

            return result;
        }

        public T MappingRowMultiTable<T, T1>(IDataReader dr) where T : class
        {
            T obj = Activator.CreateInstance<T>();
            foreach (var property in entityParent.Properties)
            {

                var columnMapping = entityParent.GetAttributDbColumn(property);

                if (columnMapping != null)
                {
                    var ordinat = dr.GetOrdinal(columnMapping.ToString());
                    property.SetValue(obj, dr.GetValue(ordinat), null);
                }

                var columnMappingcolection = entityParent.GetCollectionAttribute(property);
                if (columnMappingcolection != null)
                {
                    List<T1> list = new List<T1>();
                    T1 t1 = Activator.CreateInstance<T1>();
                    //    t1 = WriteColumnMappings<T1>(t1, dr);
                    list.Add(t1);
                }
            }
            return obj;
        }

        public List<T> WriteColumnMappingsMulti<T, T1>(IDataReader dr, IEnumerable<ColumnInfo> enumerable, List<T> ListParet)
            where T : class
            where T1 : class

        {
            EntityInfo entitychild = new EntityInfo(typeof(T1));

            if (ListParet.Count < 1)
            {
                T obj = Activator.CreateInstance<T>();

                foreach (var property in entityParent.Properties)
                {
                    var columnMapping = entityParent.GetAttributDbColumn(property);
                    if (columnMapping != null)
                    {
                        var ordinat = dr.GetOrdinal(columnMapping.ToString());
                        property.SetValue(obj, dr.GetValue(ordinat), null);
                    }

                    var columnMappingcolection = entityParent.GetCollectionAttribute(property);
                    if (columnMappingcolection != null)
                    {
                        List<T1> list = new List<T1>();
                        T1 t1 = Activator.CreateInstance<T1>();
                        t1 = WriteColumnMappings<T1>(t1, dr);
                        list.Add(t1);
                        property.SetValue(obj, list, null);

                    }
                }
                ListParet.Add(obj);
            }
            else
            {
                T obj = Activator.CreateInstance<T>();
                PropertyInfo property = null;
                string columnName = string.Empty;
                var pk = entityParent.GetAttributPrimaryKeyName();
                if (pk != null)
                {
                    columnName = pk;
                    property = entityParent.PrimaryKeyProperty;
                }

                if (property != null)
                {
                    T t = Activator.CreateInstance<T>();
                    bool newItem = false;
                    foreach (var item in ListParet)
                    {
                        var value = property.GetValue(item);
                        int ordinal = dr.GetOrdinal(columnName);
                        var readervalue = dr.GetValue(ordinal);
                        if (value.ToString() == readervalue.ToString())
                        {
                            t = item;
                        }
                        else
                        {
                            t = WriteColumnMappings<T>(t, dr) as T;
                            newItem = true;
                        }
                    }


                    if (t != null && newItem == false)
                    {
                        T1 t1 = Activator.CreateInstance<T1>();
                        var result = WriteColumnMappings<T1>(t1, dr);

                        var entity = new EntityInfo(typeof(T));

                        var propcollection = entityParent.GetPropertyIsCollection()[0];
                        var item = propcollection.GetValue(t) as List<T1>;
                        item.Add(t1);
                        propcollection.SetValue(t, item, null);

                    }
                    if (t != null && newItem == true)
                    {
                        T1 t1 = Activator.CreateInstance<T1>();
                        var result = WriteColumnMappings<T1>(t1, dr);

                        var propcollection = entityParent.GetPropertyIsCollection()[0];
                        var item = propcollection.GetValue(t) as List<T1>;
                        if (item == null)
                            item = new List<T1>();
                        item.Add(t1);
                        propcollection.SetValue(t, item, null);
                        SetListValue(t, ListParet);
                    }
                }
            }
            return ListParet;
        }

        private static void SetListValue<T>(T t, List<T> ListParet)
        {
            ListParet.Add(t); ;
        }

        private static object ConverValue(System.Reflection.PropertyInfo p, object obj)
        {
            var propertyType = p.PropertyType;
            switch (propertyType.Name)
            {
                case "Boolean":
                    obj = Convert.ToBoolean(obj);
                    break;

                case "Image":
                    obj = GetImage(obj);
                    break;
                default:
                    obj = Convert.ChangeType(obj, p.PropertyType);
                    break;
            }

            return obj;
        }

        private static object GetImage(object obj)
        {

            if (obj is DBNull)
            {
                obj = null;
            }
            //else
            //{
            //    try
            //    {
            //        MemoryStream ms = new MemoryStream((byte[])obj);
            //        Image returnImage = Image.FromStream(ms);
            //        return returnImage;
            //    }
            //    catch (Exception ex)
            //    {

            //        throw new SystemException(ex.Message);
            //    }
            //}

            return obj;
        }



    }


    public class EntityInfo
    {
        private Type type;

        public PropertyInfo[] Properties { get { return type.GetProperties(); } }
        public PropertyInfo PrimaryKeyProperty = null;
        public List<PropertyInfo> ForeignKeyProperty = new List<PropertyInfo>();
        public List<PropertyInfo> DbTableProperty = new List<PropertyInfo>();

        public EntityInfo(Type typeOfEntity)
        {
            this.type = typeOfEntity;
            this.SetAttribut();
        }

        private void SetAttribut()
        {
            foreach (PropertyInfo p in Properties)
            {
                var pk = p.GetCustomAttribute(typeof(PrimaryKeyAttribute));

                if (pk != null)
                {
                    PrimaryKeyProperty = p;
                };

                var fk = p.GetCustomAttribute(typeof(ForeignKeyAttribute));
                if (fk != null)
                    this.ForeignKeyProperty.Add(p);


                var dbtable = p.GetCustomAttribute(typeof(DbColumnAttribute));
                if (dbtable != null)
                    this.DbTableProperty.Add(p);

            }
        }

        public string TableName
        {
            get
            {
                TableNameAttribute att = (TableNameAttribute)type.GetCustomAttributes(typeof(TableNameAttribute), true)[0];
                return att.Name;

            }
        }


        public string GetAttributPrimaryKeyName()
        {
            PrimaryKeyAttribute pk = (PrimaryKeyAttribute)PrimaryKeyProperty.GetCustomAttribute(typeof(PrimaryKeyAttribute));
            return pk.Name;
        }


        public string GetAttributForeignKeyName(int index)
        {
            var prop = this.ForeignKeyProperty[index];
            ForeignKeyAttribute fk = (ForeignKeyAttribute)PrimaryKeyProperty.GetCustomAttribute(typeof(ForeignKeyAttribute));
            return fk.Name;
        }

        public Type GetEntityType()
        {
            return type;
        }

        internal object GetAttributDbColumn(PropertyInfo property)
        {
            DbColumnAttribute field = (DbColumnAttribute)property.GetCustomAttribute(typeof(DbColumnAttribute));

            if (field != null)
                return field.Name;
            else
                return null;
        }

        internal string GetCollectionAttribute(PropertyInfo property)
        {
            CollectionAttribute field = (CollectionAttribute)property.GetCustomAttribute(typeof(CollectionAttribute));
            return field.Name;
        }

        public List<PropertyInfo> GetPropertyIsCollection()
        {
            List<PropertyInfo> list = new List<PropertyInfo>();
            foreach (PropertyInfo p in Properties.Where(O => O.PropertyType.GenericTypeArguments.Count() > 0))
            {
                list.Add(p);
            }
            return list;
        }

        internal PropertyInfo GetPropertyByPropertyName(string p)
        {
            return this.Properties.Where(O => O.Name == p).FirstOrDefault();
        }


        internal object GetAttributeForeignKeyByParentType(Type typeparent)
        {
            object result = new object();
            foreach (PropertyInfo p in ForeignKeyProperty)
            {
                var res = p.GetCustomAttribute<ForeignKeyAttribute>(false);
                if (res.TypeOfParent == typeparent)
                {
                    result = res;
                }
            }

            return result;
        }
    }
    */
}
