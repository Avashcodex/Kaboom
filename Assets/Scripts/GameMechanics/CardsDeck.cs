using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsDeck : MonoBehaviour
{
    [HideInInspector]
    public bool[] cardsInDeck = new bool[53]; //first value is nothing

    private void Awake()
    {        
        for (int i = 1; i < 53; i++)
        {
            cardsInDeck[i] = true;
        }
        
    }

    public int GetARandomCardsID()
    {
        while (true)
        {
            int randomInt = Random.Range(1, 52);
            if (cardsInDeck[randomInt])
            {
                cardsInDeck[randomInt] = false;
                return randomInt;                
            }
        }
        
    }
}
