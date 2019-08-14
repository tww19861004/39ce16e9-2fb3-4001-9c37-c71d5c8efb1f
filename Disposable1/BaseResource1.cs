using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disposable1
{
    //一个资源安全的类，都实现了IDisposable接口和析构函数。提供手动释放资源和系统自动释放资源的双保险
    public class BaseResource : IDisposable
    {

        private IntPtr handle; // 句柄，属于非托管资源

        private Component comp; // 组件，托管资源

        private bool isDisposed = false; // 是否已释放资源的标志

        public BaseResource() { }

        //实现接口方法
        //由类的使用者，在外部显示调用，释放类资源
        public void Dispose()
        {
            Dispose(true);// 释放托管和非托管资源

            //将对象从垃圾回收器链表中移除，
            // 从而在垃圾回收器工作时，只释放托管资源，而不执行此对象的析构函数
            GC.SuppressFinalize(this);
        }

        //由垃圾回收器调用，释放非托管资源

        ~BaseResource()
        {
            Dispose(false);// 释放非托管资源
        }

        //参数为true表示释放所有资源，只能由使用者调用
        //参数为false表示释放非托管资源，只能由垃圾回收器自动调用
        //如果子类有自己的非托管资源，可以重载这个函数，添加自己的非托管资源的释放
        //但是要记住，重载此函数必须保证调用基类的版本，以保证基类的资源正常释放

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)// 如果资源未释放 这个判断主要用了防止对象被多次释放
            {
                if (disposing)
                {
                    comp.Dispose();// 释放托管资源
                }
                //closeHandle(handle);// 释放非托管资源
                handle = IntPtr.Zero;
            }
            this.isDisposed = true; // 标识此对象已释放
        }

    }
}
