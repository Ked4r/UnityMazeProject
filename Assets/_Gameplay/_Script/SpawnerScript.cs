using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab = null;
    [SerializeField] private Transform spawnPoint = null;
   
    public InputManager inputManager;

   

    private void Update()
    {
        inputManager.inputAsset.Player.Spawn.performed += _ => Spawn();
        inputManager.inputAsset.Player.DeSpawn.performed += _ => Despawn();
      
    
}

    public void Spawn()
    {
        if (ballPrefab != null && spawnPoint != null)
        {
            Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation,spawnPoint);
        }
    }
    private void Despawn()
    {
        Debug.Log("Despawning...");
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in spawnPoint)
        {
            Debug.Log("Found child: " + child.name);
            children.Add(child.gameObject);
        }

        foreach (GameObject child in children)
        {
            Debug.Log("Destroying child: " + child.name);
            Destroy(child);
        }
    }


}
