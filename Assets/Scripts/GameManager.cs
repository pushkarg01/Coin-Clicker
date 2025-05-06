using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int coins = 0;               
    public TextMeshProUGUI coinText;                
    public Button tapButton;         
    public Button autoCollectButton;    
    public Button multiplierButton;    

    public bool isMultiplierActive = false; 
    public bool isAutoCollectorUsed = false;

    private Coroutine autoCollectCoroutine;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UpdateCoinUI();

        autoCollectButton.interactable = false;
        multiplierButton.interactable = false;
    }

    public void UpdateCoinUI()
    {
        coinText.text = "Coins: " + coins;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinUI();
    }

    public void DeductCoins(int amount)
    {
        coins -= amount;
        UpdateCoinUI();
    }

    public void ActivateMultiplier()
    {
        if (coins >= 60 && !isAutoCollectorUsed && !isMultiplierActive)
        {
            DeductCoins(60);
            isMultiplierActive = true;
            UpdateButtonStates();
            AnimateButton(multiplierButton); 
        }
    }

    public void ActivateAutoCollector()
    {
        if (coins >= 100 && !isAutoCollectorUsed)
        {
            DeductCoins(100);
            isAutoCollectorUsed = true;
            UpdateButtonStates();
            AnimateButton(autoCollectButton);

            if (autoCollectCoroutine != null)
                StopCoroutine(autoCollectCoroutine);
            autoCollectCoroutine = StartCoroutine(AutoCollectCoins());
        }
    }

    private IEnumerator AutoCollectCoins()
    {
        while (isAutoCollectorUsed)
        {
            AddCoins(1);
            yield return new WaitForSeconds(1f);
        }
    }

    public void UpdateButtonStates()
    {
        autoCollectButton.interactable = false; 
        multiplierButton.interactable = false; 
    }

    private void AnimateButton(Button button)
    {
        button.transform.DOScale(1.2f, 0.1f).OnKill(() => button.transform.DOScale(1f, 0.1f));  
        button.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 10, 1f);  
    }

    public int GetMainButtonCoinIncrement()
    {
        return isMultiplierActive ? 2 : 1; 
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
