using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingSceneState : ISceneState
{
    LoadingUI loadingUI;
    public string m_nextSceneName;
    public AsyncOperation async;
    public LoadingSceneState(SceneStateController controller) : base(controller)
    {
        this.StateName = "LoadingSceneState";
        loadingUI = new LoadingUI();
    }
    public LoadingSceneState(SceneStateController controller, string nextSceneName) : base(controller)
    {
        this.StateName = "LoadingSceneState";
        m_nextSceneName= nextSceneName;
        loadingUI = new LoadingUI();
    }
    public override void StateBegin()
    {
        loadingUI.Init();
        if (m_nextSceneName!=null&& m_nextSceneName!="")
        {
            async= SceneManager.LoadSceneAsync(m_nextSceneName);
            async.allowSceneActivation = false;
        }
    }
    public override void StateEnd()
    {
        loadingUI.End();
        async = null;
        m_nextSceneName = string.Empty;
        
    }
    public override void StateUpdate()
    {
        loadingUI.progress =async.progress;
        loadingUI.Update();
        //Debug.Log(async.progress);
        if (loadingUI.progress>=0.99f&&async!=null)
        {
            async.allowSceneActivation = true;
            m_Controller.isLoading = false;
        }
    }
    
}

