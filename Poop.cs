using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Poop : MonoBehaviour
{
    public AudioManager AudioManager;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Moonshine"))
        {
            Destroy(gameObject);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.PlayMoonshineBreakingSound();
            }
            else
            {
                Debug.LogError("AudioManager not found in the scene!");
            }
        }
    }
}
