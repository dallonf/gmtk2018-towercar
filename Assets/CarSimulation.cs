using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSimulation : MonoBehaviour
{
	public float CarRadius = 2;
	public LineRenderer lineRenderer;
	public CarDrive car;

	void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Update()
	{
		var timestep = 0.016f;
		var positions = new List<Vector3>();
		var currentPosition = car.transform.position;
		var forward = car.transform.forward;
		positions.Add(currentPosition);
		for (int i = 0; i < 1000; i++)
		{
			currentPosition += forward * car.Speed * timestep;
			positions.Add(currentPosition);
			if (Physics.CheckSphere(currentPosition, CarRadius, LayerMask.GetMask("Default")))
			{
				break;
			}
		}
		lineRenderer.positionCount = positions.Count;
		lineRenderer.SetPositions(positions.ToArray());
	}
}