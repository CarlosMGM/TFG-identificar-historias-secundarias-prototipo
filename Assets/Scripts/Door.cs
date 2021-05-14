using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Door destinationDoor;

    public float destinationOffset = 1f;

    public override void Interact()
    {
        Debug.Log("interacting with door");
        Vector2 newPos = destinationDoor.transform.position;
        newPos.y += destinationOffset;

        player.transform.position = newPos;
    }
}
