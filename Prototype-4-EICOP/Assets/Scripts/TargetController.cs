using UnityEngine;

public class TargetController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * move * moveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, -7f, 7f);
        transform.position = new Vector2(clampedX, transform.position.y);
    }
}