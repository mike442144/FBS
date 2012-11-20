using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Runtime.Remoting.Activation;
using System.Reflection;

namespace FBS.Domain.Security
{
    internal class SecurityAspect : IMessageSink
    {
        internal SecurityAspect(IMessageSink next)
        {
            m_next = next;
        }

        #region Private Vars
        private IMessageSink m_next;
        #endregion // Private Vars

        #region IMessageSink implementation
        public IMessageSink NextSink
        {
            get { return m_next; }
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            Preprocess(msg);
            IMessage returnMethod = m_next.SyncProcessMessage(msg);
            return returnMethod;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new InvalidOperationException();
        }
        #endregion //IMessageSink implementation

        #region Helper methods
        private void Preprocess(IMessage msg)
        {
            // We only want to process method calls
            if (!(msg is IMethodMessage)) return;

            IMethodMessage call = msg as IMethodMessage;

            MethodBase mb = call.MethodBase;
            object[] attrObj = mb.GetCustomAttributes(typeof(Task), false);

            if (attrObj != null)
            {
                Task attr = (Task)attrObj[0];

                if (!string.IsNullOrEmpty(attr.Name))
                { //check permission here
                    //HttpContext.Current.Response.Write(attr.Name);
                    AuthorityManager.PermissionCheck(attr.Name);
                }
            }
        }

        #endregion Helpers
    }

    public class SecurityProperty : IContextProperty, IContributeObjectSink
    {
        #region IContributeObjectSink implementation
        public IMessageSink GetObjectSink(MarshalByRefObject o, IMessageSink next)
        {
            return new SecurityAspect(next);
        }
        #endregion // IContributeObjectSink implementation

        #region IContextProperty implementation
        public string Name
        {
            get { return "SecurityProperty"; }
        }
        public void Freeze(Context newContext)
        {
        }
        public bool IsNewContextOK(Context newCtx)
        {
            return true;
        }
        #endregion //IContextProperty implementation
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class SecurityAttribute : ContextAttribute
    {
        public SecurityAttribute() : base("Security") { }
        public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
        {
            ccm.ContextProperties.Add(new SecurityProperty());
        }
    }
}
