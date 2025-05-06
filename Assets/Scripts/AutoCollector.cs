using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AutoCollector : MonoBehaviour
{  
    void Start()
    {
        GameManager.Instance.autoCollectButton.onClick.AddListener(ActivateAutoCollector);
        GameManager.Instance.multiplierButton.onClick.AddListener(ActivateMultiplier);
        GameManager.Instance.tapButton.onClick.AddListener(OnMainButtonClick);

        GameManager.Instance.autoCollectButton.interactable = false;
        GameManager.Instance.multiplierButton.interactable = false;
    }

    void Update()
    {
        GameManager.Instance.autoCollectButton.interactable = GameManager.Instance.coins >= 25 && !GameManager.Instance.isAutoCollectorUsed;
        GameManager.Instance.multiplierButton.interactable = GameManager.Instance.coins >= 30 && !GameManager.Instance.isMultiplierActive;
    }

    void ActivateAutoCollector()
    {
        GameManager.Instance.ActivateAutoCollector();
    }

    void ActivateMultiplier()
    {
        GameManager.Instance.ActivateMultiplier();
    }

    void OnMainButtonClick()
    {
        int coinsToAdd = GameManager.Instance.GetMainButtonCoinIncrement();
        GameManager.Instance.AddCoins(coinsToAdd);

        AnimateButton(GameManager.Instance.tapButton); 

        if (GameManager.Instance.coins >= 100 && GameManager.Instance.isMultiplierActive)
        {
            GameManager.Instance.isMultiplierActive = false;
            GameManager.Instance.UpdateButtonStates(); 
        }
    }

    private void AnimateButton(Button button)
    {
        button.transform.DOScale(1.2f, 0.1f).OnKill(() => button.transform.DOScale(1f, 0.1f)); 
        button.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 10, 1f);  
    }
}
