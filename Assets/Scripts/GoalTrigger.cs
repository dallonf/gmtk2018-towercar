using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<CarDrive>())
    {
      GetComponent<GoalBehavior>().TriggerFireworks();
      GetComponent<AudioSource>().Play();
    }
  }
}