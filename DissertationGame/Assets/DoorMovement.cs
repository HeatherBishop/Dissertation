using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour {

    public Vector2 Currentposition;
    public Vector2 EndPosition;



    // Use this for initialization
    void Start() {
        Currentposition = this.gameObject.transform.position;
        EndPosition = new Vector2(0, -4);



    }

    // Update is called once per frame
    void Update() {

        transform.position = Vector2.Lerp(Currentposition, EndPosition, 3.0f * Time.deltaTime);
        gameObject.SetActive(false);
    }

}

