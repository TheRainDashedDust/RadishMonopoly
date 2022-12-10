using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 事件内容
/// </summary>
public class EventOrder
{
    /// <summary>
    /// 抽卡
    /// </summary>
    public static string DRAW_CARD = "DrawCard";
    /// <summary>
    /// 抽卡结果
    /// </summary>
    public static string CARD_RESULT = "CardResult";
    /// <summary>
    /// 创建玩家
    /// </summary>
    public static string CREATE_PLAYER = "CreatePlayer";
    /// <summary>
    /// 初始化玩家
    /// </summary>
    public static string INIT_PLAYER = "InitPlayer";
    /// <summary>
    /// 玩家离开这个格子
    /// </summary>
    public static string PLAYER_LEAVE = "PlayerLeave";
    /// <summary>
    /// 玩家到达这个格子
    /// </summary>
    public static string PLAYER_ARRIVE = "PlayerArrive";
    /// <summary>
    /// 旋转萝卜
    /// </summary>
    public static string ROTATE_RADISH = "RotateRadish";
    /// <summary>
    /// 旋转萝卜后触发事件
    /// </summary>
    public static string ROTATE_RADISH_RESULT = "RotateRadishResult";
    

}

