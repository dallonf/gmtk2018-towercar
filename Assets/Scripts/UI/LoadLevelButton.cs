using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelButton : MonoBehaviour
{
  public string LevelName;
  public bool GrayIfBeaten;
    public GameObject checkMarkPrefab;

  public void LoadLevel()
  {
    SceneManager.LoadScene(LevelName);
  }

  void Start()
  {
    if (GrayIfBeaten && LevelProgress.GetInstance().IsLevelBeaten(LevelName))
    {
            GameObject checkmark = Instantiate(checkMarkPrefab, transform);
            checkmark.transform.parent = transform;
            checkmark.transform.position = checkmark.transform.position - new Vector3(0, 5f, 0);
            checkmark.transform.localScale = new Vector3(.25f, .25f, .25f);
    }
  }
}
