using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Collections;

namespace FBS.Utils
{
    public class UploadFileInfo : System.Object
    {
        public string m_sOriginalFilePath = "";
        public string m_sFileName = "";
        public string m_sDescription = "";
        public DateTime m_oUploadDateTime;
    }

    public class Tools
    {
        private static readonly string m_sAVLocation = (ConfigurationManager.AppSettings["UploadUtilAVLocation"] == null) ? "vscanwin32.com" : ConfigurationManager.AppSettings["UploadUtilAVLocation"];
        private static readonly string[] m_pAllowedUploadTypes = (ConfigurationManager.AppSettings["UploadUtilAllowedUploadTypes"] == null) ? ".jpg;.jpeg;.jpe;.gif;.bmp;.png;.txt;.xml;.csv;.pdf;.doc;.docx;.xls;.xlsx;.tiff;.tif;.rtf;.htm;.html;.avi;.flv;.swf;.mpg;.wma;.wmv;.mpeg;.mp3;.mp4;.mpeg4;.ppt;.pptx;.zip;.rar".Split(";".ToCharArray()) : ConfigurationManager.AppSettings["UploadUtilAllowedUploadTypes"].ToLower().Split(";".ToCharArray());
        private static readonly string[] m_pAllowedUploadPictureTypes = (ConfigurationManager.AppSettings["UploadUtilAllowedUploadPictureTypes"] == null) ? ".jpg;.jpeg;.jpe;.gif;.bmp;.png".Split(";".ToCharArray()) : ConfigurationManager.AppSettings["UploadUtilAllowedUploadPictureTypes"].ToLower().Split(";".ToCharArray());
        private static readonly int m_nImageThumbSize = (ConfigurationManager.AppSettings["UploadUtilImageThumbSize"] == null) ? 100 : Convert.ToInt32(ConfigurationManager.AppSettings["UploadUtilImageThumbSize"]);

        private static bool MyCallBack()
        {
            return true;
        }

        public static void CreateThumbForFile(string sFileName, string sFolderPath, string sDefaultThumbImagePath)
        {
            string sFilePath = Path.Combine(sFolderPath, sFileName);
            string sThumbPath = Path.Combine(sFolderPath, "t_" + sFileName);
            if (IsFilePicture(sFileName))
            {
                if (File.Exists(sThumbPath) == false)
                {
                    System.Drawing.Image oSource = System.Drawing.Image.FromFile(sFilePath);
                    System.Drawing.Image oDest = oSource.GetThumbnailImage(m_nImageThumbSize, m_nImageThumbSize, new System.Drawing.Image.GetThumbnailImageAbort(MyCallBack), System.IntPtr.Zero);
                    oDest.Save(sThumbPath);
                }
            }
            else
            {
                string sExt = sFileName.Substring(sFileName.LastIndexOf("."));
                File.Copy(sDefaultThumbImagePath, sThumbPath.Replace(sExt, ".jpg"));
            }
        }

        public static bool CheckFileType(string sFilePath)
        {
            bool bFound = false;
            for (int i = 0; i < m_pAllowedUploadTypes.Length; i++)
            {
                if (sFilePath.EndsWith(m_pAllowedUploadTypes[i]))
                {
                    bFound = true;
                    break;
                }
            }
            return bFound;
        }

        public static bool IsFilePicture(string sFileName)
        {
            for (int i = 0; i < m_pAllowedUploadPictureTypes.Length; i++)
            {
                if (sFileName.EndsWith(m_pAllowedUploadPictureTypes[i])) return true;
            }
            return false;
        }

        public static bool CheckVirus(string sFilePath)
        {
            try
            {
                string sLogFilePath = sFilePath + ".log";
                String sArgs = "\"" + sFilePath + "\" /i /d /lappend /lr=\"" + sLogFilePath + "\"";
                Process proc = Process.Start(m_sAVLocation, sArgs);
                proc.WaitForExit();
                StreamReader oReader = new StreamReader(sLogFilePath);
                string sOutput = oReader.ReadToEnd().ToLower();
                oReader.Close();
                File.Delete(sLogFilePath);
                bool bRet = (sOutput.IndexOf("found 0 viruses") > 0) && (sOutput.IndexOf("maybe 0 viruses") > 0);
                if (bRet == false) throw new Exception("Found possible virus");
                return true;
            }
            catch (Exception)
            {
                File.Delete(sFilePath);
                return false;
            }
        }

        public static Hashtable LoadFileInfo(string sFolderPath)
        {
            lock (typeof(UploadFileInfo))
            {
                string sFilePath = Path.Combine(sFolderPath, "FileInfo.data");
                if (File.Exists(sFilePath))
                {
                    IFormatter Formatter = new BinaryFormatter();
                    Stream oStream = new FileStream(sFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    object oData = Formatter.Deserialize(oStream);
                    oStream.Close();
                    return (Hashtable)oData;
                }
                else return new Hashtable(512);
            }
        }

        public static void StoreFileInfo(string sFolderPath, Hashtable oFileInfo)
        {
            lock (typeof(UploadFileInfo))
            {
                string sFilePath = Path.Combine(sFolderPath, "FileInfo.data");
                IFormatter Formatter = new BinaryFormatter();
                Stream oStream = new FileStream(sFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
                Formatter.Serialize(oStream, oFileInfo);
                oStream.Close();
            }
        }

        public static void RemoveFile(string sFolderPath, string sFileName)
        {
            lock (typeof(UploadFileInfo))
            {
                Hashtable oFileInfo = LoadFileInfo(sFolderPath);
                if (oFileInfo[sFileName] != null)
                {
                    oFileInfo.Remove(sFileName);
                    StoreFileInfo(sFolderPath, oFileInfo);
                }
            }
            try
            {
                string sFilePath = Path.Combine(sFolderPath, sFileName);
                if (File.Exists(sFilePath)) File.Delete(sFilePath);
                string sThumbPath = Path.Combine(sFolderPath, "t_" + sFileName);
                if (File.Exists(sThumbPath)) File.Delete(sThumbPath);
                else
                {
                    string sExt = sFileName.Substring(sFileName.LastIndexOf("."));
                    sThumbPath = sThumbPath.Replace(sExt, ".jpg");
                    if (File.Exists(sThumbPath)) File.Delete(sThumbPath);
                }
            }
            catch (Exception) { }
        }
        //获取一张图片
        public static UploadFileInfo GetUploadFileInfo(string sFolderPath, string sFileName)
        {
            Hashtable oFileInfo = LoadFileInfo(sFolderPath);
            return GetUploadFileInfo(oFileInfo, sFileName);
        }
        //获取一张图片
        public static UploadFileInfo GetUploadFileInfo(Hashtable oFileInfo, string sFileName)
        {
            object[] pItem = (object[])oFileInfo[sFileName];
            if (pItem != null)
            {
                UploadFileInfo oInfo = new UploadFileInfo();
                oInfo.m_sOriginalFilePath = (string)pItem[0];
                oInfo.m_sFileName = (string)pItem[1];
                oInfo.m_sDescription = (string)pItem[2];
                oInfo.m_oUploadDateTime = Convert.ToDateTime(pItem[3]);
                return oInfo;
            }
            else throw new Exception("File info not found");
        }
        //读取照片信息
        public static UploadFileInfo[] GetUploadFileInfo(string sFolderPath)
        {
            Hashtable oInfo = LoadFileInfo(sFolderPath);
            SortedList oList = new SortedList(oInfo.Count);
            IDictionaryEnumerator oEnum = oInfo.GetEnumerator();
            int nCount = 0;
            while (oEnum.MoveNext())
            {
                string sKey = Convert.ToDateTime(((object[])oEnum.Value)[3]).ToString("yyyyMMddHHmmss") + nCount.ToString("00000");
                oList.Add(sKey, oEnum.Value);
                nCount++;
            }
            UploadFileInfo[] pRet = new UploadFileInfo[nCount];

            for (int i = 0; i < nCount; i++)
            {
                object[] pItem = (object[])oList.GetByIndex(i);
                pRet[i] = new UploadFileInfo();
                pRet[i].m_sOriginalFilePath = (string)pItem[0];
                pRet[i].m_sFileName = (string)pItem[1];
                pRet[i].m_sDescription = (string)pItem[2];
                pRet[i].m_oUploadDateTime = Convert.ToDateTime(pItem[3]);
            }
            return pRet;
        }

        public static void ProcessFolder(string sFolderPath, string sDefaultThumbImagePath)
        {
            if (sFolderPath == null || Directory.Exists(sFolderPath) == false) throw new Exception("Folder does not exist");
            string sFileInfoPath = sFolderPath + "\\FileInfo.data";
            if (File.Exists(sFileInfoPath)) return;
            string[] pFileList = Directory.GetFiles(sFolderPath);
            if (pFileList != null && pFileList.Length > 0)
            {
                DateTime oNow = DateTime.Now;
                Hashtable oFileInfo= new Hashtable(pFileList.Length);
                for (int i = 0; i < pFileList.Length; i++)
                {
                    string sFileName = Path.GetFileName(pFileList[i]).ToLower();
                    if (CheckFileType(sFileName))
                    {
                        if (sFileName.StartsWith("t_") && IsFilePicture(sFileName)) continue;
                        CreateThumbForFile(sFileName, sFolderPath, sDefaultThumbImagePath);
                        object[] pItem = new object[4] { "", sFileName, "", oNow };
                        oFileInfo[sFileName] = pItem;
                    }
                }
                StoreFileInfo(sFolderPath, oFileInfo);
            }
        }
    }
}
