using System;
using System.Diagnostics;
using System.Threading;

namespace GK
{
    public static class WaitHelper
    {
        public static WaitResult WaitFor(Func<bool> condition) => WaitFor(condition, TimeSpan.Zero);
        public static WaitResult WaitFor<T>(this T item, Func<T, bool> condition) => WaitFor(item, condition, TimeSpan.Zero);

        public static WaitResult WaitFor(Func<bool> condition, TimeSpan timeout)
        {
            if (timeout.Ticks > 0)
            {
                bool inTime = false;
                Stopwatch sw = Stopwatch.StartNew();
                try { while (!condition() && (inTime = sw.Elapsed < timeout)) Thread.Sleep(1); } catch (Exception error) { return new WaitResult { Error = error, IsError = true, IsFinished = false, IsTimeout = false }; }
                sw.Stop();
                return inTime ? WaitResult.Finished : WaitResult.Timeout;
            }
            else
            {
                try { while (!condition()) Thread.Sleep(1); }
                catch (Exception error) { return new WaitResult { Error = error, IsError = true, IsFinished = false, IsTimeout = false }; }
                return WaitResult.Finished;
            }
        }
        public static WaitResult WaitFor<T>(this T item, Func<T, bool> condition, TimeSpan timeout)
        {
            if (timeout.Ticks > 0)
            {
                bool inTime = false;
                Stopwatch sw = Stopwatch.StartNew();
                try { while (!condition(item) && (inTime = sw.Elapsed < timeout)) Thread.Sleep(1); } catch (Exception error) { return new WaitResult { Error = error, IsError = true, IsFinished = false, IsTimeout = false }; }
                sw.Stop();
                return inTime ? WaitResult.Finished : WaitResult.Timeout;
            }
            else
            {
                try { while (!condition(item)) Thread.Sleep(1); }
                catch (Exception error) { return new WaitResult { Error = error, IsError = true, IsFinished = false, IsTimeout = false }; }
                return WaitResult.Finished;
            }
        }
    }
}