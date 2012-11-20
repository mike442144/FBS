using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace ITsds.Web.News.ViewEngine
{
    public class WebFormThemeViewEngine : WebFormViewEngine
    {
        public WebFormThemeViewEngine()
        {
            base.MasterLocationFormats = new string[] {
                "~/Themes/{2}/Views/{1}/{0}.master", 
                "~/Themes/{2}/Views/Shared/{0}.master"
            };
            base.ViewLocationFormats = new string[] { 
                "~/Themes/{2}/Views/{1}/{0}.aspx", 
                "~/Themes/{2}/Views/{1}/{0}.ascx", 
                "~/Themes/{2}/Views/Shared/{0}.aspx", 
                "~/Themes/{2}/Views/Shared/{0}.ascx",
                "~/Views/{1}/{0}.aspx",
                "~/Views/{1}/{0}.ascx",
                "~/Views/Shared/{0}.aspx",
                "~/Views/Shared/{0}.ascx"
            };
            base.PartialViewLocationFormats = new string[] {
                "~/Themes/{2}/Views/{1}/{0}.aspx",
                "~/Themes/{2}/Views/{1}/{0}.ascx",
                "~/Themes/{2}/Views/Shared/{0}.aspx",
                "~/Themes/{2}/Views/Shared/{0}.ascx",
                "~/Views/{1}/{0}.aspx",
                "~/Views/{1}/{0}.ascx",
                "~/Views/Shared/{0}.aspx",
                "~/Views/Shared/{0}.ascx"
            };
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            try
            {
                return System.IO.File.Exists(controllerContext.HttpContext.Server.MapPath(virtualPath));
            }
            catch (HttpException exception)
            {
                if (exception.GetHttpCode() != 0x194)
                {
                    throw;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            string[] strArray;
            string[] strArray2;

            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException("viewName must be specified.", "viewName");
            }

            string themeName = this.GetThemeToUse(controllerContext);

            string requiredString = controllerContext.RouteData.GetRequiredString("controller");

            string viewPath = this.GetPath(controllerContext, this.ViewLocationFormats, "ViewLocationFormats", viewName, themeName, requiredString, "View", useCache, out strArray);
            string masterPath = this.GetPath(controllerContext, this.MasterLocationFormats, "MasterLocationFormats", masterName, themeName, requiredString, "Master", useCache, out strArray2);

            if (!string.IsNullOrEmpty(viewPath) && (!string.IsNullOrEmpty(masterPath) || string.IsNullOrEmpty(masterName)))
            {
                return new ViewEngineResult(this.CreateView(controllerContext, viewPath, masterPath), this);
            }
            return new ViewEngineResult(strArray.Union<string>(strArray2));
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            string[] strArray;
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (string.IsNullOrEmpty(partialViewName))
            {
                throw new ArgumentException("partialViewName must be specified.", "partialViewName");
            }

            string themeName = this.GetThemeToUse(controllerContext);

            string requiredString = controllerContext.RouteData.GetRequiredString("controller");
            string partialViewPath = this.GetPath(controllerContext, this.PartialViewLocationFormats, "PartialViewLocationFormats", partialViewName, themeName, requiredString, "Partial", useCache, out strArray);
            if (string.IsNullOrEmpty(partialViewPath))
            {
                return new ViewEngineResult(strArray);
            }
            return new ViewEngineResult(this.CreatePartialView(controllerContext, partialViewPath), this);

        }

        private string GetThemeToUse(ControllerContext controllerContext)
        {
            string themeName = controllerContext.HttpContext.Application["themeName"] as string;
            if (themeName == null) themeName = "Default";
            return themeName;
        }

        private static readonly string[] _emptyLocations;

        private string GetPath(ControllerContext controllerContext, string[] locations, string locationsPropertyName, string name, string themeName, string controllerName, string cacheKeyPrefix, bool useCache, out string[] searchedLocations)
        {
            searchedLocations = _emptyLocations;
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            if ((locations == null) || (locations.Length == 0))
            {
                throw new InvalidOperationException("locations must not be null or emtpy.");
            }

            bool flag = IsSpecificPath(name);
            string key = this.CreateCacheKey(cacheKeyPrefix, name, flag ? string.Empty : controllerName, themeName);
            if (useCache)
            {
                string viewLocation = this.ViewLocationCache.GetViewLocation(controllerContext.HttpContext, key);
                if (viewLocation != null)
                {
                    return viewLocation;
                }
            }
            if (!flag)
            {
                return this.GetPathFromGeneralName(controllerContext, locations, name, controllerName, themeName, key, ref searchedLocations);
            }
            return this.GetPathFromSpecificName(controllerContext, name, key, ref searchedLocations);
        }

        private static bool IsSpecificPath(string name)
        {
            char ch = name[0];
            if (ch != '~')
            {
                return (ch == '/');
            }
            return true;
        }

        private string CreateCacheKey(string prefix, string name, string controllerName, string themeName)
        {
            return string.Format(CultureInfo.InvariantCulture, ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}", new object[] { base.GetType().AssemblyQualifiedName, prefix, name, controllerName, themeName });
        }

        private string GetPathFromGeneralName(ControllerContext controllerContext, string[] locations, string name, string controllerName, string themeName, string cacheKey, ref string[] searchedLocations)
        {
            string virtualPath = string.Empty;
            searchedLocations = new string[locations.Length];
            for (int i = 0; i < locations.Length; i++)
            {
                string str2 = string.Format(CultureInfo.InvariantCulture, locations[i], new object[] { name, controllerName, themeName });

                if (this.FileExists(controllerContext, str2))
                {
                    searchedLocations = _emptyLocations;
                    virtualPath = str2;
                    this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
                    return virtualPath;
                }
                searchedLocations[i] = str2;
            }
            return virtualPath;
        }

        private string GetPathFromSpecificName(ControllerContext controllerContext, string name, string cacheKey, ref string[] searchedLocations)
        {
            string virtualPath = name;
            if (!this.FileExists(controllerContext, name))
            {
                virtualPath = string.Empty;
                searchedLocations = new string[] { name };
            }
            this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
            return virtualPath;
        }
    }
}
