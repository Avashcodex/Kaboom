using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class DisplayCards : MonoBehaviourPun
{
    [SerializeField] DistributeCardsSystem distributeCardSystem;
    
    [SerializeField] GameObject[] players;
    [SerializeField] Transform centralDeck;

    [SerializeField] float cardDistributionTime=1f;
    //[SerializeField] GameObject[] playersCard;
    
    /*
    [SerializeField] GameObject testcardPrefab;
    [SerializeField] Transform testStartTransform;
    [SerializeField] Transform testEndTransform;
    [SerializeField] float testDuration;*/


    private Candidate candidate;

    // currently coding for only the main i.e player1 card distribution

    private void Start()
    {
        //StartCoroutine(lerper(testcardPrefab, testStartTransform, testEndTransform, testDuration));
        //testcardPrefab.transform.rotation = Quaternion.Euler(0f, 90f, -90f);
    }

    private void OnEnable()
    {
        distributeCardSystem.OnCardUpdated += DistributeCardSystem_OnCardUpdated;
    }

    private void OnDisable()
    {
        distributeCardSystem.OnCardUpdated -= DistributeCardSystem_OnCardUpdated;
    }

    private void DistributeCardSystem_OnCardUpdated(object sender, Candidate candidateArg)
    {
        
        candidate = candidateArg;         
        int playerNumber = candidate.localPlayerNumber;        

        StartCoroutine(Distributer(candidate, playerNumber));
        /*
        foreach (Transform child in players[playerNumber].transform)
        {
            
            GameObject cardPrefab = Instantiate(candidate.cardsInHand[i].cardPrefab, centralDeck);
            StartCoroutine(lerper(cardPrefab, centralDeck, child.transform, 1f));
            //wait for second
            i++;
        }*/
        
    }

    IEnumerator Distributer(Candidate candidate ,int playerNumber)
    {
        int i = 0;
        foreach (Transform child in players[playerNumber].transform)
        {

            GameObject cardPrefab = Instantiate(candidate.cardsInHand[i].cardPrefab, centralDeck);
            if (candidate.localPlayerNumber%2!=0)
            {
                cardPrefab.transform.rotation = Quaternion.Euler(0f, 90f, -90f);
            }
            else
            {
                cardPrefab.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            }
            
            StartCoroutine(CardPositionlerper(cardPrefab, centralDeck, child.transform, cardDistributionTime));
            cardPrefab.transform.SetParent(child);
            //cardPrefab.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            yield return new WaitForSeconds(cardDistributionTime + 0.1f);
            i++;
        }
    }

   
    IEnumerator  CardPositionlerper(GameObject cardPrefab, Transform from, Transform to, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime< duration)
        {
            float t = elapsedTime / duration;
            cardPrefab.transform.position = Vector2.Lerp(from.position, to.position, t);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        cardPrefab.transform.position = to.position;
        
        //cardPrefab.transform.localPosition = Vector3.zero;
        
    }


}
