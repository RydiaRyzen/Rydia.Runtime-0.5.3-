using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.GetQuery, GL.Ext.GetQuery
    public enum GetQueryParam
    {
        //
        // 概要:
        //     Original was GL_QUERY_COUNTER_BITS_EXT = 0x8864
        QueryCounterBitsExt = 34916,
        //
        // 概要:
        //     Original was GL_CURRENT_QUERY = 0x8865
        CurrentQuery = 34917,
        //
        // 概要:
        //     Original was GL_CURRENT_QUERY_EXT = 0x8865
        CurrentQueryExt = 34917
    }
}
