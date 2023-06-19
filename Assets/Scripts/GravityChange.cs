using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChange : MonoBehaviour
{
    private Rigidbody targetRigidbody;
    private SpawnManagerX spawnManager;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] Vector3 destination;
    public AudioClip playerAnimationSound; // Sonido al desaparecer el jugador

    private void Start()
    {
        targetRigidbody = GameObject.FindGameObjectWithTag("PlayerRb").GetComponent<Rigidbody>();
        spawnManager = FindObjectOfType<SpawnManagerX>();
    }

    private void ActivateGravity()
    {
        targetRigidbody.useGravity = true;
    }

    private void Update()
    {
        if (targetRigidbody.transform.position == targetPosition)
        {
            MoveToDestination();
        }

        // Verificar si no hay GameObjects con la etiqueta "Player" en la escena
        if (spawnManager.playerCount == 0)
        {
            AudioSource.PlayClipAtPoint(playerAnimationSound, Vector3.zero);
            ActivateGravity();
            DestroyAllHoles();
        }
    }

    void DestroyAllHoles()
    {
        GameObject[] holes = GameObject.FindGameObjectsWithTag("Hole");

        foreach (GameObject hole in holes)
        {
            Destroy(hole);
        }
    }

        private void MoveToDestination()
    {
        targetRigidbody.useGravity = false;
        targetRigidbody.transform.position = destination;
    }
}