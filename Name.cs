using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name : MonoBehaviour
{
    public Text nametext;
    PhotonView PV;

    void Awake() 
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if(!PV.isMine)
        {
            nametext.text = PV.owner.NickName;
        }
    }
}
