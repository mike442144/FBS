using System;

namespace FBS.Utils
{
    public class SiteNullException : ApplicationException
    {
        public SiteNullException(string message)
            : base(message)
        {
        }
    }

    public class NodeNullException : ApplicationException
    {
        public NodeNullException(string message)
            : base(message)
        {
        }
    }

    public class SectionNullException : ApplicationException
    {
        public SectionNullException(string message)
            : base(message)
        {
        }
    }

    public class AccessForbiddenException : ApplicationException
    {
        public AccessForbiddenException(string message)
            : base(message)
        {
        }

        public AccessForbiddenException(string message, Exception e)
            : base(message, e)
        { }
    }

    public class ActionForbiddenException : ApplicationException
    {
        public ActionForbiddenException(string message)
            : base(message)
        {
        }
    }

    public class DeleteForbiddenException : ApplicationException
    {
        public DeleteForbiddenException(string message)
            : base(message)
        {
        }
    }

    public class EmailException : ApplicationException
    {
        public EmailException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    
    /// <summary>
    /// 登录异常
    /// </summary>
    public class LogonException : ApplicationException
    {
        public LogonException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// 注册异常
    /// </summary>
    public class RegisterException : ApplicationException
    {
        public RegisterException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// 添加主题异常
    /// </summary>
    public class AddForumThreadException : ApplicationException
    {
        public AddForumThreadException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// 更新主题异常
    /// </summary>
    public class UpdateForumThreadException : ApplicationException
    {
        public UpdateForumThreadException(string message)
            : base(message)
        {
        }
    }

    public class ReplyForumThreadException : ApplicationException
    {
        public ReplyForumThreadException(string message)
            : base(message)
        {
        }
        public ReplyForumThreadException(string message,Exception e):base(message,e)
        {
        }
    }
    /// <summary>
    /// 帐户不存在异常
    /// </summary>
    public class NoAccountException : ApplicationException
    {
        public NoAccountException(string msg)
            : base(msg)
        { }
    }
}
