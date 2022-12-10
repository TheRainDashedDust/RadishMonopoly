using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

/// <summary>
/// 观察者事件中心
/// </summary>
public class MessageCenterByObserver:Singleton<MessageCenterByObserver>
{
    //单例
    /// <summary>
    /// 观察者库，键为观察的内容，值为对应的观察者群组
    /// </summary>
    public Dictionary<string,List<IObserver>> MCDic = new Dictionary<string, List<IObserver>>();

    /// <summary>
    /// 增加对应观察者
    /// </summary>
    /// <param name="key"></param>
    /// <param name="observer"></param>
    public void AddMessage(string key,IObserver observer)
    {
        if (MCDic.ContainsKey(key))
        {
            if (!MCDic[key].Contains(observer))
                MCDic[key].Add(observer);
        }
        else
        {
            MCDic.Add(key, new List<IObserver>() { observer });
        }
    }
    /// <summary>
    /// 以一个观察者多个观察内容的方式添加
    /// </summary>
    /// <param name="keys">订购内容</param>
    /// <param name="observer">观察者</param>
    public void AddMessage(List<string> keys, IObserver observer)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (MCDic.ContainsKey(keys[i]))
            {
                if (!MCDic[keys[i]].Contains(observer))
                    MCDic[keys[i]].Add(observer);
            }
            else
            {
                MCDic.Add(keys[i], new List<IObserver>() { observer });
            }
        }
        
    }
    /// <summary>
    /// 删除对应观察者
    /// </summary>
    /// <param name="key"></param>
    /// <param name="observer"></param>
    public void RemoveMessage(string key, IObserver observer)
    {
        if (MCDic.ContainsKey(key))
        {
            if (MCDic[key].Contains(observer))
            {
                MCDic[key].Remove(observer);
            }
            if (MCDic[key].Count==0)
            {
                MCDic.Remove(key);
            }
        }
    }
    /// <summary>
    /// 以一个观察者多个观察内容的方式删除
    /// </summary>
    /// <param name="keys"></param>
    /// <param name="observer"></param>
    public void RemoveMessage(List<string> keys, IObserver observer)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (MCDic.ContainsKey(keys[i]))
            {
                if (MCDic[keys[i]].Contains(observer))
                {
                    MCDic[keys[i]].Remove(observer);
                }
                if (MCDic[keys[i]].Count == 0)
                {
                    MCDic.Remove(keys[i]);
                }
            }
        }
       
    }
    /// <summary>
    /// 给对应的观察者派发事件
    /// </summary>
    /// <param name="eventKey"></param>
    /// <param name="notification"></param>
    public virtual void NotifyObserver(string eventKey,Notification notification)
    {
        if (MCDic.ContainsKey(eventKey))
        {
            foreach (var item in MCDic[eventKey])
            {
                item.HandleNotification(eventKey,notification);
            }
        }
        
    }
}

