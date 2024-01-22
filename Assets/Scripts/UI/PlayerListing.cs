using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI text;

    public Player Player { get; private set; }
    public bool Ready = false;
   
    public void SetPlayerInfo(Player playerArg)
    {
        Player = playerArg;
        SetPlayerText(playerArg);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer != null && targetPlayer==Player)
        {
            if (changedProps.ContainsKey("RandomNumber"))
            {
                SetPlayerText(targetPlayer);
            }
        }
    }

    private void SetPlayerText(Player playerArg)
    {
        int result = -1;
        if (playerArg.CustomProperties.ContainsKey("RandomNumber"))
        {
            result = (int)playerArg.CustomProperties["RandomNumber"];
        }
        text.text = result.ToString() + ", " + playerArg.NickName;
    }
}
