using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadBlocker : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
