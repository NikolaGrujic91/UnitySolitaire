using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static int score = 0;

	// Use this for initialization
	void Start () {
        if (this.GetComponent<Text>() == null)
            Debug.LogError("There is no gui text component");
    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = score.ToString();
    }
}
