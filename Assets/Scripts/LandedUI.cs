using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class landedUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private Button nextButton;

    private void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    private void Start()
    {
        Lander.Instance.OnLanded += Lander_OnLanded;

        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    { 
        if(e.landingType == Lander.LandingType.Success)
        {
            titleTextMesh.text = "Successful Landing!";
        }
        else
        {
            titleTextMesh.text = "<color=#ff0000>CRAH!</color>";
        }

        statsTextMesh.text =
            e.landingSpeed + "\n" +
            e.dotVector + "\n" +
            "x" + e.scoreMultiplier + "\n" +
            e.score;

        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
