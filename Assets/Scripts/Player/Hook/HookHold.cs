using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookHold : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject hook;

    private GameObject hookObj;


    private bool canHold;

    private void Awake()
    {
        canHold = true;
        HookHoldCheck.HookIsHold += Hold;
    }

    private void OnDestroy()
    {
        HookHoldCheck.HookIsHold -= Hold;
    }

    //private void FixedUpdate()
    //{
    //    if(hookObj != null)
    //    {
    //        if (hookObj.GetComponent<Rigidbody2D>().mass <= player.GetComponentInParent<Rigidbody2D>().mass)
    //        {
    //            hookObj.GetComponent<Rigidbody2D>().velocity += gameObject.GetComponent<Rigidbody2D>().velocity;
    //        }
    //    }
    //}

    private void Hold(Collision2D hookObjectCollision)
    {
        hookObj = hookObjectCollision.gameObject;
        if (canHold && hookObjectCollision.gameObject.GetComponent<Rigidbody2D>().mass > player.GetComponentInParent<Rigidbody2D>().mass)
        {
            canHold = false;
            Debug.Log("масса больше");
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            gameObject.transform.SetParent(hookObjectCollision.gameObject.transform);
            gameObject.GetComponent<FixedJoint2D>().enabled = true;
            gameObject.GetComponent<SpringJoint2D>().enabled = false;
            player.GetComponent<SpringJoint2D>().enabled = true;
        }
        if (canHold && hookObjectCollision.gameObject.GetComponent<Rigidbody2D>() == null)
        {
            canHold = false;
            Debug.Log("масса больше");
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            gameObject.GetComponent<FixedJoint2D>().enabled = true;
            gameObject.GetComponent<SpringJoint2D>().enabled = false;
            player.GetComponent<SpringJoint2D>().enabled = true;
        }
        if (canHold && hookObjectCollision.gameObject.GetComponent<Rigidbody2D>().mass <= player.GetComponentInParent<Rigidbody2D>().mass)
        {
            canHold = false;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            Debug.Log("масса меньше");
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            hookObjectCollision.gameObject.GetComponent<Transform>().SetParent(gameObject.transform);
            hookObjectCollision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            hookObjectCollision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            //hookObjectCollision.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            Debug.Log(hookObjectCollision.gameObject);
        }
    }
}
