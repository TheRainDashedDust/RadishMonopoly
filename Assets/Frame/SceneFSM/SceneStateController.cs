using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
    
    private ISceneState m_state;
    private bool m_RunBegin=false;
    private AsyncOperation async;
    LoadingSceneState loadingSceneState;
    public bool isLoading = false;
    public void SetStateAsync(ISceneState state, string loadSceneName)
    {
        isLoading= true;

        SetState(new LoadingSceneState(this,loadSceneName), "LoadingScene");
    }
    public void SetState(ISceneState state,string loadSceneName)
    {
        m_RunBegin = false;
        LoadSceneAsync(loadSceneName);
        if (m_state!=null)
        {
            m_state.StateEnd();
        }
        m_state= state; 

    }
    
    public void LoadSceneAsync(string loadSceneName)
    {
        if (loadSceneName == null || loadSceneName == "")
        {
            return;
        }
        async = SceneManager.LoadSceneAsync(loadSceneName);
        
        
    }
    public void StateUpdate()
    {
        #region
        if (isLoading)
        {
            if (async.isDone)
            {
                if (m_state != null && m_RunBegin == false)
                {
                    m_state.StateBegin();
                    m_RunBegin = true;
                }
                if (m_state != null)
                {
                    m_state.StateUpdate();
                }

            }

            return;
        }


        if (async == null|| async.isDone)
        {
            if (m_state != null && m_RunBegin == false)
            {
                m_state.StateBegin();
                m_RunBegin = true;
            }
            if (m_state != null)
            {
                m_state.StateUpdate();
            }

            return;
        }
       /* if (!async.isDone)
        {
            return;
        }

        if (m_state != null && m_RunBegin == false)
        {
            m_state.StateBegin();
            m_RunBegin = true;
        }
        if (m_state != null)
        {

            m_state.StateUpdate();
        }*/
        #endregion

    }
}

