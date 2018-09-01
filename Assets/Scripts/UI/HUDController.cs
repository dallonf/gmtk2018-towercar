using UnityEngine;

public class HUDController : MonoBehaviour
{
  public GameObject PlayButton;
  public GameObject StopButton;
  public GameObject BuildModePanel;
  private LevelManager levelManager;

  void Awake()
  {
    levelManager = FindObjectOfType<LevelManager>();
  }

  void Update()
  {
    PlayButton.SetActive(levelManager.CurrentLevelMode == LevelManager.LevelMode.BUILD);
    StopButton.SetActive(levelManager.CurrentLevelMode == LevelManager.LevelMode.PLAY);

    BuildModePanel.SetActive(levelManager.CurrentLevelMode == LevelManager.LevelMode.BUILD);
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