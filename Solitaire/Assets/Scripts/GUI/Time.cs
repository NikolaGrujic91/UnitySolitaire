using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Time : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (this.GetComponent<Text>() == null)
            Debug.LogError("There is no gui text component");

        InvokeRepeating("DecreaseScore", 1.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
        float totalSeconds = UnityEngine.Time.timeSinceLevelLoad;
        int minutes = (int)(totalSeconds % 3600) / 60;
        int seconds = (int)(totalSeconds % 60);

        this.GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    private void DecreaseScore()
    {
        Score.score--;
    }
}
