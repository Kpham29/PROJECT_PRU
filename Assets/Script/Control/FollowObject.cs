using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;    // Nhân vật để follow
    public float smoothSpeed = 0.125f; // tốc độ mượt
    public Vector3 offset;      // khoảng cách camera so với nhân vật

    void LateUpdate()
    {
        if (target == null) return;

        // Vị trí mong muốn
        Vector3 desiredPosition = target.position + offset;

        // Di chuyển mượt
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Cập nhật camera
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
