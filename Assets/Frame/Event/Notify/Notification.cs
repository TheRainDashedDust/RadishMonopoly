using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 消息类
/// </summary>
public class Notification
{
    //消息类型
    public string msg;
    //存储消息的集合
    public object[] data;
    /// <summary>
    /// 通过对对象的刷新达到一个消息存储位置多次使用
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="data"></param>
    public void Refresh(string msg, params object[] data)
    {
        this.msg = msg;
        this.data = data;
    }
    /// <summary>
    /// 不用了就清除内存
    /// </summary>
    public void Clear()
    {
        msg = string.Empty;
        data = null;
    }
}

