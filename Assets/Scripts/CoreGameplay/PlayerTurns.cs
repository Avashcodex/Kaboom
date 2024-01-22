using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTurns : MonoBehaviourPun
{
    [SerializeField] SetPlayers setPlayers;
    [SerializeField] float waitTimeFirstPlay=2f;
    [SerializeField] float playerTurnTime = 5f;
    [SerializeField] TextMeshProUGUI testPlayerTurnDisplay;    

    const byte PLAYER_TURN_EVENT = 101;
    private List<Candidate> candidates;
    private Candidate localCandidate;
    private void OnEnable()
    {
        setPlayers.OnPlayersSet += SetPlayers_OnPlayersSet;
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable()
    {
        setPlayers.OnPlayersSet -= SetPlayers_OnPlayersSet;
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        print("even received"+obj.Code.ToString());
        
        if (obj.Code ==PLAYER_TURN_EVENT)
        {

            print("INside event");
            int candidateNum = (int)obj.CustomData;
            if (candidateNum == localCandidate.candidateNumber)
            {
                //send rpc
                            
                print("current turn "+ candidateNum.ToString());
                StartCoroutine(PlayTurn(candidateNum));
            }
        }
        
    }

    [PunRPC]
    private void SetPlayerTurnTextRPC(string name)
    {
        testPlayerTurnDisplay.text = name;

    }

    IEnumerator StartFirstTurn(float waitTime)
    {
        
        yield return new WaitForSeconds(waitTime);        
        object candidateNumber = candidates[0].candidateNumber;
        StartCoroutine(PlayTurn((int)candidateNumber));
        //PhotonNetwork.RaiseEvent(PLAYER_TURN_EVENT, candidateNumber, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        //print("event first  time sent");
        // raise event and tell everyone 

    }

    IEnumerator PlayTurn(int candidateNumber)
    {    
        
        string displayName = localCandidate.player.NickName;
        base.photonView.RPC("SetPlayerTurnTextRPC", RpcTarget.All, displayName);
        yield return new WaitForSeconds(playerTurnTime);

        //give turn to next player
        object nextCandidateNumber = GetNextCandidateNumber(candidateNumber);
        PhotonNetwork.RaiseEvent(PLAYER_TURN_EVENT, nextCandidateNumber, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    private int GetNextCandidateNumber(int currentCandidateNumber)
    {
        int totalPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        int nextCandidateNumber = currentCandidateNumber + 1;
        if (nextCandidateNumber>totalPlayers-1)
        {
            nextCandidateNumber = 0;
        }

        return nextCandidateNumber;
    }

    private void SetPlayers_OnPlayersSet(object sender, List<Candidate> candidateArgs)
    {
        candidates = candidateArgs;
        foreach (Candidate candidate in candidates)
        {
            if (candidate.player == PhotonNetwork.LocalPlayer)
            {
                localCandidate = candidate;
                break;
            }
        }
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(StartFirstTurn(waitTimeFirstPlay));      

        }
    }


    
    



}
