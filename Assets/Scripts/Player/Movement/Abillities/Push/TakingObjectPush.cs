using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingObjectPush : MonoBehaviour
{
    private Transform parentTransform;
    private GameObject pushObject;

    [SerializeField] private GameEvent _PushObjectBiggerOn;
    [SerializeField] private GameEvent _PushObjectBiggerOff;
    private void Start()
    {
        parentTransform = gameObject.GetComponentInParent<Transform>();
    }
    private void FixedUpdate()
    {
        if (gameObject.GetComponent<TakingObjectPush>().enabled == true)
        {
            if (Pushing.isPushing == false)
            {
                pushObject.GetComponent<Rigidbody2D>().simulated = true;
                gameObject.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                pushObject.GetComponent<Rigidbody2D>().bodyType = Pushing.pushObjectType;
                gameObject.transform.parent.GetComponent<Transform>().SetParent(null);
                pushObject.GetComponent<Transform>().SetParent(null);
                gameObject.GetComponent<Transform>().SetParent(parentTransform);
                _PushObjectBiggerOff.Raise();
                gameObject.GetComponent<TakingObjectPush>().enabled = false;
            }
            else if (Pushing.isPushing == true)
            {
                if (pushObject.GetComponent<Rigidbody2D>().mass > gameObject.GetComponentInParent<Rigidbody2D>().mass)
                {
                    _PushObjectBiggerOn.Raise();
                    gameObject.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    gameObject.transform.parent.GetComponent<Transform>().SetParent(pushObject.transform);
                    gameObject.GetComponent<Transform>().SetParent(parentTransform);
                }
                else if (pushObject.GetComponent<Rigidbody2D>().mass <= gameObject.GetComponentInParent<Rigidbody2D>().mass)
                {
                    pushObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity.x, pushObject.GetComponent<Rigidbody2D>().velocity.y);
                    pushObject.GetComponent<Transform>().SetParent(gameObject.transform);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            pushObject = collision.gameObject;
    }
}
