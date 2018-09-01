using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour {

    public enum Direction
    {
        Clockwise,
        CounterClockwise
    }

    public Direction direction;

    private Transform arrow;
	// Use this for initialization
	void Start () {
        arrow = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        if(direction == Direction.Clockwise)
        {
            arrow.Rotate(Vector3.forward, 1f);
        }
        else
        {
            arrow.Rotate(Vector3.forward, -1f);
        }
    }
}
