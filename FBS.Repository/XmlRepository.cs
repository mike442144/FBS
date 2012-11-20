using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Specifications;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.IO;

namespace FBS.Repository
{
    public class XmlRepository:IXmlRepository
    {
        private XmlData _xmlData=null;
        private XElement _root;
        public XmlRepository()
        {
            this._xmlData = new XmlData();
            this._root = this._xmlData.Doc.Root;
        }

        #region IRepository<Province> 成员

        public void Add(Province entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(ISpecification<Province> specification)
        {
            throw new NotImplementedException();
        }

        public int Count(ISpecification<Province> specification)
        {
            throw new NotImplementedException();
        }

        public Province Find(ISpecification<Province> specification)
        {
            throw new NotImplementedException();
        }

        public Province GetByKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<Province> FindAll()
        {
            IList<Province> targets = new List<Province>();

            var list= this._root.Elements("Province");
            foreach (XElement node in list)
            {
                Province instance = new Province(Convert.ToInt32(node.Attribute("ID").Value), node.Attribute("Name").Value);
                targets.Add(instance);
            }

            return targets;
        }

        public IList<Province> FindAll(ISpecification<Province> specification)
        {
            throw new NotImplementedException();
        }

        public void Remove(Province entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveByKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Province entity)
        {
            throw new NotImplementedException();
        }

        public void PersistAll()
        {
            throw new NotImplementedException();
        }

        public IList<City> Select(string attr, string value)
        {
            IList<City> targets = new List<City>();

            XElement ele=this._root.Descendants("Province").Single(t=>t.Attribute(attr).Value==value);
            
            foreach (XElement x in ele.Descendants("City"))
            {
                City tmp = new City(Convert.ToInt32(x.Attribute("ID").Value), x.Value, x.Attribute("PostCode").Value);
                targets.Add(tmp);
            }

            return targets;
        }

        #endregion
    }
}
