using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private playercontroller player;
    public TMP_Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Playa").GetComponent<playercontroller>();

    }

    // Update is called once per frame
    void Update()
    {        
        coinText.text = "Coin: " + player.coin;
        
    }
}
