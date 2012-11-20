/*
 此类为Discuz NT 3.1 版本中的帮助类
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;

namespace FBS.Utils
{
    public class Validator
    {
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object expression)
        {
            if (expression != null)
                return IsNumeric(expression.ToString());

            return false;

        }

        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(string expression)
        {
            if (expression != null)
            {
                string str = expression;
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否为Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDouble(object expression)
        {
            if (expression != null)
                return Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");

            return false;
        }

        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
                return false;

            if (strNumber.Length < 1)
                return false;

            foreach (string id in strNumber)
            {
                if (!IsNumeric(id))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 过滤敏感词
        /// </summary>
        public static bool filter(string str)
        {
            bool u=false;
            string xxx = string.Empty;
            StreamReader m_streamReader = null;
            try
            {

                if (HttpContext.Current.Cache["xxx"] == null)
                {
                    FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/敏感词库大全2.txt"), FileMode.Open);
                    m_streamReader = new StreamReader(fs);
                    //使用StreamReader类来读取文件 
                    xxx = m_streamReader.ReadToEnd();
                    HttpContext.Current.Cache.Insert("words", xxx);
                }
                else
                { 
                    xxx = HttpContext.Current.Cache["xxx"].ToString(); 
                }

                string user_data = str;

                string[] arrays = xxx.Split('@');

                for (int i = 0; i < arrays.Length; i++)
                {
                    if (user_data.IndexOf(arrays[i]) >= 0)
                    {
                        u = true;
                        return u;

                    }
                  
                    
                }

                m_streamReader.Close();
               
                return u;
               
            }

            catch (Exception em)
            {
                return false;
            }
        
        
        }
    }
}
