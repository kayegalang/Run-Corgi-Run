using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopPlacer : MonoBehaviour
{
    public GameObject PoopPrefab;
    
    public void Place(Vector3 corgiPosition)
    {
        GameObject Poop = Instantiate(PoopPrefab, corgiPosition, Quaternion.identity);
        
        Rigidbody2D PoopRB = Poop.GetComponent<Rigidbody2D>();
        PoopRB.velocity = Vector2.up * GameParameters.PoopForce;
    }
}
