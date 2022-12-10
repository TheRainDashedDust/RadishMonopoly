using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoad : MonoSingleton<AsyncLoad>
{
    public int sceneIndex = 1;
    AsyncOperation async;
    public Scrollbar scrollbar;
    float progress;
    // Start is called before the first frame update
    void Start()
    {
        async = SceneManager.LoadSceneAsync(sceneIndex);
        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (async.progress>=0.9f)
        {
            progress = 1;
        }
        else progress=async.progress;

        scrollbar.size=Mathf.MoveTowards(scrollbar.size, progress,0.002f);
        if (scrollbar.size >= 0.99f)
        {
            async.allowSceneActivation = true;
            if (gameObject!=null)
            {
                Destroy(gameObject);
            }
        }
    }
}
