using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

/// <summary>
/// 对外UI系统，由于本游戏只有抽奖UI，故下属于抽奖系统
/// </summary>
public class DrawCardUI
{
    public Button drawBtn,beginBtn;
    public Text result;

    /// <summary>
    /// 初始化UI
    /// </summary>
    public void Init()
    {
        result=UITool.FindUIGameObject("result").GetComponent<Text>();
        beginBtn=UITool.FindUIGameObject("Begin_Btn").GetComponent<Button>();
        beginBtn.onClick.AddListener(() =>
        {
            MessageCenterByObserver.Instance.NotifyObserver(EventOrder.INIT_PLAYER, null);
        });
        drawBtn = UITool.GetButton("Random");
        if (drawBtn!=null)
        {
            drawBtn.onClick.AddListener(() =>
            {
                MessageCenterByObserver.Instance.NotifyObserver(EventOrder.DRAW_CARD,null);
            });
        }
    }
    /// <summary>
    /// 刷新UI显示
    /// </summary>
    /// <param name="card"></param>
    public void Refresh(ICard card)
    {
        if (result!=null)
        {
            result.text = card.des;
        }
    }
}

