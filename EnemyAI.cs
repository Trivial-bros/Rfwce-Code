using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform Target; //What the enemy will follow
    public float activeDistance = 50f; //How close the player has to be for the enemy to start pathfinding
    public float pathUpdateSeconds = 0.5f; //How often the path updates (The lower it is the more accurate but more taxing on the computer)

    [Header("Physics")]
    public float speed = 200f; //Speed, duh
    public float nextWaypointDistance = 3f; //Distance to the next waypoint
    public float JumpNodeHeightRequirement = 0.8f;//How high of a ledge the enemy can jump up
    public float JumpStrength = 0.3f; //How high the enemy jumps
    public float JumpCheckOffset = 0.1f; //Radius of the ground check

    [Header("Custom Behaiviour")]
    public bool followenabled = true; //Will follow or not, can be turned off to disable this script
    public bool Jumpenabled = true; //Prevents the enemy from jumping if it's off
    public bool directionLookEnabled = true; //Can change direction, useful for turrets

    private Path path;
    private int currentWaypoint;
    bool isGrounded = false;
    Seeker Seeker;
    Rigidbody2D rb;
    public bool hasStopped;

    public void Start()
    {
        Seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    void FixedUpdate()
    {
        float Distance = Vector2.Distance(transform.position, Target.position);

        if (Distance < 7f)
            hasStopped = true;
        else
            hasStopped = false;


        if(TargetInDistance() && followenabled && Distance > 7f)
        {
            Pathfollow();
        }
    }

    private void UpdatePath()
    {
       if(followenabled && TargetInDistance() && Seeker.IsDone())
        {
            Seeker.StartPath(rb.position, Target.position, OnPathComplete);
        }
    }

    private void Pathfollow()
    {
        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + JumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (Jumpenabled && isGrounded)
        {
            if (direction.y > JumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * JumpStrength);
            }
        }
        // Movement
        rb.AddForce(force);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
    
    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, Target.transform.position) < activeDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}