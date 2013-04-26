using bmalangutil.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bmalangutil.forms
{
    internal class AppThreadTaskMessager : IMessageFilter
    {
        //截取消息，进行处理
        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case AppThread.THREAD_TASK_MESSAGE: 　　　　　//拦截自定义消息　
                    AppThread.instance.run();
                    return true;
                default:
                    return false; 　　　       //返回false则消息未被裁取，系统会处理
            }
        }
    }

    public interface AppThreadTask
    {
        void run();
    }

    public class AppThread
    {
        internal const int THREAD_TASK_MESSAGE = 0x0400 + 1090;

        private static AppThread _instance = new AppThread();
        public static AppThread instance     
        {
            get {
                return _instance;
            }
        }

        private IntPtr _defaultHWND;
        public void start(IntPtr mainWnd)
        {
            _defaultHWND = mainWnd;
            Application.AddMessageFilter(new AppThreadTaskMessager());
        }

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
            int Msg,            // 消息ID
            int wParam,         // 参数1
            int lParam            // 参数2
        );

        private ConcurrentLinkedQueue<AppThreadTask> tasks = new ConcurrentLinkedQueue<AppThreadTask>();
        public void postTask(AppThreadTask task)
        {
            postTask(_defaultHWND, task);
        }
        public void postTask(IntPtr hWnd, AppThreadTask task)
        {
            tasks.offer(task);
            PostMessage(hWnd, THREAD_TASK_MESSAGE, 0, 0);
        }

        internal void run()
        {
            AppThreadTask task;
            while (tasks.poll(out task))
            {
                try
                {
                    task.run();
                }
                catch (Exception)
                {

                }
            }
        }

    }
}
