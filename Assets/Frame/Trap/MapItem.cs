using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 地图块/关卡地图的重要组成部分
/// </summary>
public class MapItem
{
    /// <summary>
    /// 实体,Transform/GameObject
    /// </summary>
    public Transform transform;
    /// <summary>
    /// 玩家/本游戏采用跳棋，判断节点上有无玩家
    /// </summary>
    public Character character;
    /// <summary>
    /// 陷阱/判断有无陷阱
    /// </summary>
    public ITrap trap;
    /// <summary>
    /// 对应地图上的节点下标
    /// </summary>
    public int nodeIndex;
    //构造
    public MapItem(Transform transform, int nodeIndex)
    {
        this.transform = transform;
        this.nodeIndex = nodeIndex;
    }
}

