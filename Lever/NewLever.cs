using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class NewLever : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float sensitivity = 0.04f;
    [SerializeField] private float leverClamp = 0.8f;
    [SerializeField] [Range(0f,1f)] private float stopZone = 0.2f;
    [SerializeField] private float extendHandle = 0.1f;

    [Header("Components")]
    [SerializeField] private GameObject handle;
    [SerializeField] private Transform handlePanel;
    [SerializeField] private LayerMask clickableLayers;
    [SerializeField] private GameObject eyesObject;
    [HideInInspector] public RailCart railCart; //is set in RailCart script

    private bool playerHoldingLever;
    private float mouseY;

    public float currentSpeed;

    private void Awake()
    {
        handle.transform.position = handlePanel.position + (handlePanel.TransformDirection(new Vector3(1 * mouseY, extendHandle, 0)) / handlePanel.transform.localScale.magnitude);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            playerHoldingLever = false;
        }

        if (playerHoldingLever && Input.GetMouseButton(1))
        {
            mouseY += Input.GetAxisRaw("Mouse Y") * sensitivity * Time.timeScale;

            mouseY = Mathf.Clamp(mouseY, -leverClamp, leverClamp);
            Vector3 pos = handlePanel.position + (handlePanel.TransformDirection(new Vector3(1 * mouseY, extendHandle, 0)) / handlePanel.transform.localScale.magnitude);
            handle.transform.position = pos;

            //float speed = -1 + (mouseY + leverClamp) * (2) / (leverClamp + leverClamp);
            float speed = -1 + (mouseY + leverClamp)/leverClamp;

            if (speed > stopZone || speed < -stopZone) currentSpeed = speed;
            else currentSpeed = 0;
        }
    }

    public void PlayerGrabLever()
    {
        playerHoldingLever = true;
        railCart.breaking = false;
    }
}
