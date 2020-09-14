using System;

namespace MySchool
{
    /// <summary>
    /// Send response  EventArgs
    /// </summary>
    public class ResponseCodeEventArgs : EventArgs
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }

    public enum CodeException
    {
        Exception = 21,
        Alert = 22
    }

    public enum CodeDataLayer
    {
        Save = 1,
        Fetch = 2,
        Update = 3,
        Delete = 4,
        Clear = 5
    }

    public enum DbResponse
    {
         success = 1,
         failed = 0
    }
}
