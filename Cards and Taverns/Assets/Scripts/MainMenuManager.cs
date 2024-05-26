using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject infoPanel;
    private bool _infPan;
    public void SwitchInfoPanel()
    {
        _infPan = !_infPan;
        infoPanel.SetActive(_infPan);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
