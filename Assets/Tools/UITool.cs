using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UI管理工具
/// </summary>
public static class UITool
{
	private static GameObject m_CanvasObj = null; // 场景上的2D画布物体

	public static void ReleaseCanvas()
	{
		m_CanvasObj = null;
	}

	// 查找限定在Canvas下的UI界面
	public static GameObject FindUIGameObject(string UIName)
	{
		if(m_CanvasObj == null)
			m_CanvasObj = UnityTool.FindGameObject( "Canvas" );
		if(m_CanvasObj ==null)
			return null;
		return UnityTool.FindChildGameObject( m_CanvasObj, UIName);
	}
	
	// 取得UI元件
	public static T GetUIComponent<T>(GameObject Container,string UIName) where T : UnityEngine.Component
	{
		// 找出子物件 
		GameObject ChildGameObject = UnityTool.FindChildGameObject( Container, UIName);
		if( ChildGameObject == null)
			return null;
		
		T tempObj = ChildGameObject.GetComponent<T>();
		if( tempObj == null)
		{
			Debug.LogWarning("元件["+UIName+"]不是["+ typeof(T) +"]");		
			return null;
		}
		return tempObj;
	}
	
	public static Button GetButton(string BtnName)
	{
		// 取得Canvas
		GameObject UIRoot = GameObject.Find("Canvas");
		if(UIRoot==null)
		{
			Debug.LogWarning("场景上沒有UI Canvas");
			return null;
		}
		
		// 找出对应的Button
		Transform[] allChildren = UIRoot.GetComponentsInChildren<Transform>();
		foreach(Transform child in allChildren)
		{
			if( child.name == BtnName ) 
			{
				Button tmpBtn = child.gameObject.GetComponent<Button>();
				if(tmpBtn == null)				
					Debug.LogWarning("UI原件["+BtnName+"]不是Button");
				return tmpBtn;
			}	
		}

		Debug.LogWarning("UI Canvas中没有Button["+BtnName+"]存在");
		return null;
	}

	// 取得UI元件
	public static T GetUIComponent<T>(string UIName) where T : UnityEngine.Component
	{
		// 取得Canvas
		GameObject UIRoot = GameObject.Find("Canvas");
		if(UIRoot==null)
		{
			Debug.LogWarning("场景上没有UI Canvas");
			return null;
		}
		return GetUIComponent<T>( UIRoot,UIName); 
	}
}
