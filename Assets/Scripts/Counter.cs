using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public static int globalCount = 0;
    public AudioClip collisionSound; // Sonido de colisión del jugador con el agujero


    private void Start()
    { 
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.StartCountdown();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                Transform playerTransform = other.transform;
                Transform otherTransform = transform;

                float playerSizeX = playerTransform.localScale.x;
                float otherSizeX = otherTransform.localScale.x;

                if (otherSizeX > playerSizeX)
                {
                    globalCount++;
                    Debug.Log("Colisión detectada con el jugador. Objeto de jugador destruido.");
                    Debug.Log("Contador global: " + globalCount);
                    UIManager uiManager = FindObjectOfType<UIManager>();
                    uiManager?.UpdateCounterText(globalCount);


                    // Reproducir el sonido de colisión
                    if (collisionSound != null)
                    {
                        AudioSource.PlayClipAtPoint(collisionSound, otherTransform.position);
                    }

                    Destroy(other.gameObject);

                   
                }
            }
        }
    }

  
  
    public static void RestartGlobalCount()
    {
        globalCount = 0;
    }
}