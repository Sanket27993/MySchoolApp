using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchool
{
    /// <summary>
    /// EventHandler for sendresponse  
    /// </summary>
    public interface IEventSendResponse
    {
        event EventHandler<ResponseCodeEventArgs> EventSendResponse;
    }
}
