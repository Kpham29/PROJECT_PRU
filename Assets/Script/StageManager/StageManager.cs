using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject currentMap;
    [SerializeField] private GameObject nextMap;

    private void Start()
    {
        currentMap.SetActive(true);
    }

    public void OnTriggerEnter2D()
    {
        currentMap.SetActive(false);
        nextMap.SetActive(true);
    }
}
