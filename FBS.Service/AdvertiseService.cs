using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FBS.Domain.Aggregate.Entity;
using System.Web;
using System.Web.Caching;
using System.IO;
using System.Collections;
using FBS.Service.ActionModels;


namespace FBS.Service
{
    public  class AdvertiseService
    {
      
            //to be used locally in the class, improvement: can be stored in web.config
            private  string aps_path = HttpContext.Current.Server.MapPath("~") + "/App_Data /Advertiser.xml";


            /// <summary>
            /// Returns an instance of the Xml Document
            /// that contains the Ads, also uses Cache
            /// to store the document to improve performance
            /// </summary>\
            /// <returns>XmlDocument</returns>
            public  XmlDocument GetXmlDocument()
            {
                XmlDocument doc;
                if (HttpContext.Current.Cache["AdvertiserDocument"] != null)
                {
                    doc = (XmlDocument)HttpContext.Current.Cache["AdvertiserDocument"];
                }
                else
                {
                    doc = new XmlDocument();
                    doc.Load(aps_path);
                    CacheDependency cp = new CacheDependency(aps_path);
                    HttpContext.Current.Cache.Insert("AdvertiserDocument", doc, cp);
                }

                return doc;
            }

            public AdvertiseService()
            {
                //
                // TODO: Add constructor logic here
                //
            }

            /// <summary>
            /// Adds an Advertise to a specefic location
            /// The location is provided within the object
            /// </summary>
            /// <param name="adv"></param>
            public void AddOne(AdvertiseDspModel adv)
            {
                XmlDocument doc = GetXmlDocument();
                //adv.Location = adv.Location.Replace(".", "/");
                //[@Id='
                //Advertiser/Section[@Id='']/Page[@Id='']/Area[@Id='']/Position[@Id='']
                //"Advertiser.User.Login.area1.position1"
                //Advertiser.Section.Page.Area.Position
                string[] nodel = adv.Location.Split('.');
                string xpath = string.Empty;


                xpath += nodel[0] + "/";


                xpath += "Section" + "[@Id='" + nodel[1] + "']/";



                xpath += "Page" + "[@Id='" + nodel[2] + "']/";


                xpath += "Area" + "[@Id='" + nodel[3] + "']/";


                xpath += "Position" + "[@Id='" + nodel[4] + "']";


                XmlNode list = doc.SelectSingleNode(xpath);
                if (list == null)
                {
                    throw new Exception("此广告位在配置中不存在，请与系统管理员联系。");
                }
                AddAdvertNode(doc, list, adv, aps_path);
            }

            /// <summary>
            /// Writes AdvertiseObject as a node in the Xml document
            /// </summary>
            /// <param name="xDoc">The Advertiser Xml Document</param>
            /// <param name="node">The xml parent node to write to</param>
            /// <param name="adv">The object</param>
            /// <param name="docPath">I'm lazy removing this after I globally defined it, but instead wasting time writing this useless description</param>
            public void AddAdvertNode(XmlDocument xDoc, XmlNode node, AdvertiseDspModel adv, string docPath)
            {
                #region [Create Element list]
                XmlNode root = xDoc.CreateNode(XmlNodeType.Element, "Advertisement", string.Empty);

                XmlAttribute attribute = xDoc.CreateAttribute("Id");
                attribute.Value = adv.ID.ToString();
                root.Attributes.Append(attribute);

                XmlNode type = root.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "type", string.Empty));
                type.InnerText = adv.ContentType.ToString();

                XmlNode path = root.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "path", string.Empty));
                path.InnerText = adv.Path;

                XmlNode width = root.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "width", string.Empty));
                width.InnerText = adv.Width.ToString();

                XmlNode height = root.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "height", string.Empty));
                height.InnerText = adv.Height.ToString();

                XmlNode priority = root.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "priority", string.Empty));
                priority.InnerText = adv.Priority.ToString();

                XmlNode begin = root.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "BeginTime", string.Empty));
                begin.InnerText = adv.BeginTime.ToString("yyyy-MM-dd");

                XmlNode end = root.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "EndTime", string.Empty));
                end.InnerText = adv.EndTime.ToString("yyyy-MM-dd");

                XmlNode advertiseName = root.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "advertiseName", string.Empty));
                advertiseName.InnerText = adv.AdvertiseName;


                XmlNode location = root.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "Location", string.Empty));
                location.InnerText = adv.Location;
                //Flash movie doesn't require a URL
                if (adv.ContentType != AdvertiseType.FlashMovie)
                {
                    if (!string.IsNullOrEmpty(adv.Url))
                    {
                        XmlNode url = root.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "url", string.Empty));
                        url.InnerText = adv.Url;
                    }
                }
                #endregion

                // append the node and save The Document
                node.AppendChild(root);
                xDoc.Save(docPath);
            }
        /// <summary>
        /// 根据位置编号找广告
        /// </summary>
        /// <param name="locationid"></param>
        /// <returns></returns>
            public AdvertiseDspModel[] GetAdvertiseDspModelByPositionId(string positionid)
            {
                XmlDocument doc = GetXmlDocument();
                //string[] nodel = location.Split('.');
                string xpath = string.Empty;


                xpath += "Advertiser/";


                xpath += "Section/";



                xpath += "Page/";


                xpath += "Area/";


                xpath += "Position" + "[@Id='" + positionid + "']";


                XmlNodeList advertises = doc.SelectNodes(xpath + "/Advertisement");
                if (advertises.Count == 0)
                {
                    return new AdvertiseDspModel[0];
                }
                ArrayList arr = new ArrayList();
                int counter = 0;
                foreach (XmlNode item in advertises)
                {
                    DateTime b= DateTime.Parse(item["BeginTime"].InnerText);
                    DateTime e = DateTime.Parse(item["EndTime"].InnerText);
                    if (e >= DateTime.Now && b <= DateTime.Now)
                    {
                        AdvertiseDspModel adv = new AdvertiseDspModel();

                        adv.ContentType = (AdvertiseType)Enum.Parse(typeof(AdvertiseType), item["type"].InnerText);
                        adv.Height = int.Parse(item["height"].InnerText);
                        adv.Priority = int.Parse(item["priority"].InnerText);
                        Guid id = new Guid(item.Attributes["Id"].Value);
                        adv.ID = id;
                        adv.Location = item["Location"].InnerText;
                        adv.Path = item["path"].InnerText;
                        if (item["url"] != null)
                            adv.Url = item["url"].InnerText;
                        adv.Width = int.Parse(item["width"].InnerText);
                        adv.AdvertiseName = item["advertiseName"].InnerText;
                        adv.BeginTime = DateTime.Parse(item["BeginTime"].InnerText);
                        adv.EndTime = DateTime.Parse(item["EndTime"].InnerText);
                        arr.Add(adv);
                        counter++;
                    }
                }


                AdvertiseDspModel[] res = new AdvertiseDspModel[counter];
                for (int i = 0; i < counter; i++)
                {
                    res[i] = (AdvertiseDspModel)arr[i];
                }

                return res;
            }

            /// <summary>
            /// Returns array of ads that can be used as Data source object
            /// </summary>
            /// <param name="location"></param>
            /// <returns></returns>
            public AdvertiseDspModel[] GetObjectsByLocation(string location)
            {
                XmlDocument doc = GetXmlDocument();

                string[] nodel = location.Split('.');
                string xpath = string.Empty;


                xpath += nodel[0] + "/";


                xpath += "Section" + "[@Id='" + nodel[1] + "']/";



                xpath += "Page" + "[@Id='" + nodel[2] + "']/";


                xpath += "Area" + "[@Id='" + nodel[3] + "']/";


                xpath += "Position" + "[@Id='" + nodel[4] + "']";


                XmlNodeList advertises = doc.SelectNodes(xpath + "/Advertisement");
                if (advertises.Count == 0)
                {
                    return new AdvertiseDspModel[0];
                }
                ArrayList arr = new ArrayList();
                int counter = 0;
                foreach (XmlNode item in advertises)
                {
                    AdvertiseDspModel adv = new AdvertiseDspModel();

                    adv.ContentType = (AdvertiseType)Enum.Parse(typeof(AdvertiseType), item["type"].InnerText);
                    adv.Height = int.Parse(item["height"].InnerText);
                    adv.Priority = int.Parse(item["priority"].InnerText);
                    Guid id = new Guid(item.Attributes["Id"].Value);
                    adv.ID = id;
                    adv.Location = location;
                    adv.Path = item["path"].InnerText;
                    if (item["url"] != null)
                        adv.Url = item["url"].InnerText;
                    adv.Width = int.Parse(item["width"].InnerText);
                    adv.AdvertiseName = item["advertiseName"].InnerText;
                    adv.BeginTime =DateTime.Parse( item["BeginTime"].InnerText);
                    adv.EndTime = DateTime.Parse(item["EndTime"].InnerText);
                    arr.Add(adv);
                    counter++;
                }


                AdvertiseDspModel[] res = new AdvertiseDspModel[counter];
                for (int i = 0; i < counter; i++)
                {
                    res[i] = (AdvertiseDspModel)arr[i];
                }

                return res;

            }

            /// <summary>
            /// Deletes an Advertise
            /// </summary>
            /// <param name="id"></param>
            public void DeleteAdvertiseDspModel(AdvertiseDspModel mo)
            {
                XmlDocument doc = GetXmlDocument();


                string[] nodel = mo.Location.Split('.');
                string xpath = string.Empty;


                xpath += nodel[0] + "/";


                xpath += "Section" + "[@Id='" + nodel[1] + "']/";



                xpath += "Page" + "[@Id='" + nodel[2] + "']/";


                xpath += "Area" + "[@Id='" + nodel[3] + "']/";


                xpath += "Position" + "[@Id='" + nodel[4] + "']";


                XmlNodeList advertises = doc.SelectNodes(xpath + "/Advertisement");


                XmlNode parent = doc.SelectNodes(xpath)[0];


                foreach (XmlNode item in advertises)
                {

                    if (item.Attributes["Id"].Value == mo.ID.ToString())
                    {
                        //Remove the file
                        string file_aps_path = HttpContext.Current.Server.MapPath("~") + item["path"].InnerText;
                        if (File.Exists(file_aps_path))
                            File.Delete(file_aps_path);

                        //remove the child node
                        parent.RemoveChild(item);

                        //save the document
                        doc.Save(aps_path);

                        //time to escape :)  
                        break;
                    }


                }
            }





          

            /// <summary>
            /// Update AdvertiseObject Properties
            /// </summary>
            /// <param name="adv"></param>
            public void UpdateAdvertiseDspModel(AdvertiseDspModel adv)
            {
                XmlDocument doc = GetXmlDocument();
                string[] nodel = adv.Location.Split('.');
                string xpath = string.Empty;


                xpath += nodel[0] + "/";


                xpath += "Section" + "[@Id='" + nodel[1] + "']/";



                xpath += "Page" + "[@Id='" + nodel[2] + "']/";


                xpath += "Area" + "[@Id='" + nodel[3] + "']/";


                xpath += "Position" + "[@Id='" + nodel[4] + "']";


                XmlNode item = doc.SelectSingleNode(xpath + "/Advertisement[@Id='" + adv.ID + "']");



                // Updating Values
                item["width"].InnerText = adv.Width.ToString();
                item["height"].InnerText = adv.Height.ToString();
                if (adv.ContentType != AdvertiseType.FlashMovie)
                {
                    if (!string.IsNullOrEmpty(adv.Url))
                        item["url"].InnerText = adv.Url;
                }
                item["path"].InnerText = adv.Path;
                item["BeginTime"].InnerText = adv.BeginTime.ToString("yyyy-MM-dd");
                item["EndTime"].InnerText = adv.EndTime.ToString("yyyy-MM-dd");
                item["priority"].InnerText = adv.Priority.ToString();
                //save the document
                doc.Save(aps_path);


            }

            /// <summary>
            /// Get AdvertiseObject by Id
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public AdvertiseDspModel GetAdvertiseDspModelById(Guid id)
            {
                XmlDocument doc = GetXmlDocument();
                XmlNodeList advertises = doc.GetElementsByTagName("Advertisement");
                AdvertiseDspModel adv = null;
                foreach (XmlNode item in advertises)
                {
                    if (item.Attributes["Id"].Value == id.ToString())
                    {
                        adv = new AdvertiseDspModel();
                        adv.ContentType = (AdvertiseType)Enum.Parse(typeof(AdvertiseType), item["type"].InnerText);
                        adv.Height = int.Parse(item["height"].InnerText);
                        adv.ID = id;
                        adv.Location = item["Location"].InnerText;
                        adv.Path = item["path"].InnerText;
                        if (item["url"] != null)
                            adv.Url = item["url"].InnerText;
                        adv.Width = int.Parse(item["width"].InnerText);
                        adv.Priority = int.Parse(item["priority"].InnerText);
                        adv.AdvertiseName = item["advertiseName"].InnerText;
                        adv.BeginTime = DateTime.Parse( item["BeginTime"].InnerText);
                        adv.EndTime =DateTime.Parse( item["EndTime"].InnerText);
                        break;
                    }

                }
                   
                return adv;
            }

           /// <summary>
           /// 随机获取一个广告根据位置
           /// </summary>
           /// <param name="location"></param>
           /// <returns></returns>
            public AdvertiseDspModel GetOneAdvertiseDspModel(string location)
            {
                AdvertiseDspModel[] k = GetAdvertiseDspModelByPositionId(location);
                if (k.Length == 0)
                {
                    return null;
                }
                Random rand = new Random(System.Guid.NewGuid().GetHashCode());
                
                return  k[rand.Next(0, k.Length)];
            }
    }
}
