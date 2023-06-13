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
    public void PunchSound(string type, bool isGuarded = false)
    {
        switch (type)
        {
            case "jab":
                jab.volume = isGuarded ? 0.225f : 0.35f;
                jab.pitch = isGuarded ? 0.8f :1 ;
                jab.Play();
                break;
            case "hook":
                hook.volume = isGuarded ? 0.225f : 0.35f;
                hook.pitch = isGuarded ? 0.8f : 1;
                hook.Play();
                break;
            case "strongPunch":
                strongPunch.volume = isGuarded ? 0.225f : 0.35f;
                strongPunch.pitch = isGuarded ? 0.8f : 1;
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
