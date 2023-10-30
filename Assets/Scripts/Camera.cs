using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    private Vector3 _cameraPos1;
    private Vector3 _cameraPos2;
    private Vector3 _cameraPos3;
    private Vector3 _cameraPos4;

    [SerializeField]
    private GameObject player;




    // Start is called before the first frame update
    void Start()
    {
        _cameraPos1 = new Vector3(0, 0, -10);
        _cameraPos2 = new Vector3(17.6f, 0, -10);
        _cameraPos3 = new Vector3(-17.8f, 0, -10);
        _cameraPos4 = new Vector3(35.5f, 0, -10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.transform.position.x >= 9f && player.transform.position.x < 27f)
        {
            transform.position = _cameraPos2;
        }
        if (player.transform.position.x >= 27f )
        {
            transform.position = _cameraPos4;
        }
        if (player.transform.position.x < -9f)
        {
            transform.position = _cameraPos3;
        }
        if (player.transform.position.x >= -9f && player.transform.position.x <9f)
        {
            transform.position = _cameraPos1;
        }
    }
}
