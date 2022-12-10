using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GameSceneState : ISceneState
{
    public GameSceneState(SceneStateController controller) : base(controller)
    {
        this.StateName = "GameState";
    }
    public override void StateBegin()
    {
        base.StateBegin();
        GameCenter.Instance.Initinal();
    }
    public override void StateEnd()
    {
        base.StateEnd();
        GameCenter.Instance.Release();
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        GameCenter.Instance.Update();
        if (GameCenter.Instance.GetGameState())
        {
            m_Controller.SetState(new StartSceneState(m_Controller), "StartScene");
        }
    }
}

