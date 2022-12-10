using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏系统基类
/// </summary>
public abstract class IGameSystem 
{
    /// <summary>
    /// 系统绑定游戏中心外观
    /// </summary>
    public GameCenter gameCenter = null;

    protected IGameSystem(GameCenter gameCenter)
    {
        this.gameCenter = gameCenter;
    }

    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }
}
