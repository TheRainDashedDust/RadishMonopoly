using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StartSceneState : ISceneState
{
    public StartSceneState(SceneStateController controller) : base(controller)
    {
        this.StateName = "StartState";
    }
    public override void StateBegin()
    {
        base.StateBegin();
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        m_Controller.SetState(new GameSceneState(m_Controller), "Exercise");
    }
}

