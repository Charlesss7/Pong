using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float xInitialForce;
    public float yInitialForce;
    private Vector2 trajectoryorigin;
    void ResetBall()
    {
        transform.position = Vector2.zero;
        rigidbody2D.velocity = Vector2.zero;
        
    }
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryorigin; }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryorigin = transform.position;
    }
    void PushBall()
    {
        //float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        float randomDirection = Random.Range(0, 2);
        if (randomDirection < 1.0f)
        {
            rigidbody2D.AddForce(new Vector2(-xInitialForce, yInitialForce));
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(xInitialForce, yInitialForce));
        }
    }
    void RestartGame()
    {
        ResetBall();
        Invoke("PushBall", 2);
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        trajectoryorigin = transform.position;
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
