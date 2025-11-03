using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    private static GameObject currentTarget; // Tham chiếu tĩnh, không dùng biến public target
    public float smoothSpeed = 0.125f; // Tốc độ mượt
    public Vector3 offset; // Khoảng cách camera so với nhân vật

    void LateUpdate()
    {
        if (currentTarget == null)
        {
            Debug.LogWarning("FollowObject: Current target is null! Camera: " + gameObject.name);
            return;
        }
        // Vị trí mong muốn
        Vector3 desiredPosition = currentTarget.transform.position + offset;
        // Di chuyển mượt
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Cập nhật camera
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }

    // Phương thức để gán target tĩnh
    public static void SetTarget(GameObject newTarget)
    {
        currentTarget = newTarget;
        Debug.Log("FollowObject: Target updated to " + (currentTarget != null ? currentTarget.name : "null"));
    }
}