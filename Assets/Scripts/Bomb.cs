using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
    if(other.gameObject.CompareTag("Player")){
        GameManager.Instance.Lose();
    }
    else if(other.gameObject.CompareTag("Fall Area")){
        gameObject.SetActive(false);
    }
   }
}
