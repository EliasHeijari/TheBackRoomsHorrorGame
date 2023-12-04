using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGuide : MonoBehaviour
{
    bool hasSeen = false;
    public event EventHandler OnGuideSeen;

    private void OnTriggerEnter(Collider other)
    {
        hasSeen = true;
        OnGuideSeen?.Invoke(this, EventArgs.Empty);
    }

    public bool HasSeen()
    {
        return hasSeen;
    }
}
