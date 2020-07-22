using System;
using System.Diagnostics;
using Xunit;

namespace AsyncDelegates
{
    public class UnitTest1
    {
        private void DoWork()
        {
            Debug.WriteLine("hello world");
            Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
        }

        delegate void DoWorkDelegate();

        [Fact]
        public void Demo01()
        {
            Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
            DoWorkDelegate doWorkDelegate = new DoWorkDelegate(DoWork);
            //doWorkDelegate();
            AsyncCallback callback = new AsyncCallback(Callback);
            IAsyncResult ar = doWorkDelegate.BeginInvoke(callback, doWorkDelegate);
        }

        private static void Callback(IAsyncResult ar)
        {
            var doWorkDelegate = ar.AsyncState as DoWorkDelegate;
            doWorkDelegate.EndInvoke(ar);
        }
    }
}
