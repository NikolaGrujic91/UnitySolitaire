using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour {

    public GameObject gameOverPanel;
    public GameObject score;
    private Foundation foundationCuori;
    private Foundation foundationQuadri;
    private Foundation foundationFiori;
    private Foundation foundationPicche;
    private const string foundationCuoriName = "FoundationCuori";
    private const string foundationQuadriName = "FoundationQuadri";
    private const string foundationFioriName = "FoundationFiori";
    private const string foundationPiccheName = "FoundationPicche";

    // Use this for initialization
    void Start () {
        if (gameOverPanel == null)
            Debug.LogError("gameOverPanel is not assigned");

        if (score == null)
            Debug.LogError("score is not assigned");

        foundationCuori = GameObject.Find(foundationCuoriName).GetComponent<Foundation>();
        foundationQuadri = GameObject.Find(foundationQuadriName).GetComponent<Foundation>();
        foundationFiori = GameObject.Find(foundationFioriName).GetComponent<Foundation>();
        foundationPicche = GameObject.Find(foundationPiccheName).GetComponent<Foundation>();
    }
	
	// Update is called once per frame
	void Update () {
        if (foundationCuori.finished && foundationQuadri.finished && foundationFiori.finished && foundationPicche.finished)
        {
            gameOverPanel.SetActive(true);           
            score.GetComponent<Text>().text = (2 * Score.score - 10 * UnityEngine.Time.timeSinceLevelLoad).ToString();
            UnityEngine.Time.timeScale = 0;
        }
	}

    public void OnClick()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        gameOverPanel.SetActive(false);
    }
}
