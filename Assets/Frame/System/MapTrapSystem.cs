using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Random = UnityEngine.Random;

/// <summary>
/// 地图管理系统/子系统
/// </summary>
public class MapTrapSystem : IGameSystem ,IObserver
{
    /// <summary>
    /// 陷阱库 陷阱对应的下标位置作key，陷阱作value
    /// </summary>
    private Dictionary<int, ITrap> m_Traps = new Dictionary<int, ITrap>();
    /// <summary>
    /// 地图节点
    /// </summary>
    private List<MapItem> nodes = new List<MapItem>();
    /// <summary>
    /// 场景道具
    /// </summary>
    private Radish radish = null;

    public MapTrapSystem(GameCenter gameCenter) : base(gameCenter)
    {
        Initialize();
        MessageCenterByObserver.Instance.AddMessage(listNotification(), this);
    }
    /// <summary>
    /// 获取场景节点
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public MapItem GetMapNode(int index)
    {
        if (index<nodes.Count-1)
        {
            return nodes[index];
        }
        return null;
    }
    /// <summary>
    /// 获取场景节点数
    /// </summary>
    /// <returns></returns>
    public int GetNodesCount()
    {
        return nodes.Count;
    }
    /// <summary>
    /// 添加陷阱
    /// </summary>
    /// <param name="index"></param>
    /// <param name="tr"></param>
    public void AddTrap(int index,ITrap tr)
    {
        if (!m_Traps.ContainsKey(index))
        {
            m_Traps.Add(index,tr);
        }
        
    }
    /// <summary>
    /// 删除陷阱
    /// </summary>
    /// <param name="index"></param>
    /// <param name="trap"></param>
    public void RemoveTrap(int index,ITrap trap)
    {
        if (m_Traps.ContainsKey(index))
        {
            trap.TrapClose();
            m_Traps.Remove(index);
        }
        
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        
        GameObject plane = UnityTool.FindGameObject("Plane");
        for (int i = 0; i < plane.transform.childCount; i++)
        {
            //nodes.Add(plane.transform.GetChild(i));
            nodes.Add(new MapItem(plane.transform.GetChild(i), i));
        }
        for (int i = 0; i < nodes.Count; i++)
        {
           
            if (i == nodes.Count - 1)
            {
                DrawLineTool.DrawLS(nodes[i].transform.gameObject, UnityTool.FindGameObject("Radish"));
            }
            else
            {
                DrawLineTool.DrawLS(nodes[i].transform.gameObject, nodes[i + 1].transform.gameObject);
            }

        }
        radish = new Radish(UnityTool.FindGameObject("Radish"));
        RefreshTrap();
    }
    /// <summary>
    /// 旋转萝卜
    /// </summary>
    public void RotateRadish()
    {
        if (radish!=null)
        {
            radish.RotateRadish();
        }
    }
    /// <summary>
    /// 初始化陷阱
    /// </summary>
    /// <param name="trapCount"></param>
    public void InitTrap(int trapCount)
    {
        m_Traps.Clear();
        for (int i = 0; i < trapCount; i++)
        {
            int j = Random.Range(2, nodes.Count);
            int k = Random.Range(1, 3);
            ITrap trap = CreateTrap(k, j);
            if (!m_Traps.ContainsKey(j))
            {
                AddTrap(j, trap);
            }
            else
            {
                i--;
            }
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            if (m_Traps.ContainsKey(i))
            {
                m_Traps[i].SetGameObject(nodes[i].transform.gameObject);
                m_Traps[i].Init();
                nodes[i].trap = m_Traps[i];
            }
            else
            {
                if (nodes[i].trap!=null)
                {
                    nodes[i].trap.TrapClose();
                    nodes[i].trap = null;
                }
                
            }
        }
    }
    /// <summary>
    /// 实例化陷阱
    /// </summary>
    /// <param name="type"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public ITrap CreateTrap(int type,int index)
    {
        switch (type)
        {
            case 1:
                return new HoleTrap(type, index);
            case 2:
                return new CatapultTrap(type, index);
            default:
                return null;
        }
    }
    /// <summary>
    /// 刷新陷阱/场景开始时调用,当xu要刷新或者重新布局时
    /// </summary>
    public void RefreshTrap()
    {
        InitTrap(5);
    }
    /// <summary>
    /// 更新
    /// </summary>
    public override void Update()
    {

    }
    /// <summary>
    /// 释放
    /// </summary>
    public override void Release()
    {
        base.Release();
        foreach (var item in m_Traps)
        {
            item.Value.TrapClose();
        }
        m_Traps.Clear();
        nodes.Clear();
    }
    /// <summary>
    /// 旋转萝卜触发所有陷阱
    /// </summary>
    public void RadishTriggerTrap()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (m_Traps.ContainsKey(i))
            {
                if (nodes[i].character!=null)
                {
                    m_Traps[i].TriggerEvent(nodes[i].character);
                }
            }
        }
    }
    /// <summary>
    /// 关注的内容
    /// </summary>
    /// <returns></returns>
    public List<string> listNotification()
    {
        List<string> list = new List<string>()
        {
            EventOrder.CREATE_PLAYER,
            EventOrder.PLAYER_LEAVE,
            EventOrder.PLAYER_ARRIVE,
            EventOrder.ROTATE_RADISH,
            EventOrder.ROTATE_RADISH_RESULT,
            
        };
        return list;
    }
    
    /// <summary>
    /// 观察者回调
    /// </summary>
    /// <param name="key"></param>
    /// <param name="notification"></param>
    public void HandleNotification(string key, Notification notification)
    {
        switch (key)
        {
            case "CreatePlayer":
                //创建玩家
                //Character player = notification.data[0] as Character;
                //player.MoveTo(nodes[0].position);
                break;
            case "PlayerLeave":
                int index = (int)notification.data[0];
                if (index<nodes.Count&&index>=0)
                {
                    nodes[index].character = null;
                }
                
                break;
            case "PlayerArrive":
                int indexA = (int)notification.data[0];
                Character character = notification.data[1] as Character;
                nodes[indexA].character = character;
                break;
            case "RotateRadish":
                RotateRadish();
                break;
            case "RotateRadishResult":
                RadishTriggerTrap();
                break;
            
            default:
                break;
        }
    }
}

