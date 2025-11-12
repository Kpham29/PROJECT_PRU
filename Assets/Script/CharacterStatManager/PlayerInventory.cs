using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;
    [SerializeField] private GameObject keyUIcon;

    public void PickedUpKey()
    {
        hasKey = true;
        Debug.Log("Đã nhặt chìa khóa!");
        if (keyUIcon != null)
        {
            keyUIcon.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (keyUIcon == null)
        {
            keyUIcon = GameObject.Find("KeyIcon");
        }

        if (keyUIcon != null)
        {
            keyUIcon.SetActive(false);
            Debug.Log("Tìm thấy KeyIcon");
        }
        else
        {
            Debug.LogWarning("⚠️ Không tìm thấy KeyIcon trong Scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UsedKey()
    {
        hasKey = false;
        if (keyUIcon != null)
        {
            keyUIcon.SetActive(false);
        }
    }
}
