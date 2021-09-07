using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public BallControl ball;
    CircleCollider2D ballcollider;
    Rigidbody2D ballrigidbody;
    public GameObject ballatcollision;
    // Start is called before the first frame update
    void Start()
    {
        ballrigidbody = ball.GetComponent<Rigidbody2D>();
        ballcollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool drawballatcollision = false;
        Vector2 offsethitpoint = new Vector2();
        RaycastHit2D[] circlecasthit2Darray = Physics2D.CircleCastAll(ballrigidbody.position, ballcollider.radius, ballrigidbody.velocity.normalized);
        foreach (RaycastHit2D circlecasthit2D in circlecasthit2Darray)
        {
            if (circlecasthit2D.collider != null && circlecasthit2D.collider.GetComponent<BallControl>() == null)
            {
                Vector2 hitpoint = circlecasthit2D.point;
                Vector2 hitnormal = circlecasthit2D.normal;
                offsethitpoint = hitpoint + hitnormal * ballcollider.radius;
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsethitpoint);
                if (circlecasthit2D.collider.GetComponent<SideWall>() == null)
                {
                    Vector2 inVector = (offsethitpoint - ball.TrajectoryOrigin).normalized;
                    Vector2 outVector = Vector2.Reflect(inVector, hitnormal);
                    float outdot = Vector2.Dot(outVector, hitnormal);
                    if (outdot > -1.0f && outdot < 1.0)
                    {
                        DottedLine.DottedLine.Instance.DrawDottedLine(offsethitpoint, offsethitpoint + outVector * 10.0f);
                        drawballatcollision = true;
                    }
                }
                break;
            }
            if (drawballatcollision)
            {
                ballatcollision.transform.position = offsethitpoint;
                ballatcollision.SetActive(true);
            }
            else
            {
                ballatcollision.SetActive(false);
            }

        }

    }
}
