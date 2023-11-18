using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderJumpScare : MonoBehaviour
{
    [SerializeField] private GameObject spider;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            spider.SetActive(true);
        }
    }
}
