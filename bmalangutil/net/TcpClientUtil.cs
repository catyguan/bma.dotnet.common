using bmalangutil.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace bmalangutil.net
{
    public class TcpClientUtil
    {
        public static void connect(TcpClient client, String ip,int port, int timeoutMSec)
        {
            TimeOut<Exception> to = new TimeOut<Exception>();
            AsyncCallback cb = to.createTimeout(new TimeOut<Exception>.asyncCallback(timeoutCallback));
            client.BeginConnect(ip, port, cb, client);
            Exception err = to.waitFor(timeoutMSec);
            if (err != null)
            {
                throw err;
            }
        }

        private static Exception timeoutCallback(IAsyncResult ar)
        {
            try
            {
                TcpClient tcpclient = ar.AsyncState as TcpClient;
                if (tcpclient.Client != null)
                {
                    tcpclient.EndConnect(ar);
                    return null;
                }
                return new Exception("connect fail");
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
