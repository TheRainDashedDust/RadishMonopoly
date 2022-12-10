using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 玩家管理系统
/// </summary>
public class CharacterSystem : IGameSystem,IObserver
{
    /// <summary>
    /// 玩家库
    /// </summary>
    private List<Character> m_players=new List<Character>();
    /// <summary>
    /// 当前玩家下标
    /// </summary>
    private int currIndex=0;
    /// <summary>
    /// 本局玩家
    /// </summary>
    private Character m_currCharacter;
    /// <summary>
    /// 消息
    /// </summary>
    private Notification notification = null;

    public CharacterSystem(GameCenter gameCenter) : base(gameCenter)
    {
        //初始化玩家系统
        Initialize();
        //订购
        MessageCenterByObserver.Instance.AddMessage(listNotification(), this);
    }
    /// <summary>
    /// 添加玩家
    /// </summary>
    /// <param name="player"></param>
    public void AddPlayer(Character player)
    {
        if (!m_players.Contains(player))
        {
            m_players.Add(player);
        }
        else
        {
            Debug.Log(player.name+"用户名不能重复添加");
        }
       
    }
    /// <summary>
    /// 删除玩家
    /// </summary>
    /// <param name="player"></param>
    public void RemovePlayer(Character player)
    {
        if (m_players.Contains(player))
        {
            m_players.Remove(player);
        }
        
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        AddPlayer(new Character("RedPlayer"));
        AddPlayer(new Character("BluePlayer"));
        m_currCharacter = m_players[currIndex];
        
        
    }
    /// <summary>
    /// 初始化玩家
    /// </summary>
    public void InitPlayer()
    {
        for (int i = 0; i < m_players.Count; i++)
        {
            m_players[i].Init();
            Notification notify = new Notification();
            notify.Refresh(EventOrder.CREATE_PLAYER, m_players[i]);
            MessageCenterByObserver.Instance.NotifyObserver(EventOrder.CREATE_PLAYER, notify);
        }
    }
    /// <summary>
    /// 释放
    /// </summary>
    public override void Release()
    {
        base.Release();
        m_players.Clear();
    }
    /// <summary>
    /// 更新
    /// </summary>
    public override void Update()
    {
        base.Update();
    }

    public List<string> listNotification()
    {
        List<string> list = new List<string>()
        {
            EventOrder.INIT_PLAYER,
            EventOrder.CARD_RESULT,
        };

        return list;
    }

    public void HandleNotification(string key, Notification notification)
    {
        switch (key)
        {
            case "InitPlayer":
                InitPlayer();
                break;
            case "CardResult":
                PlayerMove(notification.data[0] as ICard);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 当前玩家抽卡结果进行响应
    /// </summary>
    /// <param name="card"></param>
    public void PlayerMove(ICard card)
    {
        switch (card.count)
        {
            case 0:
                Debug.Log("旋转萝卜");
                //notification.Refresh(EventOrder.ROTATE_RADISH,)
                MessageCenterByObserver.Instance.NotifyObserver(EventOrder.ROTATE_RADISH, null);
                break;
            case 1:
                Debug.Log("当前玩家前进1步");
                m_currCharacter.Move(1);
                break;
            case 2:
                Debug.Log("当前玩家前进2步");
                m_currCharacter.Move(2);
                break;
            case 3:
                Debug.Log("当前玩家前进3步");
                m_currCharacter.Move(3);
                
                break;
            default:
                break;
        }
        if (currIndex<m_players.Count-1)
        {
            currIndex++;
        }
        else
        {
            currIndex = 0;
        }
        
        m_currCharacter = m_players[currIndex];

    }
}

