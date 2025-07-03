using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text feedbackText;
    public Text endGameText;
    public GameObject goal;

    public GameObject startButton;
    public GameObject restartButton;

    private bool gameEnded = false;
    private bool gameStarted = false;

    private bool underPressure = false;
    public int recoverStreakThreshold = 3;

    private int score = 0;
    private int streak = 0;
    private int misses = 0;

    void Start()
    {
        Time.timeScale = 0f; // Pause game at start

        if (feedbackText != null) feedbackText.text = "";
        if (scoreText != null) scoreText.text = "Score: 0";
        if (endGameText != null) endGameText.enabled = false;

        // Reset buttons visibility
        if (startButton != null) startButton.SetActive(true);
        if (restartButton != null) restartButton.SetActive(false);
    }

    public void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f;

        if (startButton != null) startButton.SetActive(false);
        if (restartButton != null) restartButton.SetActive(false);
        if (feedbackText != null) feedbackText.text = "Game On!";
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnScore(bool success)
    {
        if (gameEnded || !gameStarted) return;

        if (success)
        {
            score++;
            streak++;
            misses = 0;

            if (streak >= 3)
            {
                feedbackText.text = "🔥 Hot Streak! +1 Bonus!";
                score++; // bonus point
            }
            else
            {
                feedbackText.text = "✅ Goal!";
            }

            if (underPressure && streak >= recoverStreakThreshold)
            {
                StartCoroutine(GrowGoalBack());
                underPressure = false;
            }
        }
        else
        {
            streak = 0;
            misses++;
            feedbackText.text = "❌ Miss!";

            if (misses >= 2)
            {
                feedbackText.text = "😰 Under Pressure!";
                goal.transform.localScale = Vector3.one * 0.5f;
                underPressure = true;
            }
            else
            {
                goal.transform.localScale = Vector3.one;
            }
        }

        scoreText.text = "Score: " + score;

        // Win or lose conditions
        if (score >= 100 && !gameEnded)
        {
            EndGame(true);
        }

        if (misses >= 10 && !gameEnded)
        {
            EndGame(false);
        }
    }

    private void EndGame(bool didWin)
    {
        gameEnded = true;
        Time.timeScale = 0f;

        if (endGameText != null)
        {
            endGameText.enabled = true;
            endGameText.text = didWin ? "🏆 You Win!" : "💀 Game Over!";
        }

        if (feedbackText != null)
            feedbackText.text = didWin ? "Well played!" : "Try again!";

        if (restartButton != null)
            restartButton.SetActive(true);

        if (startButton != null)
            startButton.SetActive(false); // ensure it never shows again
    }

    private IEnumerator GrowGoalBack()
    {
        Vector3 targetScale = Vector3.one;
        Vector3 currentScale = goal.transform.localScale;
        float duration = 1f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            goal.transform.localScale = Vector3.Lerp(currentScale, targetScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        goal.transform.localScale = targetScale;

        if (feedbackText != null)
            feedbackText.text = "💪 Recovered!";
    }
}
