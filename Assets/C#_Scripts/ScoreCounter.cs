using UnityEngine;
using TMPro;   // IMPORTANT

public class ScoreCounter : MonoBehaviour
{
    [Header("Dynamic")]
    public int score = 0;

    private TMP_Text uiText;  // Use TMP_Text instead of Text

    void Start()
    {
        uiText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (uiText != null)
        {
            uiText.text = score.ToString("#,0");
        }
    }
}
