using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Data.Common;

namespace FBS.Domain.QueryObject
{
    public class ObjectReader<T> : IEnumerable<T>, IEnumerable where T : class
    {
        Enumerator enumerator;

        public ObjectReader(DbDataReader reader)
        {
            this.enumerator = new Enumerator(reader);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Enumerator e = this.enumerator;
            if (e == null)
            {
                throw new InvalidOperationException("Cannot enumerate more than once");
            }
            this.enumerator = null;
            return e;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        class Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            DbDataReader reader;
            //FieldInfo[] fields;
            //int[] fieldLookup;
            T current;

            Type type;
            MethodInfo method;
            internal Enumerator(DbDataReader reader)
            {
                this.reader = reader;
                //this.fields = typeof(T).GetFields();

                //反射获得函数
                type = typeof(T);
                method = type.GetMethod("CreateFromReader");
            }

            public T Current
            {
                get { return this.current; }
            }

            object IEnumerator.Current
            {
                get { return this.current; }
            }

            public bool MoveNext()
            {
                if (this.reader.Read())
                {
                    T instance = (T)method.Invoke(null, new object[1] { this.reader });
                    this.current = instance;
                    //if (this.fieldLookup == null)
                    //{
                    //    this.InitFieldLookup();
                    //}
                    //T instance = new T();
                    //for (int i = 0, n = this.fields.Length; i < n; i++)
                    //{
                    //    int index = this.fieldLookup[i];
                    //    if (index >= 0)
                    //    {
                    //        FieldInfo fi = this.fields[i];
                    //        if (this.reader.IsDBNull(index))
                    //        {
                    //            fi.SetValue(instance, null);
                    //        }
                    //        else
                    //        {
                    //            fi.SetValue(instance, this.reader.GetValue(index));
                    //        }
                    //    }
                    //}
                    //this.current = instance;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
            }

            public void Dispose()
            {
                this.reader.Dispose();
            }

            private void InitFieldLookup()
            {
                //Dictionary<string, int> map = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
                //for (int i = 0, n = this.reader.FieldCount; i < n; i++)
                //{
                //    map.Add(this.reader.GetName(i), i);
                //}
                //this.fieldLookup = new int[this.fields.Length];
                //for (int i = 0, n = this.fields.Length; i < n; i++)
                //{
                //    int index;
                //    if (map.TryGetValue(this.fields[i].Name, out index))
                //    {
                //        this.fieldLookup[i] = index;
                //    }
                //    else
                //    {
                //        this.fieldLookup[i] = -1;
                //    }
                //}
            }
        }
    }
}
