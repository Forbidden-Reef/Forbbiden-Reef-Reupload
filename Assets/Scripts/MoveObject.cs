using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//hi
public class MoveObject : MonoBehaviour {
    public GameObject item;
    public GameObject tempParent;
    public Transform guide;
    public bool inHand = false;

    void Start () {
        item.GetComponent<Rigidbody> ().useGravity = true;
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.E) && checkDistance () && inHand == false) {

            item.GetComponent<Rigidbody> ().useGravity = false;
            item.GetComponent<Rigidbody> ().isKinematic = true;
            item.transform.position = guide.transform.position;
            item.transform.rotation = guide.transform.rotation;
            item.transform.parent = tempParent.transform;
            inHand = true;
        } else if (Input.GetKeyDown (KeyCode.E) && inHand == true) {
            item.GetComponent<Rigidbody> ().useGravity = true;
            item.GetComponent<Rigidbody> ().isKinematic = false;
            item.transform.position = guide.transform.position;
            item.transform.parent = null;
            inHand = false;
        }
    }

    bool checkDistance () {
        float distance = Vector3.Distance (item.transform.position, tempParent.transform.position);
        // print(distance);
        if (distance < 1.50) {
            return true;
        } else {
            return false;
        }

    }

}