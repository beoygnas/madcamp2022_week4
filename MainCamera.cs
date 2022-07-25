using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject character;
    Vector3 cameraPosition;
    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        cameraPosition = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (character == null)
        {
            rigidBody.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            Vector3 diff = new Vector3(character.transform.position.x - transform.position.x, character.transform.position.y - transform.position.y, 0);
            rigidBody.velocity = diff * 10 + cameraPosition;
        }
    }
}
