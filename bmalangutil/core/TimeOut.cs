using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bmalangutil.core
{
    public class TimeOut<RESULT>
    {
        public delegate RESULT asyncCallback(IAsyncResult ar);

        private RESULT result;
        private asyncCallback callback;

        private int startTick;
        private bool isCallback = false;
        private ManualResetEvent waitObject = new ManualResetEvent(false);

        public AsyncCallback createTimeout(asyncCallback cb)
        {
            result = default(RESULT);
            startTick = Environment.TickCount;
            callback = cb;
            isCallback = false;
            waitObject.Reset();
            return new AsyncCallback(callBackMethod);
        }

        public RESULT waitFor(int time)
        {
            waitObject.WaitOne(time);
            if (!isCallback)
            {
                throw new TimeoutException();
            }
            return result;
        }

        public bool checkFor(out RESULT r, int time)
        {            
            if (isCallback)
            {
                r = result;
                return true;
            }
            if (Environment.TickCount - startTick < time)
            {
                r = default(RESULT);
                return false;
            }
            throw new TimeoutException();
        }

        private void callBackMethod(IAsyncResult asyncresult)
        {
            isCallback = true;            
            if (callback != null)
            {
                result = callback(asyncresult);
            }
            waitObject.Set();
        }

    }
}
