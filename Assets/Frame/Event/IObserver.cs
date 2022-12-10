using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 观察者接口
/// </summary>
public interface IObserver
{
    /// <summary>
    /// 订购事件内容
    /// </summary>
    /// <returns></returns>
    List<string> listNotification();
    /// <summary>
    /// 回调
    /// </summary>
    /// <param name="key"></param>
    /// <param name="notification"></param>
    void HandleNotification(string key,Notification notification);
}

