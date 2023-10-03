using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{

    bool isAlreadyCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isAlreadyCollected)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            MoneyCollect myCollect;
            if (other.TryGetComponent(out myCollect))
            {

                myCollect.AddNewItem(this.transform);
                isAlreadyCollected = true;
            }
        }

        if (other.gameObject.CompareTag("MoneyHolder"))
        {
            Destroy(this);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isAlreadyCollected)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<CapsuleCollider>().isTrigger = true;
            Destroy(GetComponent<Rigidbody>());
            //GetComponent<Rigidbody>().freezeRotation=true;
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            Destroy(GetComponent<MoneyCollect>());
            MoneyCollect myCollect;
            if (other.gameObject.TryGetComponent(out myCollect))
            {
                myCollect.AddNewItem(this.transform);
                isAlreadyCollected = true;
            }
        }
    }

    // farklı bir coin gönderme efekti

    //private bool collected = false;

    //void Update()
    //{
    //    if (collected)
    //    {
    //        Vector3 targetPos = UIController.Instance.GetIconPosition(transform.position);

    //        if (Vector2.Distance(transform.position, targetPos) > 1f)
    //        {
    //            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
    //        }
    //        else
    //        {
    //            gameObject.SetActive(false);
    //        }
    //    }
    //}

    //public void SetCollected()
    //{
    //    collected = true;
    //}

}
