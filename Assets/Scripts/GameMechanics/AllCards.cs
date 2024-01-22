using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "card")]
public class AllCards : ScriptableObject
{    
    [SerializeField] Cards[] AllCardsList = null;

    public Cards GetCard(int cardID)
    {
        return AllCardsList[cardID-1];
    }    

}
