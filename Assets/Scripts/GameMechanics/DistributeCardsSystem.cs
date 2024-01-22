using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DistributeCardsSystem : MonoBehaviourPun
{
    [SerializeField] SetPlayers setPlayers;
    [SerializeField] AllCards allCardsSO;
    [SerializeField] CardsDeck cardsDeck;

    private List<Candidate> candidates;    

    public event EventHandler<Candidate> OnCardUpdated;

    private void OnEnable()
    {
        setPlayers.OnPlayersSet += SetPlayers_OnPlayersSet;
    }

    private void OnDisable()
    {
        setPlayers.OnPlayersSet -= SetPlayers_OnPlayersSet;
    }
    private void SetPlayers_OnPlayersSet(object sender, List<Candidate> candidatesArg)
    {
        candidates = candidatesArg;
        if (PhotonNetwork.IsMasterClient)
        {
            DistributeCards();
        }

    }
    private void DistributeCards()
    {
        foreach (Candidate candidate in candidates)
        {           
            if (candidate==null)
            {
                continue;
            }
            
            //print(candidate.player.NickName);
            //print(candidate.candidateNumber);
            int[] cards = new int[] { candidate.candidateNumber, -1, -1, -1,-1 }; //cards also has candidate info
            for (int i = 0; i < 4; i++)
            {
                int randomCard = cardsDeck.GetARandomCardsID();
                AssignPlayerCards(candidate, randomCard);
                cards[i + 1] = randomCard;
            }
            
            //send rpc of candidate to everyone from master client 

            SendCandidateCardRPC(cards);
            OnCardUpdated?.Invoke(this, candidate);
        }
        //ShowCardsAllPlayers();
    }

    private void SendCandidateCardRPC(int[] cards)
    {
        base.photonView.RPC("ReceiveCandidateCardRPC", RpcTarget.Others, cards);
    }

    [PunRPC]
    private void ReceiveCandidateCardRPC(int[] cardsArg)
    {
        Candidate curCandidate = new Candidate();
        foreach (Candidate candidate in candidates)
        {
            if (candidate.candidateNumber == cardsArg[0])
            {
                curCandidate = candidate;
                break;
            }
        }

        for (int i = 1; i < 5; i++)
        {
            AssignPlayerCards(curCandidate, cardsArg[i]);
        }
        OnCardUpdated?.Invoke(this, curCandidate);
    }

    private void ShowCardsAllPlayers()
    {
        foreach (Candidate candidate in candidates)
        {
            print("Player " + candidate.player.NickName + " has cards:");
            List<Cards> cardTempStore =  candidate.cardsInHand;
            foreach (Cards cards in cardTempStore)
            {
                print(cards.cardID);
            }
            
        }
    }

    private void AssignPlayerCards(Candidate candidate, int cardID)
    {
        
        Cards cardTemp = allCardsSO.GetCard(cardID);
        candidate.cardsInHand.Add(cardTemp);
    }

}
