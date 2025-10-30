using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// using Color = UnityEngine.Color;


public class CharacterSelect : MonoBehaviour
{
    private int index;
    private int selectedIndex;
    [SerializeField] Transform container;
    [SerializeField] GameObject[] charactor;
    private float moveDuration = 0.05f;
    [SerializeField] private AnimationCurve ease;
    [SerializeField] private bool useUnscaledTime = true;
    private Coroutine moveCo;
    private Vector3 containerStartPos;
    [SerializeField] TextMeshProUGUI characterName;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        selectedIndex = 0;
        SelectCharactor();
        containerStartPos = container.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPrevButtonClick()
    {
        if (index > 0)
        {
            index--;
            SelectCharactor();
            selectedIndex = index;
        }
    }

    public void OnNextButtonClick()
    {
        if (index < charactor.Length - 1)
        {
            index++;
            SelectCharactor();
            selectedIndex = index;
        }
    }

    public void SelectCharactor()
    {
        for (int i = 0; i < charactor.Length; i++)
        {
            if (i == index)
            {
                charactor[i].GetComponent<SpriteRenderer>().color = Color.white;
                charactor[i].GetComponent<Animator>().enabled = true;
                characterName.text =  charactor[i].name;

                if (i == 0 && selectedIndex == 0)
                {
                    continue;
                }
                else
                {
                    Vector3 targetPos = containerStartPos - new Vector3(i * 6f, 0f, 0f);
                    StartMove(targetPos);
                }
            }
            else
            {
                charactor[i].GetComponent<SpriteRenderer>().color = Color.black;
                charactor[i].GetComponent<Animator>().enabled = false;
            }
        }
    }

    private void StartMove(Vector3 worldTarget)
    {
        if (moveCo != null) StopCoroutine(moveCo);
        moveCo = StartCoroutine(MoveTo(container, worldTarget, moveDuration, ease, useUnscaledTime));
    }

    private static IEnumerator MoveTo(Transform tf, Vector3 target, float duration, AnimationCurve ease, bool useUnscaled)
    {
        if (tf == null) yield break;
        if (ease == null) ease = AnimationCurve.EaseInOut(0, 0, 1, 1);
        Vector3 start = tf.position;
        float t = 0f;
        while (t < 1f)
        {
            t += (useUnscaled ? Time.unscaledDeltaTime : Time.deltaTime) / duration;
            float k = ease.Evaluate(Mathf.Clamp01(t));
            tf.position = Vector3.LerpUnclamped(start, target, k);
            yield return null;
        }
        tf.position = target;
    }
    
    public void PlayGame()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene("TestScene");
    }
}
