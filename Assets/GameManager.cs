using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerControl player1;
    private Rigidbody2D player1rigidbody;

    public PlayerControl player2;
    private Rigidbody2D player2rigidbody;

    public BallControl ball;
    private Rigidbody2D ballrigidbody;
    private CircleCollider2D ballcollider;

    public int maxScore;

    private bool isDebugWindowShown = false;

    public Trajectory trajectory;

    private void Start()
    {
        player1rigidbody = player1.GetComponent<Rigidbody2D>();
        player2rigidbody = player2.GetComponent<Rigidbody2D>();
        ballrigidbody = ball.GetComponent<Rigidbody2D>();
        ballcollider = ball.GetComponent<CircleCollider2D>();
    }
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            player1.ResetScore();
            player2.ResetScore();
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }
        if (player1.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (player2.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        if (isDebugWindowShown)
        {
            Color oldcolor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            float ballmass = ballrigidbody.mass;
            Vector2 ballvelocity = ballrigidbody.velocity;
            float ballspeed = ballrigidbody.velocity.magnitude;
            Vector2 ballmomentum = ballmass * ballvelocity;
            float ballfriction = ballcollider.friction;
            float impulseplayer1X = player1.Lastcontactpoint.normalImpulse;
            float impulseplayer1Y = player1.Lastcontactpoint.tangentImpulse;
            float impulseplayer2X = player2.Lastcontactpoint.normalImpulse;
            float impulseplayer2Y = player2.Lastcontactpoint.tangentImpulse;
            string debugtext = "Ball mass = " + ballmass + "\n" +
                "Ball velocity = " + ballvelocity + "\n" +
                "Ball speed = " + ballspeed + "\n" +
                "Ball momentum = " + ballmomentum + "\n" +
                "Ball friction = " + ballfriction + "\n" +
                "Last impulse from player 1 = (" + impulseplayer1X + ", " + impulseplayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulseplayer2X + ", " + impulseplayer2Y + ")\n";

            GUIStyle guistyle = new GUIStyle(GUI.skin.textArea);
            guistyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugtext, guistyle);
            GUI.backgroundColor = oldcolor;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }
    }
    

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //Update is called once per frame
    void Update()
    {
        
    }
}
