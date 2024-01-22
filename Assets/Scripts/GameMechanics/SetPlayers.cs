using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetPlayers : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject[] playerPosition;    

    public List<Candidate> candidates = new List<Candidate>();
    
    public event EventHandler<List<Candidate>> OnPlayersSet;

    private string[] userIDs = new string[4];

    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            userIDs[i] = "NA";
        }
    }
    void Start()
    {        
        if (PhotonNetwork.IsMasterClient)
        {
            SetPlayer();    
        }
    }   

    public void SetPlayer()
    {
        int i = 0;
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;

        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {           

            Candidate candidate = new Candidate();
            candidate.candidateNumber = i;
            candidate.player = playerInfo.Value;            
            candidates.Add(candidate);
            userIDs[i] = playerInfo.Value.UserId;            
            //print("Userid: "+ userIDs[i]);
            i++;
            
        }
        
        SendCandidatesRPC();
        PlayerPositionSetter();
    }

    private void PlayerPositionSetter()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        int index = candidates.FindIndex(x => x.player == PhotonNetwork.LocalPlayer);
        int localPlayerIndex = index;
        playerPosition[0].GetComponent<TextMeshProUGUI>().text = candidates[index].player.NickName;
        candidates[index].localPlayerNumber = 0;
        int counter = 1;
        while (counter < playerCount)
        {
            index++;
            if (index > playerCount - 1)
            {
                index = 0;
            }

            playerPosition[counter].GetComponent<TextMeshProUGUI>().text = candidates[index].player.NickName;
            candidates[index].localPlayerNumber = counter;
            counter++;
        }
        OnPlayersSet?.Invoke(this, candidates);

    }


    private void SendCandidatesRPC()
    {
        base.photonView.RPC("ReceiveCandidateRPC", RpcTarget.Others, userIDs);
        //print("RPC sent");
    }

    private void RetrievePlayerList(string[] userIDs)
    {
        for (int i = 0; i < 4; i++)
        {
            if (userIDs[i] != "NA")
            {
                foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
                {
                    if (userIDs[i] == playerInfo.Value.UserId)
                    {
                        Candidate candidate = new Candidate();
                        candidate.candidateNumber = i;
                        candidate.player = playerInfo.Value;
                        candidates.Add(candidate);
                    }
                }
            }
        }
        PlayerPositionSetter();
        DisplayCandidates();
    }

    [PunRPC]
    private void ReceiveCandidateRPC(string[] userIDs)
    {        
        for (int i = 0; i < 4; i++)
        {
            print(userIDs[i]);
        }
        RetrievePlayerList(userIDs);        
    }

    private void DisplayCandidates()
    {
        int i = 1;
        foreach (Candidate candidate in candidates)
        {
            print("p"+i+" :"+candidate.player.NickName);
            i++;
        }
    }

    

    
}
