using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    // Start is called before the first frame update

    public Transform Player;
    // Update is called once per frame
    void LateUpdate () {
        Vector3 CameraFollowPosition = Player.position;
        //set the camera position higher than the player
        CameraFollowPosition.y = transform.position.y;
        transform.position = CameraFollowPosition;

    }
}