using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour
{
	public float Speed;
	public new Collider collider;
	public AudioClip CrashAudioClip;
	public AudioClip RevAudioClip;
	public AudioSource EngineLoopAudioSource;
	public AudioSource OneShotAudioSource;

	public bool GotToGoal { get; set; }

	private CarBehavior carModelBehavior;

	private LevelManager levelManager;
	private Vector3 initialPosition;
	private Quaternion initalRotation;
	private bool crashed = false;

	public void Reset()
	{
		carModelBehavior.StopSmoke();
		GetComponentInChildren<ParticleSystem>().Clear();
		crashed = false;
		GotToGoal = false;
		transform.position = initialPosition;
		transform.rotation = initalRotation;
		EngineLoopAudioSource.Play();
	}
	
	public void OnPlay()
	{
		OneShotAudioSource.PlayOneShot(RevAudioClip);
	}

	void Start()
	{
		initialPosition = transform.position;
		initalRotation = transform.rotation;
	}

	void Awake()
	{
		levelManager = FindObjectOfType<LevelManager>();
		collider = GetComponent<Collider>();
		carModelBehavior = GetComponentInChildren<CarBehavior>();
	}

	void FixedUpdate()
	{
		if (crashed && EngineLoopAudioSource.isPlaying)
		{
			EngineLoopAudioSource.Stop();
			OneShotAudioSource.PlayOneShot(CrashAudioClip, 1);
		}

		if (levelManager.CurrentLevelMode == LevelManager.LevelMode.PLAY && !crashed && !GotToGoal)
		{
			var allTowers = FindObjectsOfType<Tower>();
			float totalTurn = 0;
			foreach (var tower in allTowers)
			{
				if (tower.IsCarAffected(transform.position))
				{
					totalTurn += tower.TurnEffect;
					transform.Rotate(Vector3.up, tower.TurnEffect * Time.deltaTime, Space.World);
				}
			}

			var newPosition = transform.position + Speed * Time.deltaTime * transform.forward;
			transform.position = newPosition;

			// Check if car is above ground, "crash" otherwise
			if (!Physics.Raycast(
					transform.position + Vector3.up * 0.5f,
					Vector3.down, 2,
					LayerMask.GetMask("Ground"),
					QueryTriggerInteraction.Ignore
				))
			{
				crashed = true;
			}

			carModelBehavior.StartSmoke();
			if (totalTurn > Mathf.Epsilon)
			{
				carModelBehavior.TurnRight();
			}
			else if (totalTurn < -Mathf.Epsilon)
			{
				carModelBehavior.TurnLeft();
			}
			else
			{
				carModelBehavior.TurnStraight();
			}
		}
		else
		{
			carModelBehavior.StopSmoke();
			carModelBehavior.TurnStraight();
		}
	}

	void OnCollisionEnter(Collision other)
	{
		crashed = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<GoalTrigger>())
		{
			GotToGoal = true;
		}
	}
}