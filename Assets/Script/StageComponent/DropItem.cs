using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(FakeDrop(gameObject));
    }

    private IEnumerator FakeDrop(GameObject go)
    {
        Vector3 startPos = go.transform.position;
        Vector3 targetPos = startPos + Vector3.up * 1f;
        float upTime = 0.3f;
        float downTime = 0.3f;

        float t = 0;
        while (t < upTime)
        {
            t += Time.deltaTime;
            float lerp = t / upTime;
            go.transform.position = Vector3.Lerp(startPos, targetPos, lerp);
            yield return null;
        }

        t = 0;
        Vector3 endPos = startPos;
        while (t < downTime)
        {
            t += Time.deltaTime;
            float lerp = t / downTime;
            float fallY = Mathf.Sin((1 - lerp) * Mathf.PI / 2);
            go.transform.position = Vector3.Lerp(targetPos, endPos, lerp) + Vector3.up * fallY * 0.1f;
            yield return null;
        }
    }
}