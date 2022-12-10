using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class DrawLineTool
{
    /// <summary>
    /// 3D画线
    /// </summary>
    /// <param name="startP">起始对象</param>
    /// <param name="finalP">终止对象</param>
    public static void DrawLS(GameObject startP, GameObject finalP)
    {
        Vector3 rightPosition = (startP.transform.position + finalP.transform.position) / 2;
        Vector3 rightRotation = finalP.transform.position - startP.transform.position;
        float HalfLength = Vector3.Distance(startP.transform.position, finalP.transform.position) / 2;
        float LThickness = 0.01f;// 线的粗细
                                 //创建圆柱体
        GameObject MyLine = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //MyLine.gameObject.transform.parent = GameObject.Find("Line").transform;
        MyLine.transform.position = rightPosition;
        MyLine.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
        MyLine.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
        MyLine.GetComponent<Renderer>().material.color = Color.yellow;
        MyLine.transform.SetParent(GameObject.Find("Line").transform);
        //这里可以设置材质，具体自己设置
        //M/yLine.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
    }
    /// <summary>
    /// 3D画线
    /// </summary>
    /// <param name="startP">起始对象</param>
    /// <param name="finalP">终止对象</param>
    /// <param name="lThickness">线的粗细</param>
    public static void DrawLS(GameObject startP, GameObject finalP,float lThickness)
    {
        Vector3 rightPosition = (startP.transform.position + finalP.transform.position) / 2;
        Vector3 rightRotation = finalP.transform.position - startP.transform.position;
        float HalfLength = Vector3.Distance(startP.transform.position, finalP.transform.position) / 2;
        float LThickness = lThickness;// 线的粗细
                                 //创建圆柱体
        GameObject MyLine = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //MyLine.gameObject.transform.parent = GameObject.Find("Line").transform;
        MyLine.transform.position = rightPosition;
        MyLine.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
        MyLine.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
        MyLine.GetComponent<Renderer>().material.color = Color.yellow;
        MyLine.transform.SetParent(GameObject.Find("Line").transform);
        //这里可以设置材质，具体自己设置
        //MyLine.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
    }
    /// <summary>
    /// 3D画线
    /// </summary>
    /// <param name="startP">起始对象</param>
    /// <param name="finalP">终止对象</param>
    /// <param name="lThickness">线的粗细</param>
    /// <param name="material">线的材质</param>
    public static void DrawLS(GameObject startP, GameObject finalP, float lThickness,Material material)
    {
        Vector3 rightPosition = (startP.transform.position + finalP.transform.position) / 2;
        Vector3 rightRotation = finalP.transform.position - startP.transform.position;
        float HalfLength = Vector3.Distance(startP.transform.position, finalP.transform.position) / 2;
        float LThickness = lThickness;// 线的粗细
                                      //创建圆柱体
        GameObject MyLine = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //MyLine.gameObject.transform.parent = GameObject.Find("Line").transform;
        MyLine.transform.position = rightPosition;
        MyLine.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
        MyLine.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
        MyLine.GetComponent<Renderer>().material.color = Color.yellow;
        MyLine.transform.SetParent(GameObject.Find("Line").transform);
        //这里可以设置材质，具体自己设置
        MyLine.GetComponent<MeshRenderer>().material = material;
    }
    public static void GLDrawLS()
    {
        
    }
}

