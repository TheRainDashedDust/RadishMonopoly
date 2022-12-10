using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 陷阱
/// </summary>
public abstract class ITrap
{
    public int type { get;private set; }
    public GameObject m_TrapObject=null;
    public int currIndex;
    Notification notification;
    public ITrap(int type,int index)
    {
        this.type = type;
        this.currIndex = index;
        notification=new Notification();
    }

    
    public void SetGameObject(GameObject gameObject)
    {
        this.m_TrapObject = gameObject;
        this.Init();
    }
    /// <summary>
    /// 初始化陷阱
    /// </summary>
    public virtual void Init()
    {
        
    }
    /// <summary>
    /// 陷阱触发事件
    /// </summary>
    public virtual void TriggerEvent(Character player)
    {
        notification.Refresh("PlayerLeave", player.currentNode);
        MessageCenterByObserver.Instance.NotifyObserver(EventOrder.PLAYER_LEAVE, notification);
        if (player.hp>0)
        {
            player.hp--;
            GameObject.Destroy(player.m_GameObject, 2f);
            player.Init();
            Debug.Log(player.name + "玩家生命值:" + player.hp);
        }
        else
        {
            Time.timeScale= 0;
            Debug.Log("游戏结束,"+player.name+"玩家生命值:"+player.hp);
        }
        
    }
    /// <summary>
    /// 陷阱失效
    /// </summary>
    public virtual void TrapClose()
    {

    }
}
/// <summary>
/// 坑洞陷阱1
/// </summary>
public class HoleTrap : ITrap
{
    public HoleTrap(int type, int index) : base(type, index)
    {
    }

    public override void Init()
    {
        base.Init();
        m_TrapObject.GetComponent<Renderer>().material.color = Color.black;
        m_TrapObject.GetComponent<BoxCollider>().isTrigger= true;
    }
    public override void TriggerEvent(Character player)
    {
        
        player.m_GameObject.GetComponent<BoxCollider>().isTrigger = true;
        player.m_GameObject.transform.Translate(Vector3.up * 1);
        /*player.hp --;
        GameObject.Destroy(player.m_GameObject, 2f);
        player.Init();*/
        base.TriggerEvent(player);
    }
    public override void TrapClose()
    {
        base.TrapClose();
        if (m_TrapObject!=null)
        {
            m_TrapObject.GetComponent<Renderer>().material.color = Color.white;
            m_TrapObject.GetComponent<BoxCollider>().isTrigger = false;

        }
        
    }
}

/// <summary>
/// 弹射陷阱2
/// </summary>
public class CatapultTrap : ITrap
{
    public CatapultTrap(int type, int index) : base(type, index)
    {
    }

    public override void Init()
    {
        base.Init();
        m_TrapObject.GetComponent<Renderer>().material.color = Color.yellow;
        m_TrapObject.GetComponent<BoxCollider>().isTrigger = false;
    }
    public override void TriggerEvent(Character player)
    {
        
        player.m_GameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000);
        base.TriggerEvent(player);
    }
    public override void TrapClose()
    {
        base.TrapClose();
        if (m_TrapObject!=null)
        {
            m_TrapObject.GetComponent<Renderer>().material.color = Color.white;
        }
        
    }
}
