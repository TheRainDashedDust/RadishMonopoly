using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽卡基类/工厂
/// </summary>
public abstract class ICard
{
    public int type { get;protected set; }
    public string des { get; protected set; }
    public int count { get; protected set; }
    protected ICard(int type)
    {
        this.type = type;
    }

}
/// <summary>
/// A类型卡
/// </summary>
public class CardA : ICard
{
    public CardA(int type=1) : base(type)
    {
        this.des = "前进一步";
        this.count = 1;
    }

}
/// <summary>
/// B类卡
/// </summary>
public class CardB : ICard
{
    public CardB(int type=2) : base(type)
    {
        this.des = "前进两步";
        this.count = 2;
    }

}
/// <summary>
/// C类卡
/// </summary>
public class CardC:ICard
{
    public CardC(int type=3) : base(type)
    {
        this.des = "前进三步";
        this.count = 3;
    }
}
/// <summary>
/// D类卡
/// </summary>
public class CardD : ICard
{
    public CardD(int type=0) : base(type)
    {
        this.des = "旋转萝卜";
        this.count = 0;
    }
}
