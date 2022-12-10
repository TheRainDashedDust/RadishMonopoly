using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 萝卜/关卡道具
/// </summary>
public class Radish
{
    /// <summary>
    /// 道具实体
    /// </summary>
    public GameObject gameObject;
    public Radish(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
    /// <summary>
    /// .道具方法
    /// </summary>
    public void RotateRadish()
    {
        Vector3 targetpos = gameObject.transform.eulerAngles + new Vector3(0, 45, 0);
        gameObject.transform.DORotate(targetpos, 2f).OnComplete(()=>
        {
            //萝卜旋转完判断玩家触发陷阱
            MessageCenterByObserver.Instance.NotifyObserver(EventOrder.ROTATE_RADISH_RESULT, null);
        });
    }
}

