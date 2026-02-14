using TMPro;
using UnityEngine;

public class landedUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statsTextMesh;
     
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
