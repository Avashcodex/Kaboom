using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMoveTest : MonoBehaviourPun
{
    public Transform circle;
    int i = 0;
    public void OnClick_MoveCircle()
    {
        if (base.photonView.IsMine)
        {
            if (i == 0)
            {
                circle.position = new Vector2(circle.position.x, 2.9f);
                i = 1;
            }
            else
            {
                circle.position = new Vector2(circle.position.x, 1.5f);
                i = 0;
            }
        }
        
    }
}
