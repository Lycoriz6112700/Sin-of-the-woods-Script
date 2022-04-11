using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{

    public InputField nameInputField;

    void Start()
    {
        if(!PlayerPrefs.HasKey("PlayerName"))
        {
            return;
        }
        else
        {
            string PlayerName = PlayerPrefs.GetString("PlayerName");
            nameInputField.text = PlayerName;
        }
    }

    public void PlacePlayerName()
    {
        string PlayerNickname = nameInputField.text;
        PhotonNetwork.playerName = PlayerNickname;
        PlayerPrefs.SetString("PlayerName", PlayerNickname);
    }
}
