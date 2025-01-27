﻿using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;
    public GameObject focusGraphic;

    Transform player;

    bool isFocus = false;
    bool hasInteracted = false;

    public virtual void Interact()
    {

    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;

        if (focusGraphic != null)
            focusGraphic.SetActive(true);
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;

        if (focusGraphic != null)
            focusGraphic.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}