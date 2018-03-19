using System;

namespace GK
{
    public class WaitResult
    {
        #region Static stuff
        public static WaitResult Timeout => new WaitResult { Error = null, IsError = false, IsFinished = false, IsTimeout = true };
        public static WaitResult Finished => new WaitResult { Error = null, IsError = false, IsFinished = true, IsTimeout = false };
        #endregion

        public bool IsError { get; set; }
        public bool IsTimeout { get; set; }
        public bool IsFinished { get; set; }
        public Exception Error { get; set; }
    }
}