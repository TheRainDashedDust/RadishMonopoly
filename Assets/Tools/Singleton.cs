using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T:class,new()
{
    private static T instance;
    private static readonly Object obj = new Object();

    protected Singleton() { }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }
            return instance;
        }
        
    }

}
