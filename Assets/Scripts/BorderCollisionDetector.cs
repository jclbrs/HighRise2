using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This approach did not work well.  Switched to the trigger approach (below)
    private void OnCollisionEnter2D(Collision2D collider)
    {
        Debug.Log($"Border {gameObject.name} detected collision");
        SendMessageUpwards("CollisionDetected", gameObject.name);
    }


    private void OnTriggerEnter2D(Collider2D collider2D)
	{
        //Debug.Log($"Border {gameObject.name} detected trigger");
        SendMessageUpwards("CollisionDetected", collider2D);
	}
}
