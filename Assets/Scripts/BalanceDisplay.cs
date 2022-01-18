using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text coinsText;
    [SerializeField] TMP_Text diamondsText;
    SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        DisplayBalance();
    }

    public void DisplayBalance()
    {
        // При изменении баланса вызывать эту функцию
        SaveManager.Instance.Load();
        coinsText.text = saveManager.State.coins.ToString();
        diamondsText.text = saveManager.State.gems.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
