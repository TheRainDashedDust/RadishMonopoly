using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 游戏中心,对外外观,对内中介,采用单例
/// </summary>
public class GameCenter:Singleton<GameCenter>
{
    private bool isOver = false;
    /// <summary>
    /// 玩家管理系统
    /// </summary>
    private CharacterSystem m_CharacterSystem = null;
    /// <summary>
    /// 地图管理系统
    /// </summary>
    private MapTrapSystem m_MapTrapSystem = null;
    /// <summary>
    /// 抽奖管理系统
    /// </summary>
    private DrawCardSystem m_DrawCardSystem = null;
    /// <summary>
    /// 初始化
    /// </summary>
    public void Initinal()
    {
        isOver = false;
        m_DrawCardSystem = new DrawCardSystem(this);
        m_MapTrapSystem = new MapTrapSystem(this);
        m_CharacterSystem = new CharacterSystem(this);
    }
    
    /// <summary>
    /// 更新
    /// </summary>
    public void Update()
    {
        m_DrawCardSystem.Update();
        m_MapTrapSystem.Update();
        m_CharacterSystem.Update();

    }
    /// <summary>
    /// 释放
    /// </summary>
    public void Release()
    {
        m_DrawCardSystem.Release();
        m_MapTrapSystem.Release();
        m_CharacterSystem.Release();
    }
    /// <summary>
    /// 获取地图系统对应节点/中介
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public MapItem GetMapNode(int index)
    {
        if (index>= m_MapTrapSystem.GetNodesCount())
        {
            Debug.Log("游戏结束");
        }
        return  m_MapTrapSystem.GetMapNode(index);
    }
    public bool GetGameState()
    {
        return isOver;
    }
}

