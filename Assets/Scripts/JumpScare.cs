using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    [SerializeField] private string partyTag = "Party";
    [SerializeField] private string smileTag = "Smile";
    [SerializeField] private string bacteriaTag = "Bacteria";
    private void Start()
    {
        Player.OnJumpScare += Player_OnJumpScare;
    }

    private void Player_OnJumpScare(object sender, Player.JumpScareEventArgs e)
    {
        if (e.tag == partyTag)
        {
            Debug.Log("Party Jump Scare");
        }
        else if (e.tag == smileTag)
        {
            Debug.Log("Smile Jump Scare");
        }
        else if (e.tag == bacteriaTag)
        {
            Debug.Log("Bacteria Jump Scare");
        }
        Player.OnJumpScare -= Player_OnJumpScare;
    }
}
