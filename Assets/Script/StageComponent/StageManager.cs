using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject currentMap;
    [SerializeField] private GameObject nextMap;
    [SerializeField] private Transform startPoint;

    private void Start()
    {
        currentMap.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            currentMap.SetActive(false);
            nextMap.SetActive(true);

            if(startPoint != null)
            {
                collider.transform.position = startPoint.position;
            }
        }
        
    }
}
