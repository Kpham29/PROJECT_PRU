using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
// using Color = UnityEngine.Color;


public class CharacterSelect : MonoBehaviour
{
    private int index;
    private int selectedIndex;
    [SerializeField] Transform container;
    [SerializeField] GameObject[] characterPrefabs;
    private GameObject[] charactor;
    private float moveDuration = 0.05f;
    [SerializeField] private AnimationCurve ease;
    [SerializeField] private bool useUnscaledTime = true;
    private Coroutine moveCo;
    private Vector3 containerStartPos;
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] Transform parentObject;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        selectedIndex = 0;
        charactor = new GameObject[characterPrefabs.Length];
        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            GameObject clone = Instantiate(characterPrefabs[i], parentObject);
            Rigidbody2D rb2D = clone.GetComponent<Rigidbody2D>();
            if (rb2D != null) rb2D.isKinematic = true;
            clone.transform.localPosition = new Vector3(i * 6, 0, 0);
            charactor[i] = clone;
            
        }
        container.localPosition = Vector3.zero;

        containerStartPos = container.localPosition;
        SelectCharactor();
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
                characterName.text =  charactor[i].name.Replace("(Clone)", "");;

                if (i == 0 && selectedIndex == 0)
                {
                    continue;
                }
                else
                {
                    Vector3 targetPos = containerStartPos - new Vector3(i * 6f, 0f, 0f);
                    StartMoveLocal(targetPos);
                    
                }
            }
            else
            {
                charactor[i].GetComponent<SpriteRenderer>().color = Color.black;
                charactor[i].GetComponent<Animator>().enabled = false;
            }
        }
    }

    private void StartMoveLocal(Vector3 localTarget)
    {
        if (moveCo != null) StopCoroutine(moveCo);
        moveCo = StartCoroutine(MoveToLocal(container, localTarget, moveDuration, ease, useUnscaledTime));
    }

    private static IEnumerator MoveToLocal(Transform tf, Vector3 targetLocal, float duration, AnimationCurve ease, bool useUnscaled)
    {
        if (tf == null) yield break;
        if (ease == null) ease = AnimationCurve.EaseInOut(0, 0, 1, 1);
        Vector3 start = tf.localPosition;
        float t = 0f;
        while (t < 1f)
        {
            t += (useUnscaled ? Time.unscaledDeltaTime : Time.deltaTime) / duration;
            float k = ease.Evaluate(Mathf.Clamp01(t));
            tf.localPosition = Vector3.LerpUnclamped(start, targetLocal, k);
            yield return null;
        }
        tf.localPosition = targetLocal;
    }

    
    public void PlayGame()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlayScene");
    }
}
