using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("Player Objects")]
    [SerializeField] private GameObject[] allCharacters;
    [SerializeField] private Transform Player1SpawnPoint;
    [SerializeField] private Transform Player2SpawnPoint;

    private Dictionary<string, GameObject> characters = new Dictionary<string,GameObject>();
    public GameObject SelectedPlayer1, SelectedPlayer2;
    public float Player1Health, Player2Health;
    public bool GameOver = false;
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
        foreach(GameObject c in allCharacters)
        {
            characters.Add(c.name, c);
        }
    }
    private void Start()
    {
        StartGame();
    }
    private void StartGame()
    {

        SelectedPlayer1 = characters["Arthur"];
        SelectedPlayer2 = characters["Rinnah"];
        Player1Health = 100;
        Player2Health = 100;
        GetSelectedCharacters();
    }
    void GetSelectedCharacters()
    {
        //-45 130
        Instantiate(SelectedPlayer1, Player1SpawnPoint.position, Quaternion.Euler(0, -45f, 0), Player1SpawnPoint);
        Instantiate(SelectedPlayer2, Player2SpawnPoint.position, Quaternion.Euler(0, 130f, 0), Player2SpawnPoint);
    }
    public void UpdateHealthPoint(string punchType, string charName)
    {
        if(charName == "Player1")
        {
            Player2Health -= punchType == "jab" ? 10 : punchType == "strongPunch" ? 15 : 20;
        }
        else
        {
            Player1Health -= punchType == "jab" ? 10 : punchType == "strongPunch" ? 15 : 20;
        }
        if(Player2Health <0 || Player1Health < 0)
        {
            GameOver = true;
        }
    }
}
