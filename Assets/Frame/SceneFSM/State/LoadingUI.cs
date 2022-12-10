using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI
{
    public Slider slider;
    public Text text_progress;
    public float progress;
    public void Init()
    {
        slider = UITool.FindUIGameObject("Slider").GetComponent<Slider>();
        text_progress = UITool.FindUIGameObject("Progress_Text").GetComponent<Text>();
    }
    public void Update()
    {
        if (slider==null||text_progress==null)
        {
            return;
        }
        slider.value=Mathf.MoveTowards(slider.value, progress, 0.002f);
        
    }
    public void End()
    {
        if (slider == null || text_progress == null)
        {
            return;
        }
        slider = null;
        text_progress=null;
    }
}

