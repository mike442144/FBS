// =====================================================================================
// Copyright ?2005 by Shahed Khan. All rights are reserved.
// 
// If you like this code then feel free to go ahead and use it.
// The only thing I ask is that you don't remove or alter my copyright notice.
//
// Your use of this software is entirely at your own risk. I make no claims or
// warrantees about the reliability or fitness of this code for any particular purpose.
// If you make changes or additions to this code please mark your code as being yours.
// 
// website , email shahed.khan@gmail.com
// =====================================================================================


using System;
using System.Collections.Generic;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using System.Security.Principal;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;


namespace FBS.EntLibHelper
{

    /// <summary>
    /// Helper class for dealing with enterprise library
    /// </summary>
    public class EntLibHelper
    {
        #region Configuration Section Names

        private const string CONFIGURATION_SECTION_NAME = "globalConfiguration";

        private const string EXCEPTION_POLICY_NAME = "Exception Policy";

        private const string GENERAL_CACHE_MANAGER = "Cache Manager";
        //private const string AUTHENTICATION_PROVIDER = "Database Provider";
        private const string CACHING_STORE_PROVIDER = "Caching Store Provider";

        private const string RULE_PROVIDER = "RuleProvider";
        private const string PROFILE_PROVIDER = "Profile Database Provider";
        private const string ROLE_PROVIDER = "Role Database Provider";

        private const string SYMMETRIC_INSTANCE = "DPAPI Symmetric Cryptography Provider";

        //private const string DATABASE_HASH_INSTANCE = "SHA1Managed";
        private const string MD5_HASH_INSTANCE = "MD5CryptoServiceProvider";

        #endregion

        #region Logging

        private const string GENERAL_CATEGORY = "General";
        private const string ERRORWEBSERVICE_CATEGORY = "ErrorWebservice";
        private const string ERRORWEBPAGE_CATEGORY = "ErrorWebPage";

        private const string INFO_CATEGORY = "Info";
        private const string WARN_CATEGORY = "Warn";
        private const string ERROR_CATEGORY = "Error";
        private const string DEBUG_CATEGORY = "Debug";

        private const int INFO_PRIORITY = 4;
        private const int INFO_EVENTID = 0;

        private const int ERROR_PRIORITY = 3;
        private const int ERROR_EVENTID = 0;

        private const int WARN_PRIORITY = 2;
        private const int WARN_EVENTID = 0;

        private const int DEBUG_PRIORITY = 1;
        private const int DEBUG_EVENTID = 0;
            

        private static CacheManager manager = CacheFactory.GetCacheManager(GENERAL_CACHE_MANAGER);
        //private static ConfigurationContext _ConfigurationContext = ConfigurationManager.GetCurrentContext();

        public static void GeneralLog(object message)
        {
            Logger.Write(message);
        }

        public static void GeneralLog(object message, Dictionary<string, object> properties)
        {
            Logger.Write(message, GENERAL_CATEGORY, properties);
        }

        public static void ErrorWebserviceLog(object message, Dictionary<string, object> properties)
        {
            Logger.Write(message, ERRORWEBSERVICE_CATEGORY, properties);
        }

        public static void ErrorLog(object message, Dictionary<string, object> properties)
        {
            Logger.Write(message, ERROR_CATEGORY, properties);
        }

        public static void ErrorWebPageLog(object message, Dictionary<string, object> properties)
        {
            Logger.Write(message, ERRORWEBPAGE_CATEGORY, properties);
        }
                
        public static void Debug(object message)
        {            
            Logger.Write(message, DEBUG_CATEGORY);
        }

        public static void Debug(object message, Dictionary<string, object> properties)
        {
            Logger.Write(message, DEBUG_CATEGORY, properties);
        }

        #endregion

        #region Exception Handling

        public static bool Exception(string title, Exception x)
        {
            ApplicationException wrapped = new ApplicationException(title, x);
            bool rethrow = ExceptionPolicy.HandleException(wrapped, EXCEPTION_POLICY_NAME);
            return rethrow;
        }

        internal static void UnhandledException(Exception x)
        {
            // An unhandled exception occured somewhere in our application. Let
            // the 'Global Policy' handler have a try at handling it.
            try
            {
                bool rethrow = ExceptionPolicy.HandleException(x, "Unhandled Exception");
                if (rethrow)
                {
                    throw x;
                }
            }
            catch
            {
                // Something has gone wrong during HandleException (e.g. incorrect configuration of the block).
                // Exit the application
                string errorMsg = "An unexpected exception occured while calling HandleException with policy 'Global Policy'. ";
                errorMsg += "Please check the event log for details about the exception." + Environment.NewLine + Environment.NewLine;

                throw new Exception(errorMsg);
            }
        }

        #endregion

        #region Caching

        public static void StoreInCache(string key, object data)
        {
            
            if (null == data) manager.Remove(key);
            else manager.Add(key, data);
        }

        public static object GetCachedObject(string key)
        {
            if (manager.Contains(key))
                return manager.GetData(key);
            else
                return null;
        }

        public static void StoreInCache(string key, object value, CacheItemPriority priority, ICacheItemRefreshAction action, ICacheItemExpiration[] expir)
        {
            if (null == value) manager.Remove(key);
            else manager.Add(key, value, priority, action, expir);
        }

        public static void StoreInCache(string key, object value, string file)
        {
            if (null == value) manager.Remove(key);
            else
                manager.Add(key, value, CacheItemPriority.Normal, null, new FileDependency(file));
        }

        public static void RemoveCacheByKey(string key)
        {
            manager.Remove(key);
        }

        #endregion

    }
}
