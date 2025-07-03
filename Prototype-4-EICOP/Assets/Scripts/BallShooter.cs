using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public Transform target;
    public float shotSpeed = 10f;
    private bool isShot = false;

    void Update()
    {
        if (!isShot && Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 dir = (target.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().linearVelocity = dir * shotSpeed;
            isShot = true;
        }
    }

    public void ResetBall()
    {
        transform.position = new Vector2(0, -4);
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        isShot = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GoalArea"))
            FindObjectOfType<GameManager>().OnScore(true);
        else
            FindObjectOfType<GameManager>().OnScore(false);

        ResetBall();
    }
}