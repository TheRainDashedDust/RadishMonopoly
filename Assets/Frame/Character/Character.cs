using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*/// <summary>
/// 玩家基类
/// </summary>
public abstract class ICharacter
{
    private string Name;

    private CharacterAttr characterAttr;
    private GameObject m_GameObject = null;
    private Notification notification=null;
    //基类抽象
    protected ICharacter(string name)
    {
        Name = name;
        characterAttr = new CharacterAttr(Name);
        notification = new Notification();
    }
}
/// <summary>
/// 玩家数据基类
/// </summary>
public class CharacterAttr
{
    public string name;
    public int currentNode = -1;
    public int nextNode = 0;
    public int targetNode = 0;
    public int hp = 5;
    //正常情况下，所有数据要保证安全和开闭原则，对外get对内set
    public CharacterAttr(string name)
    {
        this.name = name;
        currentNode = -1;
        nextNode = 0;
        targetNode = 0;
        hp = 5;
    }
}*/
/// <summary>
/// 玩家,可以抽象出基类ICharacter和数据类CharacterAttr
/// </summary>
public class Character
{
    /// <summary>
    /// 玩家名/玩家预制体路径
    /// </summary>
    public string name;
    /// <summary>
    /// 玩家实体
    /// </summary>
    public GameObject m_GameObject = null;
    /// <summary>
    /// 当前节点下标
    /// </summary>
    public int currentNode=-1;
    /// <summary>
    /// 下一节点下标
    /// </summary>
    public int nextNode=0;
    /// <summary>
    /// 目标节点数
    /// </summary>
    public int targetNode=0;
    /// <summary>
    /// 命数
    /// </summary>
    public int hp = 5;
    /// <summary>
    /// 玩家自身的消息
    /// </summary>
    Notification notification;

    public Character(string name)
    {
        this.name = name;
        notification=new Notification();
    }
    /// <summary>
    /// 初始化玩家 （游戏开始和重生在起始点需要使用）
    /// </summary>
    public void Init()
    {
        if (name!=null||name!="")
        {
            m_GameObject = GameObject.Instantiate(Resources.Load<GameObject>(name));
            currentNode = 0;
            nextNode = 1;
            targetNode = 0;
        }
        
    }
    /// <summary>
    /// 设置目标
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(int target)
    {
        this.targetNode = target;
    }
    /// <summary>
    /// 游戏结束，将数据清除
    /// </summary>
    public void Release()
    {
        if (m_GameObject!=null)
        {
            GameObject.Destroy(m_GameObject,3);
        }
    }
    /// <summary>
    /// 更新
    /// </summary>
    public virtual void Update()
    {

    }
    /// <summary>
    /// 获取下一节点位置
    /// </summary>
    /// <returns></returns>
    public Vector3 GetNextMapNode()
    {
        MapItem item = GameCenter.Instance.GetMapNode(nextNode);
        if (item == null)
        {
            Debug.Log(name+"胜利");
            Time.timeScale = 0;
            return GameCenter.Instance.GetMapNode(0).transform.position+new Vector3(0,0.25f,0);
        }
        if (item.character!=null)
        {
            //nextNode++;
            targetNode++;
        }
        return GameCenter.Instance.GetMapNode(nextNode).transform.position + new Vector3(0, 0.25f, 0);
    }
    /// <summary>
    /// 前进,可重写
    /// </summary>
    public virtual void MoveTo(Vector3 pos)
    {
        m_GameObject.transform.DOMove(pos, 1).OnComplete(()=>
        {
            currentNode = nextNode;
            nextNode++;
            if (targetNode==1)
            {
                notification.Refresh("PlayerArrive", currentNode,this);
                MessageCenterByObserver.Instance.NotifyObserver(EventOrder.PLAYER_ARRIVE, notification);
                IsTriggerTrap();
                return;
            }
            else if(targetNode>1)
            {
                targetNode--;
                
                MoveTo(GetNextMapNode());
            }
        });
    }
    /// <summary>
    /// 外界调用移动方法
    /// </summary>
    /// <param name="num"></param>
    public void Move(int num)
    {
        notification.Refresh("PlayerLeave", currentNode);
        MessageCenterByObserver.Instance.NotifyObserver(EventOrder.PLAYER_LEAVE, notification);
        targetNode = num;
        nextNode = currentNode + 1;
        MoveTo(GetNextMapNode());
    }
    /// <summary>
    /// 从玩家角度判断是否踩中陷阱
    /// </summary>
    public void IsTriggerTrap()
    {
        MapItem item = GameCenter.Instance.GetMapNode(currentNode);
        if (item.trap!=null)
        {
            if (item.trap.type==1)
            {
                item.trap.TriggerEvent(this);
                Debug.Log("触发陷阱");
            }
            else
            {
                //item.character = this;
                Debug.Log("这是固定陷阱，需要旋转萝卜触发");
            }
        }
    }

}

