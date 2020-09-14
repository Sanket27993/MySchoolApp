using System;
using System.Collections.Generic;
using System.Text;

namespace MySchool
{
    public class EventInvoke
    {
        private static EventInvoke instance;

        public static EventInvoke Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventInvoke();
                }
                return instance;
            }
        }

        public void SetViewModelEventArg(EventHandler<OperationEventArgs> OpeartaionEvent, string message,int opeartaion, int result)
        {
            var opeartaionEventArg = new OperationEventArgs
            {
                Message = message,
                Opeartaion=opeartaion,
                Result = result
            };
            OpeartaionEvent.Invoke(this, opeartaionEventArg);
        }
    }
}
