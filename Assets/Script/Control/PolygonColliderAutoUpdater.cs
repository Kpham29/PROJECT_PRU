using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonColliderAutoUpdater : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;
    private Sprite lastSprite;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    void LateUpdate()
    {
        // Chỉ cập nhật khi sprite thay đổi (đỡ tốn hiệu năng)
        if (spriteRenderer.sprite != lastSprite)
        {
            UpdateCollider();
            lastSprite = spriteRenderer.sprite;
        }
    }

    private void UpdateCollider()
    {
        if (spriteRenderer.sprite == null)
            return;

        // Lấy số lượng shape vật lý từ sprite
        int shapeCount = spriteRenderer.sprite.GetPhysicsShapeCount();
        polygonCollider.pathCount = shapeCount;

        // Tạo danh sách lưu path
        List<Vector2> path = new List<Vector2>();

        // Copy dữ liệu hình dạng vật lý từ sprite sang collider
        for (int i = 0; i < shapeCount; i++)
        {
            path.Clear();
            spriteRenderer.sprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path);
        }
    }
}
