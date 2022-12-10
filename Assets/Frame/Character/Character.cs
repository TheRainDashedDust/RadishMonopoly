using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*/// <summary>
/// ��һ���
/// </summary>
public abstract class ICharacter
{
    private string Name;

    private CharacterAttr characterAttr;
    private GameObject m_GameObject = null;
    private Notification notification=null;
    //�������
    protected ICharacter(string name)
    {
        Name = name;
        characterAttr = new CharacterAttr(Name);
        notification = new Notification();
    }
}
/// <summary>
/// ������ݻ���
/// </summary>
public class CharacterAttr
{
    public string name;
    public int currentNode = -1;
    public int nextNode = 0;
    public int targetNode = 0;
    public int hp = 5;
    //��������£���������Ҫ��֤��ȫ�Ϳ���ԭ�򣬶���get����set
    public CharacterAttr(string name)
    {
        this.name = name;
        currentNode = -1;
        nextNode = 0;
        targetNode = 0;
        hp = 5;
    }
}*/
/// <summary>
/// ���,���Գ��������ICharacter��������CharacterAttr
/// </summary>
public class Character
{
    /// <summary>
    /// �����/���Ԥ����·��
    /// </summary>
    public string name;
    /// <summary>
    /// ���ʵ��
    /// </summary>
    public GameObject m_GameObject = null;
    /// <summary>
    /// ��ǰ�ڵ��±�
    /// </summary>
    public int currentNode=-1;
    /// <summary>
    /// ��һ�ڵ��±�
    /// </summary>
    public int nextNode=0;
    /// <summary>
    /// Ŀ��ڵ���
    /// </summary>
    public int targetNode=0;
    /// <summary>
    /// ����
    /// </summary>
    public int hp = 5;
    /// <summary>
    /// ����������Ϣ
    /// </summary>
    Notification notification;

    public Character(string name)
    {
        this.name = name;
        notification=new Notification();
    }
    /// <summary>
    /// ��ʼ����� ����Ϸ��ʼ����������ʼ����Ҫʹ�ã�
    /// </summary>
    public void Init()
    {
        if (name!=null||name!="")
        {
            m_GameObject = GameObject.Instantiate(Resources.Load<GameObject>(name));
            currentNode = 0;
            nextNode = 1;
            targetNode = 0;
        }
        
    }
    /// <summary>
    /// ����Ŀ��
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(int target)
    {
        this.targetNode = target;
    }
    /// <summary>
    /// ��Ϸ���������������
    /// </summary>
    public void Release()
    {
        if (m_GameObject!=null)
        {
            GameObject.Destroy(m_GameObject,3);
        }
    }
    /// <summary>
    /// ����
    /// </summary>
    public virtual void Update()
    {

    }
    /// <summary>
    /// ��ȡ��һ�ڵ�λ��
    /// </summary>
    /// <returns></returns>
    public Vector3 GetNextMapNode()
    {
        MapItem item = GameCenter.Instance.GetMapNode(nextNode);
        if (item == null)
        {
            Debug.Log(name+"ʤ��");
            Time.timeScale = 0;
            return GameCenter.Instance.GetMapNode(0).transform.position+new Vector3(0,0.25f,0);
        }
        if (item.character!=null)
        {
            //nextNode++;
            targetNode++;
        }
        return GameCenter.Instance.GetMapNode(nextNode).transform.position + new Vector3(0, 0.25f, 0);
    }
    /// <summary>
    /// ǰ��,����д
    /// </summary>
    public virtual void MoveTo(Vector3 pos)
    {
        m_GameObject.transform.DOMove(pos, 1).OnComplete(()=>
        {
            currentNode = nextNode;
            nextNode++;
            if (targetNode==1)
            {
                notification.Refresh("PlayerArrive", currentNode,this);
                MessageCenterByObserver.Instance.NotifyObserver(EventOrder.PLAYER_ARRIVE, notification);
                IsTriggerTrap();
                return;
            }
            else if(targetNode>1)
            {
                targetNode--;
                
                MoveTo(GetNextMapNode());
            }
        });
    }
    /// <summary>
    /// �������ƶ�����
    /// </summary>
    /// <param name="num"></param>
    public void Move(int num)
    {
        notification.Refresh("PlayerLeave", currentNode);
        MessageCenterByObserver.Instance.NotifyObserver(EventOrder.PLAYER_LEAVE, notification);
        targetNode = num;
        nextNode = currentNode + 1;
        MoveTo(GetNextMapNode());
    }
    /// <summary>
    /// ����ҽǶ��ж��Ƿ��������
    /// </summary>
    public void IsTriggerTrap()
    {
        MapItem item = GameCenter.Instance.GetMapNode(currentNode);
        if (item.trap!=null)
        {
            if (item.trap.type==1)
            {
                item.trap.TriggerEvent(this);
                Debug.Log("��������");
            }
            else
            {
                //item.character = this;
                Debug.Log("���ǹ̶����壬��Ҫ��ת�ܲ�����");
            }
        }
    }

}

