using System;
using System.Collections.Generic;
using System.Text;

namespace MySchool
{

    public enum EnumResult
    {
        success,
        fail,
        exception
    }
    public enum EnumOperation
    {
        Get,
        Insert,
        Update,
        Delete,
        
    }

    public class OperationEventArgs : EventArgs
    {
        public string Message { get; set; }
        public int Opeartaion { get; set; }
        public int Result { get; set; }
    }
    public class ValidationEventArgs : EventArgs
    {
        public string Message { get; set; }
        public int Result { get; set; }

        public int Opeartaion { get; set; }
    }
}
