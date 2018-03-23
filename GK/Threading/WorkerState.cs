namespace GK.Threading
{
    public enum WorkerState : byte
    {
        None = 0x00,
        Creating = 0x01,
        Ready = 0x02,
        Starting = 0x03,
        Running = 0x04,
        Suspending = 0x05,
        Suspended = 0x06,
        Resuming = 0x07,
        Stopping = 0x08,
        Stopped = 0x09,
    }
}