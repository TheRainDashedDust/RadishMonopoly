using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 一个脚本实现的内容
/// </summary>
public enum EventType
{
    None,前进1步,前进2步,前进3步,旋转萝卜,
}
public class ChessGame : MonoBehaviour
{
    public Button randomBtn;
    public Text actionContent;
    public Transform initialPoint;
    public List<Transform> nodes= new List<Transform>();
    public GameObject radish;
    public GameObject redPlayer, bluePlayer;
    public bool Bout=true;
    public List<int> traps = new List<int>();
    public int trapNum = 3;
    public int redCurr, blueCurr;
    
    // Start is called before the first frame update
    void Start()
    {
        randomBtn.onClick.AddListener(RandomEvent);
        InitGame();
    }
    public void InitGame()
    {
        //Random.InitState(100);
        traps.Clear();
        for (int i = 0; i < trapNum; i++)
        {
            int j = Random.Range(2, nodes.Count);
            if (!traps.Contains(j))
            {
                traps.Add(j);
            }
            else
            {
                i--;
            }
        }
        
        for (int i = 0; i < nodes.Count; i++)
        {
            if (traps.Contains(i))
            {
                nodes[i].GetComponent<Renderer>().material.color = Color.black;
                nodes[i].GetComponent<BoxCollider>().isTrigger= true;
            }
            else
            {
                nodes[i].GetComponent<Renderer>().material.color = Color.white;
                nodes[i].GetComponent<BoxCollider>().isTrigger = false;
            }
            if (i== nodes.Count-1)
            {
                DrawLS(nodes[i].gameObject, radish);
            }
            else
            {
                DrawLS(nodes[i].gameObject, nodes[i+1].gameObject);
            }
            
        }
        
    }
    void DrawLS(GameObject startP, GameObject finalP)
    {
        Vector3 rightPosition = (startP.transform.position + finalP.transform.position)/ 2;
        Vector3 rightRotation = finalP.transform.position - startP.transform.position;
        float HalfLength = Vector3.Distance(startP.transform.position, finalP.transform.position) / 2;
        float LThickness = 0.01f;// 线的粗细
                                //创建圆柱体
        GameObject MyLine = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //MyLine.gameObject.transform.parent = transform;
        MyLine.transform.position = rightPosition;
        MyLine.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
        MyLine.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
        MyLine.GetComponent<Renderer>().material.color = Color.yellow;
        MyLine.transform.SetParent(GameObject.Find("Line").transform);
        //这里可以设置材质，具体自己设置
        //M/yLine.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
    }



    public void RandomEvent()
    {
        //Random.InitState(1000);
        int i = Random.Range(1, 5);
        actionContent.text = ((EventType)i).ToString();
        if (i < 4)
        {
            if (Bout)
            {
                redCurr += i;
                if (redCurr > nodes.Count)
                {
                    Debug.Log("红方获胜");
                    //Time.timeScale = 0;
                    return;
                }
                if (redCurr == blueCurr)
                {
                    Debug.Log("蓝方在此位置，跳过");
                    redCurr++;
                }
                Debug.Log("红方玩家:" + actionContent.text);
                SetPlayerPos(redPlayer, redCurr);
                Bout = false;

            }
            else
            {
                blueCurr += i;
                if (blueCurr > nodes.Count)
                {
                    Debug.Log("蓝方获胜");
                    //Time.timeScale = 0;
                    return;
                }
                if (redCurr == blueCurr)
                {
                    Debug.Log("红方在此位置，跳过");
                    blueCurr++;
                }
                Debug.Log("蓝方玩家:" + actionContent.text);
                SetPlayerPos(bluePlayer, blueCurr);
                Bout = true;
            }
        }
        else if(i==4)
        {
            Debug.Log("旋转萝卜，重置陷阱");
            InitGame();
            SetPlayerPos(bluePlayer, blueCurr);
            SetPlayerPos(redPlayer, redCurr);
        }
    }
    public void SetPlayerPos(GameObject player,int curr)
    {
        if (curr==0||curr>=nodes.Count)
        {
            return;
        }   
        player.transform.position = nodes[curr].transform.position + new Vector3(0, 0.01f, 0);
        if (traps.Contains(curr))
        {
            player.transform.Translate(transform.up * 1f);
            Debug.Log(player.name + "踩到陷阱,失败");
            //randomBtn.interactable = false;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
