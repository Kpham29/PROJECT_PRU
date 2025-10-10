using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    [SerializeField] private PatrollPhysics patrollPhysics;

    //Vào vùng phát hiện - attackable
    private void OnTriggerStay2D(Collider2D collision)
    {
        patrollPhysics.inAttackRange = true;
    }

    //Thoát khỏi vùng phát hiện
    private void OnTriggerExit2D(Collider2D collision)
    {
        patrollPhysics.inAttackRange = false;
    }
}
