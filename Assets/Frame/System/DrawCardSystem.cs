using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 抽卡系统
/// </summary>
public class DrawCardSystem : IGameSystem,IObserver
{
    /// <summary>
    /// 持有抽卡UI
    /// </summary>
    private DrawCardUI drawCardUI;

    Notification notification = null;
    public DrawCardSystem(GameCenter gameCenter) : base(gameCenter)
    {
        drawCardUI = new DrawCardUI();
        notification = new Notification();
        Initialize();
        MessageCenterByObserver.Instance.AddMessage(listNotification(),this);
    }

    public void HandleNotification(string key,Notification notification)
    {
        switch (key)
        {
            case "DrawCard":
                ICard card= DrawCard();
                drawCardUI.Refresh(card);
                this.notification.Refresh(EventOrder.CARD_RESULT, card);
                MessageCenterByObserver.Instance.NotifyObserver(EventOrder.CARD_RESULT, this.notification);
                break;
            default:
                break;
        }
    }
    public List<string> listNotification()
    {
        List<string> list = new List<string>() {
            EventOrder.DRAW_CARD,
        };
        return list;
    }
    /// <summary>
    /// 随机抽卡
    /// </summary>
    /// <returns></returns>
    public ICard DrawCard()
    {
        Random.InitState((int)DateTime.Now.Ticks);
        ICard card = null;
        int i= Random.Range(0, 4);
        switch (i)
        {
            case 0:
                card = new CardD(i);
                break;
            case 1:
                card = new CardA(i);
                break;
            case 2:
                card = new CardB(i);
                break;
            case 3:
                card = new CardC(i);
                break;
            default:
                break;
        }
        return card;
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        drawCardUI.Init();
        
    }

    
    /// <summary>
    /// 释放
    /// </summary>
    public override void Release()
    {
        base.Release();
        drawCardUI = null;
        MessageCenterByObserver.Instance.RemoveMessage(listNotification(), this);
    }
    /// <summary>
    /// 更新
    /// </summary>
    public override void Update()
    {
        base.Update();
    }
}

