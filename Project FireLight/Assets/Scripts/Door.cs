using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float cooldown = 2f;
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;

    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine("ToggleDoor");
        }
    }

    IEnumerator ToggleDoor() {
        boxCollider.enabled = false;
        meshRenderer.enabled = false;
        yield return new WaitForSeconds(cooldown);
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
    }
}
