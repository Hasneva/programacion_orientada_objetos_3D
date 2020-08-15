using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] Vector3 posicionCamara;

    // Start is called before the first frame update
    void Start()
    {
        posicionCamara = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x,-0.25f,0.04f),Mathf.Clamp(player.position.y,0,35f),transform.position.z);

        //transform.position = player.position + posicionCamara;
    }
}
