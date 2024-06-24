using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingInteractable : BaseInteractable
{
    [Header("Movement direction")]
    [SerializeField] private locations location;

    [Header("This direction")]
    [SerializeField] private Vector3 thisSpawnPoisition;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        thisSpawnPoisition = this.transform.position;
    }

    private void Start()
    {
        if(DataPersistenceManager.instance.gameData.currentScene == location.ToString() && !DataPersistenceManager.instance.gameData.movingFromMenu)
        {
            Debug.Log("Found location");
            player.transform.position = thisSpawnPoisition;
        }
    }

    public override void Interact()
    {
        Debug.Log("Cliked on moving plattform " + location.ToString());

        // save the game anytime before loading a new scene
        DataPersistenceManager.instance.SaveGame();

        SceneManager.LoadScene(location.ToString());
    }
}