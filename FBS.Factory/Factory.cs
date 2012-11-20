using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Web;

namespace FBS.Factory
{
    public class Factory<T>
    {
        public static T GetConcrete<TEntity>()
        {
            //对象类型
            Type type = null;

            //类型名称
            string typeName = typeof(T).FullName;

            //类名称
            string className = typeName.Substring(0, typeName.IndexOf('['));

            //缓存键值
            string cacheKey = null;

            //程序集
            string assembly = ConfigurationManager.AppSettings[className].ToString();


           

            cacheKey = typeName;

            //检查缓存
            if (null == EntLibHelper.EntLibHelper.GetCachedObject(cacheKey))
            {
                try
                {

                    type = Type.GetType(assembly);
                    
                    //指定泛型
                    type = type.MakeGenericType(typeof(TEntity));

                    Type[] typeParams = System.Type.EmptyTypes;
                    T a = (T)type.GetConstructor(typeParams).Invoke(new object[0]);
                    //存入缓存
                    EntLibHelper.EntLibHelper.StoreInCache(cacheKey, a);
                    //EntLibHelper.EntLibHelper.StoreInCache(cacheKey, a, HttpContext.Current.Server.MapPath("~/Web.config"));
                }
                catch (Exception error)
                {
                   // throw new Exception("加载对象出错:"+error.Message, error);
                }
            }

            return (T)EntLibHelper.EntLibHelper.GetCachedObject(cacheKey);
        }

        public static T GetConcrete()
        {
            //对象类型
            Type type = null;

            //类型名称
            string typeName = typeof(T).FullName;
            //缓存键值
            string cacheKey = null;

            //程序集
            string assembly = ConfigurationManager.AppSettings[typeName].ToString();

            cacheKey = typeName;

            //检查缓存
            if (null == EntLibHelper.EntLibHelper.GetCachedObject(cacheKey))
            {
                try
                {
                    type = Type.GetType(assembly);

                    Type[] typeParams = System.Type.EmptyTypes;
                    ConstructorInfo info = type.GetConstructor(typeParams);
                    T a = (T)info.Invoke(new object[0]);
                    //存入缓存
                    EntLibHelper.EntLibHelper.StoreInCache(cacheKey, a);
                    //EntLibHelper.EntLibHelper.StoreInCache(cacheKey, a, HttpContext.Current.Server.MapPath("~/Web.config"));
                }
                catch (Exception error)
                {
                   // throw new Exception("加载对象出错", error);
                }
            }

            return (T)EntLibHelper.EntLibHelper.GetCachedObject(cacheKey);
        }

        public static T GetConcreteStub()
        {
            //对象类型
            Type type = null;

            //类型名称
            string typeName = typeof(T).FullName;
            //缓存键值
            string cacheKey = null;

            //程序集
            string assembly=ConfigurationManager.AppSettings[typeName].ToString();

            cacheKey = typeName;

            //检查缓存
            if (null == EntLibHelper.EntLibHelper.GetCachedObject(cacheKey))
            {
                try
                {
                    type = Type.GetType(assembly);

                    Type[] typeParams = System.Type.EmptyTypes;

                    //存入缓存
                    EntLibHelper.EntLibHelper.StoreInCache(cacheKey, type.GetConstructor(typeParams));
                }
                catch (Exception error)
                {
                    throw new Exception("加载对象出错",error);
                }
            }

            return (T)((ConstructorInfo)EntLibHelper.EntLibHelper.GetCachedObject(cacheKey)).Invoke(new object[0]);
        }
    }
}
