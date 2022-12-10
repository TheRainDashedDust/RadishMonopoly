using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸϵͳ����
/// </summary>
public abstract class IGameSystem 
{
    /// <summary>
    /// ϵͳ����Ϸ�������
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
