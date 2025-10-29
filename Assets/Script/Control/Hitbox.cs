using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private GameObject Weapon;

    public void EnableHitbox()
    {
        Weapon.SetActive(true);
    }

    public void DisableHitbox()
    {
        Weapon.SetActive(false);
    }
}
