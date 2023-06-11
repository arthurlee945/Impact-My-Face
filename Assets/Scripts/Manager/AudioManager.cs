using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header("Combat Audios")]
    [SerializeField] private AudioSource jab;
    [SerializeField] private AudioSource hook;
    [SerializeField] private AudioSource strongPunch;
    [SerializeField] private AudioSource boom;
    [Header("BGM")]
    [SerializeField] private AudioSource music;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    public void PunchSound(string type)
    {
        switch (type)
        {
            case "jab":
                jab.Play();
                break;
            case "hook":
                hook.Play();
                break;
            case "strongPunch":
                strongPunch.Play();
                break;
            case "boom":
                boom.Play();
                break;
            default:
                break;
        }
    }
}
