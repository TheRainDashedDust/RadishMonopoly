using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 游戏入口
/// </summary>
public class GameLoop : MonoBehaviour
{
    SceneStateController stateController;
    private void Awake()
    {
        stateController = new SceneStateController();
        GameObject.DontDestroyOnLoad(this.gameObject);
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
    }
    // Start is called before the first frame update
    void Start()
    {
        stateController.SetState(new StartSceneState(stateController), "");
        //对外统一入口，外观模式
        //GameCenter.Instance.Initinal();
    }
    // Update is called once per frame
    void Update()
    {
        stateController.StateUpdate();
        //GameCenter.Instance.Update();
    }
    private void OnDisable()
    {
        //GameCenter.Instance.Release();
    }
}
