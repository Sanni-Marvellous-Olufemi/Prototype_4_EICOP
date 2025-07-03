using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text feedbackText;
    public GameObject goal;

    int score = 0;
    int streak = 0;
    int misses = 0;

    public void OnScore(bool success)
    {
        if (success)
        {
            score++;
            streak++;
            misses = 0;

            if (streak >= 3)
            {
                feedbackText.text = "Hot Streak! +1 Bonus!";
                score++;
            }
            else
            {
                feedbackText.text = "Goal!";
            }
        }
        else
        {
            streak = 0;
            misses++;
            feedbackText.text = "Miss!";

            if (misses >= 2)
            {
                feedbackText.text = "Under Pressure!";
                goal.transform.localScale = Vector3.one * 0.5f;
            }
            else
            {
                goal.transform.localScale = Vector3.one;
            }
        }

        scoreText.text = "Score: " + score;
    }
}