using UnityEngine;

public class HUDController : MonoBehaviour
{
  public GameObject PlayButton;
  public GameObject StopButton;
  private LevelManager levelManager;

  void Awake()
  {
    levelManager = FindObjectOfType<LevelManager>();
  }

  void Update()
  {
    PlayButton.SetActive(levelManager.CurrentLevelState == LevelManager.LevelState.BUILD);
    StopButton.SetActive(levelManager.CurrentLevelState == LevelManager.LevelState.PLAY);
  }

  public void HandlePlayButtonClick()
  {
    levelManager.Play();
  }

  public void HandleStopButtonClick()
  {
    levelManager.Stop();
  }
}