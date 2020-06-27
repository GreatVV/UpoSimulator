using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithNPC : MonoBehaviour
{
    public GameObject player;
    private Collider playerCollider;
    void Start()
    {
        playerCollider = player.GetComponent<Collider>();
    }


    void Update()
    {
        
        //foreach (Collider player in)
    }
}
