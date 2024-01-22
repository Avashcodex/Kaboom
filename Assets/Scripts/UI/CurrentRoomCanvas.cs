using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField] private PlayerListingMenu playerListingMenu;
    [SerializeField] private LeaveRoomMenu leaveRoomMenu;
    public LeaveRoomMenu LeaveRoomMenu { get { return leaveRoomMenu; } }

    private RoomsCanvases roomsCanvases;
    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
        playerListingMenu.FirstInitialize(canvases);
        leaveRoomMenu.FirstInitialize(canvases);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
