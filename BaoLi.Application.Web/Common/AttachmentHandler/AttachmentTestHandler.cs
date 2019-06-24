using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaoLi.Application.Web
{
    public class AttachmentTestHandler : AttachmentHandlerBase
    {
        public override void BreakPointTransmission(HttpRequestBase RequstBase)
        {
            throw new NotImplementedException();
        }

        public override Movit.Application.Entity.BaseManage.T_AttachmentEntity SaveAttach(HttpRequestBase RequstBase, HttpServerUtilityBase ServerBase)
        {
            throw new NotImplementedException();
        }

        public override void DeleteAttach(HttpRequestBase RequstBase, string AttchId)
        {
            throw new NotImplementedException();
        }

        public override void DownFile(string filePath, string fileName, HttpResponseBase ResponseBase, HttpServerUtilityBase ServerBase)
        {
            throw new NotImplementedException();
        }
    }
}