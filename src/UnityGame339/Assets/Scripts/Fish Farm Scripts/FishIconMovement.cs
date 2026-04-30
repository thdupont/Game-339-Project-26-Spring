using UnityEngine;

public class FishIconMovement : MonoBehaviour
{
    public float driftSpeed = 0.3f;
    public float driftRadius = 0.4f; // how far it wanders from its start position
    
    public float bobSpeed = 1.2f;
    public float bobAmount = 0.05f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private SpriteRenderer sr;
    private float driftTimer;
    private float driftDuration;

    void Start()
    {
        startPosition = transform.localPosition;
        sr = GetComponent<SpriteRenderer>();
        PickNewTarget();

        // Randomize phase so multiple fish don't bob in sync
        driftTimer = Random.Range(0f, 10f);
    }

    void Update()
    {
        // Drift toward target
        driftTimer += Time.deltaTime;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * driftSpeed);

        // Bob up and down
        Vector3 pos = transform.localPosition;
        pos.y += Mathf.Sin(Time.time * bobSpeed + driftTimer) * bobAmount * Time.deltaTime;
        transform.localPosition = pos;

        // Flip sprite based on movement direction
        float moveDir = targetPosition.x - transform.localPosition.x;
        if (Mathf.Abs(moveDir) > 0.01f)
            sr.flipX = moveDir < 0;

        // Pick a new target when close enough or timed out
        if (driftTimer >= driftDuration || Vector3.Distance(transform.localPosition, targetPosition) < 0.05f)
            PickNewTarget();
    }

    private void PickNewTarget()
    {
        // Wander randomly within driftRadius of the fish's slot position
        Vector2 randomOffset = Random.insideUnitCircle * driftRadius;
        targetPosition = startPosition + new Vector3(randomOffset.x, randomOffset.y, 0f);

        driftDuration = Random.Range(2f, 5f);
        driftTimer = 0f;
    }
}