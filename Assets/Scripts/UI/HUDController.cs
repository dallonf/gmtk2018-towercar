using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
  [System.Serializable]
  public struct WinPanelElements
  {
    public GameObject WinPanel;
    public TextMeshProUGUI YourTowersText;
    public TextMeshProUGUI YourTimeText;
    public TextMeshProUGUI ParTowersText;
    public TextMeshProUGUI ParTimeText;
  }
  public GameObject PlayButton;
  public GameObject StopButton;
  public GameObject BuildModePanel;
  public WinPanelElements WinPanel;
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
    WinPanel.WinPanel.SetActive(levelManager.CurrentLevelMode == LevelManager.LevelMode.WIN);
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