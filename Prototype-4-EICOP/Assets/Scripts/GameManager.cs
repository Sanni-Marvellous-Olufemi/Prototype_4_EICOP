using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text feedbackText;
    public GameObject goal;

    private bool underPressure = false;
    public int recoverStreakThreshold = 3;

    private int score = 0;
    private int streak = 0;
    private int misses = 0;

    public void OnScore(bool success)
    {
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

            // Check for recovery from pressure
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
    }

    private IEnumerator GrowGoalBack()
    {
        Vector3 targetScale = Vector3.one; // normal size
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
        feedbackText.text = "💪 Recovered!";
    }
}
