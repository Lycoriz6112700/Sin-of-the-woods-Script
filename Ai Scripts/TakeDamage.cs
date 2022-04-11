using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : Photon.MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Enemy"){
            Application.Quit();
            gameObject.tag = "Dead";
            Destroy(gameObject, 0.1f);
        }
    }

}
