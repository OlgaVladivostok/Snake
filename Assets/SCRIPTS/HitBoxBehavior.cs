﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBehavior : MonoBehaviour
{

    SnakeMovement SM;


    void Start()
    {
        SM = transform.GetComponentInParent<SnakeMovement>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Box") && transform == SM.BodyParts[0])
        {


            if (SM.BodyParts.Count > 1 && SM.BodyParts[1] != null)
            {
                SM.PartsAmountTextMesh.transform.parent = SM.BodyParts[1];
                SM.PartsAmountTextMesh.transform.position = SM.BodyParts[1].position +
                    new Vector3(0, 0.5f, 0);
            }
            else if (SM.BodyParts.Count == 1)
            {
                SM.PartsAmountTextMesh.transform.parent = null;
            }

            SM.SnakeParticle.Stop();

            SM.SnakeParticle.transform.position = collision.contacts[0].point;


            SM.SnakeParticle.Play();

            Destroy(this.gameObject);

            GameController.SCORE++;

            collision.transform.GetComponent<AutoDestroy>().life -= 1;
            collision.transform.GetComponent<AutoDestroy>().UpdateText();

         //   collision.transform.GetComponent<AutoDestroy>().SetBoxColor();

            SM.BodyParts.Remove(SM.BodyParts[0]);


        }


        else if (collision.transform.CompareTag("SimpleBox") && transform == SM.BodyParts[0])
        {

            SM.SnakeParticle.Stop();


            SM.SnakeParticle.transform.position = collision.contacts[0].point;


            SM.SnakeParticle.Play();


            if (SM.BodyParts.Count > 1 && SM.BodyParts[1] != null)
            {
                SM.PartsAmountTextMesh.transform.parent = SM.BodyParts[1];
                SM.PartsAmountTextMesh.transform.position = SM.BodyParts[1].position +
                    new Vector3(0, 0.5f, 0);
            }
            else if (SM.BodyParts.Count == 1)
            {
                SM.PartsAmountTextMesh.transform.parent = null;
            }

            Destroy(this.gameObject);


            GameController.SCORE++;


            collision.transform.GetComponent<AutoDestroy>().life -= 1;
            collision.transform.GetComponent<AutoDestroy>().UpdateText();

         //   collision.transform.GetComponent<AutoDestroy>().SetBoxColor();

            SM.BodyParts.Remove(SM.BodyParts[0]);
        }
        else if (collision.transform.CompareTag("SimpleBox") && transform != SM.BodyParts[0])
        {
            Physics.IgnoreCollision(transform.GetComponent<Collider>(), collision.transform.GetComponent<Collider>());
            collision.transform.GetComponent<AutoDestroy>().dontMove = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (SM.BodyParts.Count > 0)
        {
            if (collision.transform.tag == "Food" && transform == SM.BodyParts[0])
            {
                for (int i = 0; i < collision.transform.GetComponent<FoodBehavior>().foodAmount; i++)
                {
                    SM.AddBodyPart();
                }

                Destroy(collision.transform.gameObject);
            }
        }

    }


}
