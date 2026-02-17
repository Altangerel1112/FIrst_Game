using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 4;              // 3b: 4 baskets
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;

    [Header("UI")]
    public GameObject startScreen;          // 3a
    public GameObject restartButton;        // 4
    public TMP_Text roundText;              // 4

    [Header("Dynamic")]
    public List<GameObject> basketList;

    private bool gameStarted = false;
    private bool gameOver = false;

    void Start()
    {
        basketList = new List<GameObject>();

        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }

        gameStarted = false;
        gameOver = false;

        if (startScreen != null) startScreen.SetActive(true);
        if (restartButton != null) restartButton.SetActive(false);

        UpdateRoundUI();
        Time.timeScale = 0f; // pause until Start
    }

    // 3a: Start button OnClick -> ApplePicker.StartGame()
    public void StartGame()
    {
        if (gameOver) return;

        gameStarted = true;
        if (startScreen != null) startScreen.SetActive(false);

        Time.timeScale = 1f;
        UpdateRoundUI();
    }

    // 4: Restart button OnClick -> ApplePicker.RestartGame()
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AppleMissed()
    {
        if (!gameStarted || gameOver) return;

        // Destroy all falling apples
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject go in appleArray) Destroy(go);

        // (Optional safety) also clear branches
        GameObject[] branchArray = GameObject.FindGameObjectsWithTag("Branch");
        foreach (GameObject go in branchArray) Destroy(go);

        // Remove one basket
        int basketIndex = basketList.Count - 1;
        GameObject basketGO = basketList[basketIndex];
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        UpdateRoundUI();

        // No baskets left => Game Over
        if (basketList.Count == 0)
        {
            GameOver();
        }
    }

    // 5: called when Branch is caught
    public void BranchCaught()
    {
        if (!gameStarted || gameOver) return;
        GameOver();
    }

    private void GameOver()
    {
        gameOver = true;

        // Clear falling objects
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject go in appleArray) Destroy(go);

        GameObject[] branchArray = GameObject.FindGameObjectsWithTag("Branch");
        foreach (GameObject go in branchArray) Destroy(go);

        if (roundText != null) roundText.text = "Game Over";
        if (restartButton != null) restartButton.SetActive(true);

        Time.timeScale = 0f;
    }

    private void UpdateRoundUI()
    {
        if (roundText == null) return;

        if (gameOver)
        {
            roundText.text = "Game Over";
            return;
        }

        // Round 1..4 based on baskets remaining
        int round = (numBaskets - basketList.Count) + 1;
        round = Mathf.Clamp(round, 1, 4);

        roundText.text = "Round " + round;
    }
}
