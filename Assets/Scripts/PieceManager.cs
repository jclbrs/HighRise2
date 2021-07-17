using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public float yMove = -0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + yMove);
    }

    public void CollisionDetected()
	{
        Debug.Log("Piece notified of collision");
        yMove = 0f;
	}

}
