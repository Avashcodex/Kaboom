using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private ExitGames.Client.Photon.Hashtable myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    private void SetCustomNumber()
    {
        int result = Random.Range(0, 99);
        text.text = result.ToString();

        myCustomProperties["RandomNumber"] = result;

        PhotonNetwork.SetPlayerCustomProperties(myCustomProperties);
    }

    public void OnClick_Button()
    {
        SetCustomNumber();
    }
}
