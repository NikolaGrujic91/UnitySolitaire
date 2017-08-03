using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Moves : MonoBehaviour {

    public static int moves = 0;

	// Use this for initialization
	void Start () {
        moves = 0;

        if (this.GetComponent<Text>() == null)
            Debug.LogError("There is no gui text component");
    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = moves.ToString();
    }
}
