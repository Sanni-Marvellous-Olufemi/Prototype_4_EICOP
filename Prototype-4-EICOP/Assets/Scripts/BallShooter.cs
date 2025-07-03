using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public Transform target;
    public float shotSpeed = 10f;
    private bool isShot = false;
    private Vector2 lastPosition;


    void Update()
    {
        if (!isShot && Input.GetKeyDown(KeyCode.Space))
        {
            lastPosition = transform.position; // 🔹 Store position before shooting

            Vector2 dir = (target.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().linearVelocity = dir * shotSpeed;
            isShot = true;
        }
    }


    public void ResetBall(bool wasSuccess)
    {
        if (wasSuccess)
        {
            // Randomize for goals
            float randomX = Random.Range(-6f, 6f);
            transform.position = new Vector2(randomX, -4);
        }
        else
        {
            // Return to the last shot position
            transform.position = lastPosition;
        }

        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        isShot = false;
    }




    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GoalArea"))
        {
            FindObjectOfType<GameManager>().OnScore(true);
            ResetBall(true);
        }
        else if (other.CompareTag("MissZone"))
        {
            FindObjectOfType<GameManager>().OnScore(false);
            ResetBall(false);
        }
    }


}