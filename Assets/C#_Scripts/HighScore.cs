using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    static private TMP_Text _UI_TEXT;
    static private int _SCORE = 1000;

    void Awake()
    {
        _UI_TEXT = GetComponent<TMP_Text>();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            SCORE = PlayerPrefs.GetInt("HighScore");
        }

        PlayerPrefs.SetInt("HighScore", SCORE);
        UpdateUI();
    }

    static public int SCORE
    {
        get { return _SCORE; }
        private set
        {
            _SCORE = value;
            PlayerPrefs.SetInt("HighScore", value);
            UpdateUI();
        }
    }

    static private void UpdateUI()
    {
        if (_UI_TEXT != null)
        {
            _UI_TEXT.text = "High Score: " + _SCORE.ToString("#,0");
        }
    }

    static public void TRY_SET_HIGH_SCORE(int scoreToTry)
    {
        if (scoreToTry <= SCORE) return;
        SCORE = scoreToTry;
    }

    [Tooltip("Check this box to reset the highscore in PlayerPrefs.")]
    public bool resetHighScoreNow = false;

    void OnDrawGizmos()
    {
        if (resetHighScoreNow)
        {
            resetHighScoreNow = false;
            PlayerPrefs.SetInt("HighScore", 1000);
            Debug.LogWarning("PlayerPrefs HighScore reset to 1,000");
        }
    }
}
