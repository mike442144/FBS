using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service
{
    class UserAuthorityService
    {
        //判断用户对某个板块的访问权限
        public bool CheckVisitForumsAuthority()
        {
            bool hasAuth = true;
            return hasAuth;
        }

        //判断用户创建主题的权限
        public bool CheckCreateThreadAuthority()
        {
            bool hasAuth = true;
            return hasAuth;
        }

        //判断用户添加附件的权限
        public bool ChechPostAttachmentAuthority()
        {
            bool hasAuth = true;
            return hasAuth;
        }

        //判断用户下载附件的权限
        public bool CheckDownloadAttachmentAuthority()
        {
            bool hasAuth = true;
            return hasAuth;
        }

    }
}
