using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Contexts;
using System.Diagnostics;

namespace FBS.Domain.Log
{
    internal class LoggingAspect:IMessageSink
    {
        internal LoggingAspect(IMessageSink next)
        {
            m_next = next;
        }

        #region Private Vars
        private IMessageSink m_next;
        private String m_typeAndName;
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
            PostProcess(msg, returnMethod);
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
            Type type = Type.GetType(call.TypeName);
            m_typeAndName = type.Name + "." + call.MethodName;
            //Console.Write("PreProcessing: " + m_typeAndName + "(");

            //写入日志文件
            Utils.LoggerHelper.Info("PreProcessing: " + m_typeAndName + "(");


            // Loop through the [in] parameters
            for (int i = 0; i < call.ArgCount; ++i)
            {
                if (i > 0) Console.Write(", ");
                //Console.Write(call.GetArgName(i) + " = " + call.GetArg(i));

                //写入日志文件
                Utils.LoggerHelper.Info(call.GetArgName(i) + " = " + call.GetArg(i));
            }
            //Console.WriteLine(")");

            Utils.LoggerHelper.Info(")");
        }

        private void PostProcess(IMessage msg, IMessage msgReturn)
        {
            // We only want to process method return calls
            if (!(msg is IMethodMessage) ||
                !(msgReturn is IMethodReturnMessage)) return;

            IMethodReturnMessage retMsg = (IMethodReturnMessage)msgReturn;
            
            //Console.Write("PostProcessing: ");
            
            //写入日志文件
            //Utils.LoggerHelper.Info("PostProcessing: ");

            Exception e = retMsg.Exception;
            if (e != null)
            {
                //Console.WriteLine("Exception was thrown: " + e);

                //写入日志文件
                Utils.LoggerHelper.Error("Exception was thrown: ",e);

                return;
            }

            // Loop through all the [out] parameters
            //Console.Write(m_typeAndName + "(");


            //写入日志文件
            //Utils.LoggerHelper.Info(m_typeAndName + "(");
            //if (retMsg.OutArgCount > 0)
            //{

            //    //Console.Write("out parameters[");

            //    //写入日志文件
            //    Utils.LoggerHelper.Info("out parameters[");

            //    for (int i = 0; i < retMsg.OutArgCount; ++i)
            //    {
            //        if (i > 0)
            //            //Console.Write(", ");
            //            //写入日志文件
            //            Utils.LoggerHelper.Info(",");
            //        //Console.Write(retMsg.GetOutArgName(i) + " = " +retMsg.GetOutArg(i));
                    
            //        //写入日志文件
            //        Utils.LoggerHelper.Info(retMsg.GetOutArgName(i) + " = " + retMsg.GetOutArg(i));

            //    }
            //    //Console.Write("]");

            //    Utils.LoggerHelper.Info("]");
            //}
            //if (retMsg.ReturnValue.GetType() != typeof(void))
                //Console.Write(" returned [" + retMsg.ReturnValue + "]");

                //Utils.LoggerHelper.Info(" returned [" + retMsg.ReturnValue + "]");

            //Console.WriteLine(")\n");

            //Utils.LoggerHelper.Info(")");
        }
        #endregion Helpers

    }

    public class LoggingProperty : IContextProperty, IContributeObjectSink
    {
        #region IContributeObjectSink implementation
        public IMessageSink GetObjectSink(MarshalByRefObject o, IMessageSink next)
        {
            return new LoggingAspect(next);
        }
        #endregion // IContributeObjectSink implementation

        #region IContextProperty implementation
        public string Name
        {
            get
            {
                return "CallLoggingProperty";
            }
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

    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Class)]
    public class LoggingAttribute : ContextAttribute
    {
        public LoggingAttribute() : base("CallLogging") { }
        public override void GetPropertiesForNewContext(System.Runtime.Remoting.Activation.IConstructionCallMessage ctorMsg)
        {
            ctorMsg.ContextProperties.Add(new LoggingProperty());
        }
    }
}
