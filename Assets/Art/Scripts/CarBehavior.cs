using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour {

    public GameObject SmokePuffPrefab;

    enum Direction
    {
        Straight,
        Left,
        Right,
    }

    private const float turnSpeed = .2f;
    private const float turnAngle = 25f;

    private Direction direction;
    private float turnTargetAngle = 0;
    private readonly Transform[] wheels = new Transform[2];

	// Use this for initialization
	void Start () {
        direction = Direction.Straight;
        wheels[0] = transform.GetChild(0);
        wheels[1] = transform.GetChild(1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (direction)
        {
            case Direction.Straight:
                turnTargetAngle = Mathf.Lerp(turnTargetAngle, 0, turnSpeed);
                break;
            case Direction.Left:
                turnTargetAngle = Mathf.Lerp(turnTargetAngle, -turnAngle, turnSpeed);
                break;
            case Direction.Right:
                turnTargetAngle = Mathf.Lerp(turnTargetAngle, turnAngle, turnSpeed);
                break;
        }

        wheels[0].localEulerAngles = new Vector3(0, 0, turnTargetAngle);
        wheels[1].localEulerAngles = new Vector3(0, 0, turnTargetAngle);
    }

    public void TurnRight()
    {
        direction = Direction.Right;
    }

    public void TurnLeft()
    {
        direction = Direction.Left;
    }

    public void TurnStraight()
    {
        direction = Direction.Straight;
    }

    public void StartSmoke()
    {
        GetComponent<ParticleSystem>().Play();
    }

    public void StopSmoke()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}
