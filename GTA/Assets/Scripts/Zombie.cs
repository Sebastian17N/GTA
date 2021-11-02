using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Zombie : MonoBehaviour
{
    Rigidbody2D Rigidbody;
    Vector2 TargetPosition;
    Player TargetPlayer;

    [SerializeField]
    float speed = 1f;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeTargetPositionCoroutine());
    }
    IEnumerator ChangeTargetPositionCoroutine()
    {
        while (true)
        {
            TargetPosition = (Vector2)transform.position + Random.insideUnitCircle * 10f;
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }
    void Update()
    {
        var targetSpeed = speed;
        if(TargetPlayer != null)
        {
            TargetPosition = TargetPlayer.transform.position;
            targetSpeed *= 4f;
        }
        var direction = (Vector3)TargetPosition - transform.position;
        var targetVelocity = direction.normalized * targetSpeed;

        Rigidbody.velocity = Vector3.Lerp(
            Rigidbody.velocity,
            targetVelocity,
            Time.deltaTime / 2f);

        transform.right = direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
            TargetPlayer = player;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
            TargetPlayer = null;
    }
}
