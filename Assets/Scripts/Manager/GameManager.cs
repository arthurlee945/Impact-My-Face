using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //private static readonly object _lock = new object();
    [Header("Player Objects")]
    [SerializeField]
    private Dictionary<string, GameObject> players;
    //public static GameManager GetInstance()
    //{
    //    if (instance == null)
    //    {
    //        lock (_lock)
    //        {
    //            instance ??= new GameManager();
    //        }
    //    }
    //    return instance;
    //}

    private void Awake()
    {
        if(Instance !=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);
    }
}
