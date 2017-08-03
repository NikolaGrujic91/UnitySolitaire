using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public GameObject pausePanel;

    // Use this for initialization
    void Start () {
        if (pausePanel == null)
            Debug.LogError("pausePanel is not assigned");

        pausePanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UnityEngine.Time.timeScale == 1)
            {
                UnityEngine.Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
            else
            {
                UnityEngine.Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
        }
    }

    public void Continue()
    {
        UnityEngine.Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        UnityEngine.Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
}
