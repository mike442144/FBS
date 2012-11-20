using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Domain.Security
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple=false,Inherited=true)]
    public sealed class Task:Attribute
    {
        private string _name, _description;

        public string Name { get { return this._name; } set { this._name = value; } }
        public string Description { get { return this._description; }set{this._description=value;} }

        public Task(string name,string description)
        {
            this._name = name;
            this._description = description;
        }
    }
}
