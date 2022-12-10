using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �鿨����/����
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
/// A���Ϳ�
/// </summary>
public class CardA : ICard
{
    public CardA(int type=1) : base(type)
    {
        this.des = "ǰ��һ��";
        this.count = 1;
    }

}
/// <summary>
/// B�࿨
/// </summary>
public class CardB : ICard
{
    public CardB(int type=2) : base(type)
    {
        this.des = "ǰ������";
        this.count = 2;
    }

}
/// <summary>
/// C�࿨
/// </summary>
public class CardC:ICard
{
    public CardC(int type=3) : base(type)
    {
        this.des = "ǰ������";
        this.count = 3;
    }
}
/// <summary>
/// D�࿨
/// </summary>
public class CardD : ICard
{
    public CardD(int type=0) : base(type)
    {
        this.des = "��ת�ܲ�";
        this.count = 0;
    }
}
