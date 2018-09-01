using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSimulation : MonoBehaviour
{
	public float CarRadius = 2;
	public Vector3 offset = Vector3.up;
	public LineRenderer lineRenderer;
	public CarDrive car;
	
	private LevelManager levelManager;

	void Awake()
	{
		levelManager = FindObjectOfType<LevelManager>();
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Update()
	{
		if (levelManager.CurrentLevelMode == LevelManager.LevelMode.BUILD) {
			var allTowers = FindObjectsOfType<Tower>();
			var timestep = 0.016f;
			var positions = new List<Vector3>();
			var currentPosition = car.transform.position;
			var forward = car.transform.forward;
			positions.Add(currentPosition + offset);
			for (int i = 0; i < 1000; i++)
			{
				foreach (var tower in allTowers)
				{
					if (tower.IsCarAffected(currentPosition))
					{
						forward = Quaternion.AngleAxis(tower.TurnEffect * timestep, Vector3.up) * forward;
					}
				}
				currentPosition += forward * car.Speed * timestep;
				positions.Add(currentPosition + offset);
				if (Physics.CheckSphere(currentPosition, CarRadius, LayerMask.GetMask("Default")))
				{
					break;
				}
			}
			lineRenderer.positionCount = positions.Count;
			lineRenderer.SetPositions(positions.ToArray());
			lineRenderer.enabled = true;
		} else {
			lineRenderer.enabled = false;
		}
	}
}