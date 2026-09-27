using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.GetQueryObject, GL.Ext.GetQueryObject
    public enum GetQueryObjectParam
    {
        //
        // 概要:
        //     Original was GL_QUERY_RESULT = 0x8866
        QueryResult = 34918,
        //
        // 概要:
        //     Original was GL_QUERY_RESULT_EXT = 0x8866
        QueryResultExt = 34918,
        //
        // 概要:
        //     Original was GL_QUERY_RESULT_AVAILABLE = 0x8867
        QueryResultAvailable = 34919,
        //
        // 概要:
        //     Original was GL_QUERY_RESULT_AVAILABLE_EXT = 0x8867
        QueryResultAvailableExt = 34919
    }
}
