using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Pumpkin : MonoBehaviour
{
    public UnityEvent PumpkinHit = new UnityEvent() ;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PumpkinHit.Invoke();
        }
    }
   

}
