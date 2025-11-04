using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeLine : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private TextMeshProUGUI skipText;
    [SerializeField] private Image progressImage;
    private float holdTime = 3f;
    private float currentHoldTime = 0f;
    private bool isHolding = false;
    private float maxFillAmount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Reset()
    {
        if (!director) director = GetComponent<PlayableDirector>();
        if (!skipText) skipText = GetComponentInChildren<TextMeshProUGUI>();
        if (!progressImage) progressImage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isHolding)
            {
                isHolding = true;
                skipText.text = "Holding Space...";
            }

            currentHoldTime += Time.deltaTime;

            progressImage.fillAmount = currentHoldTime / holdTime;

            if (currentHoldTime > holdTime)
            {
                SkipToEnd();
            }
        }
        else
        {
            if (isHolding)
            {
                isHolding = false;
                skipText.text = "Press Space to Skip";
            }
            currentHoldTime = 0f;
            progressImage.fillAmount = 0f;
        }
    }

    public void SkipToEnd()
    {
        if (!director) return;
        director.time = director.duration;
        director.Evaluate();
        SceneManager.LoadScene("Main Menu");
    }
}