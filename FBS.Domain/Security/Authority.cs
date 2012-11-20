using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Principal;
using System.Xml;
using FBS.Utils;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Domain.Security
{
    public class AuthorityManager
    {
        /// <summary>
        /// 
        /// </summary>
        public AuthorityManager() 
        {

        }

        /// <summary>
        /// 从指定的xml中检查指定的角色是否具有使用Task的权限
        /// </summary>
        /// <param name="taskName">Task名称</param>
        /// <param name="roles">角色</param>
        /// <param name="doc">检查依据文件</param>
        /// <returns>是否具有操作权限</returns>
        private static bool AccessCheck(string taskName,string[] roles,XmlDocument doc)
        {
            bool IsPermissiable=false;
            
            //用户角色
            if (roles != null&&!string.IsNullOrEmpty(roles[0])&&doc!=null)
            {
                XmlNode root = doc.DocumentElement;
                XmlNodeList roleNodes, taskNodes;
                for (int i = 0; i < roles.Length; i++)
                {
                    roleNodes = root.SelectNodes("Roles/Role[@Name='" + roles[i] + "']");

                    if (roleNodes != null)
                    {
                        taskNodes = roleNodes.Item(0).SelectNodes("Task[@Name='" + taskName + "']");
                        if (taskNodes.Count != 0)
                        {
                            IsPermissiable = true; break;
                        }
                    }
                }
            }

            return IsPermissiable;
        }

        /// <summary>
        /// 检查是否具有指定Task的操作权限
        /// </summary>
        /// <param name="taskName">Task的名称</param>
        public static void PermissionCheck(string taskName)
        {
            XmlDocument doc = LoadXml();
            string[] roles = GetUserRoles();

            if (roles == null)
            {
                throw new AccessForbiddenException("");
            }

            if (!AccessCheck(taskName, roles, doc))
                throw new ActionForbiddenException("访问被拒绝，当前用户不具有操作此功能的权限！");
        }

        /// <summary>
        /// 加载配置角色与权限的Xml文档
        /// </summary>
        /// <returns>Xml文档</returns>
        public static XmlDocument LoadXml()
        {
            
            XmlDocument doc=null;
            if (HttpContext.Current != null)
            {
                doc = (XmlDocument)HttpContext.Current.Cache["fbs_PermissionsXml"];
                if (null == doc)
                {
                    string fileName = HttpContext.Current.Server.MapPath("~/App_Data/Permissions.xml");
                    doc = new XmlDocument();
                    doc.Load(fileName);

                    HttpContext.Current.Cache.Insert("fbs_PermissionsXml", doc, new System.Web.Caching.CacheDependency(fileName));
                }
            }
            return doc;
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns>角色名数组</returns>
        public static string[] GetUserRoles()
        {
            string[] roles = null;

            //用户仓储
            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            Account acc = null;
            if (HttpContext.Current != null)
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    acc = accRep.GetByKey(new Guid(HttpContext.Current.User.Identity.Name));
                    if (acc != null)
                        roles = acc.Roles.Split('|');
                }
            return roles;
        }

        public System.Security.Principal.IIdentity Identity
        {
            get 
            {
                IIdentity id = null;
                if (HttpContext.Current == null)
                    id = null;
                else if (HttpContext.Current.Request.IsAuthenticated)
                        id = HttpContext.Current.User.Identity;
                return id;
            }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }


        
    }
}
