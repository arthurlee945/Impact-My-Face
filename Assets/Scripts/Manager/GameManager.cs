using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private static readonly object _lock = new object();
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
            }
        }
        return instance;
    }
    private void Awake()
    {

    }
}
