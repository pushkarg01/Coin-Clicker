using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;

    [SerializeField] private Button mainButton;
    [SerializeField] private Button multiplierButton1;
    [SerializeField] private Button multiplierButton2;
    [SerializeField] private Button multiplierButton3;

    public int multiplierCost1 = 50;
    public TextMeshProUGUI multiplierOneCostText;

    public int multiplierCost2 = 300;
    public TextMeshProUGUI multiplierTwoCostText;

    public int multiplierCost3 = 500;
    public TextMeshProUGUI multiplierThreeCostText;

    private int clickAmount = 1;
    public TextMeshProUGUI clickAmountText;

    void Start()
    {
        ButtonOnStart();
    }

    public void FixedUpdate()
    {
        scoreText.text = "Score: " + score;
        clickAmountText.text = clickAmount + " per click";

        multiplierOneCostText.text = "COST: " + multiplierCost1;
        multiplierTwoCostText.text = "COST: " + multiplierCost2;
        multiplierThreeCostText.text = "COST: " + multiplierCost3;

        ButtonInteractable();
    }

    void ButtonOnStart()
    {
        multiplierButton1.interactable = false;
        multiplierButton2.interactable = false;
        multiplierButton3.interactable = false;
    }

    private void ButtonInteractable()
    {
        if (score >= multiplierCost3)
        {
            multiplierButton1.interactable = true;
            multiplierButton2.interactable = true;
            multiplierButton3.interactable = true;
        }
        else if (score >= multiplierCost2 && score <= multiplierCost3)
        {
            multiplierButton1.interactable = true;
            multiplierButton2.interactable = true;
            multiplierButton3.interactable = false;
        }
        else if (score >= multiplierCost1 && score <= multiplierCost2)
        {
            multiplierButton1.interactable = true;
            multiplierButton2.interactable = false;
            multiplierButton3.interactable = false;
        }
        else
        {
            ButtonOnStart();
        }
    }

    public void AddCoins()
    {
        AnimateButton(mainButton);
        score += clickAmount;
    }

    public void BuyMultiplierOne()
    {
        if (score >= multiplierCost1)
        {
            score -= multiplierCost1;
            clickAmount *= 2;
            AnimateButton(multiplierButton1);
            multiplierCost1 += 25;
        }
    }

    public void BuyMultiplierTwo()
    {
        if (score >= multiplierCost2)
        {
            score -= multiplierCost2;
            clickAmount *= 3;
            AnimateButton(multiplierButton2);
            multiplierCost2 += 50;
        }
    }

    public void BuyMultiplierThree()
    {
        if (score >= multiplierCost3)
        {
            score -= multiplierCost3;
            clickAmount *= 5;
            AnimateButton(multiplierButton3);
            multiplierCost3 += 100;
        }
    }

    private void AnimateButton(Button button)
    {
        button.transform.DOScale(1.2f, 0.1f).OnKill(() => button.transform.DOScale(1f, 0.1f));
        button.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 10, 1f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
