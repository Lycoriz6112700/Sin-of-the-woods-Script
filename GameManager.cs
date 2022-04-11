using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public static int Score = 13;
    public TMP_Text scoreText;
    public GameObject ScoreUI;
    public GameObject EndGameUI;

    [Space]
    public Transform spawnPoint;

    private void Awake()
    {
        SetScoreText();
    }

    private void Start()
    {
        PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity, 0);
    }

    void Update()
    {
        SetScoreText();
        EndGame();
    }

    void SetScoreText()
    {
        scoreText.text = Score.ToString();
    }

    void EndGame()
    {
        if(Score == 0)
        {
            ScoreUI.SetActive(false);
            EndGameUI.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Return))
            {
                PhotonNetwork.Disconnect();
                PhotonNetwork.LoadLevel("Menu");
            }
            
        }
    }
    
}
