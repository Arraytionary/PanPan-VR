using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.Interaction.Toolkit
{

    public class Drum : MonoBehaviour
    {
        public ActionProcesser ap;
        public string side;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Stick" && Mathf.Abs(collision.relativeVelocity.y) >= 2f)
                
                //collision.gameObject.GetComponent<XRGrabInteractable>().movementType = XRBaseInteractable.MovementType.VelocityTracking;
            {
                collision.gameObject.GetComponent<Stick>().CoolDown(Time.time);
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                //FixedJoint joint = gameObject.AddComponent<FixedJoint>();
                //ContactPoint contact = collision.contacts[0];
                //joint.anchor = transform.InverseTransformPoint(contact.point);
                //joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponent<Rigidbody>();

                //joint.breakForce = 20;
                //joint.breakTorque = 20;

                // Stops objects from continuing to collide and creating more joints
                //joint.enableCollision = false;

                switch (side)
                {
                    case "right":
                        ap.HitRI();
                        break;
                    case "left":
                        ap.HitLI();
                        break;
                    case "oleft":
                        ap.HitLO();
                        break;
                    case "oright":
                        ap.HitRO();
                        break;
                    default:
                        break;
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Stick")
                other.GetComponent<XRGrabInteractable>().movementType = XRBaseInteractable.MovementType.VelocityTracking;
            {
                switch (side)
                {
                    case "right":
                        ap.HitRI();
                        break;
                    case "left":
                        ap.HitLI();
                        break;
                    case "oleft":
                        ap.HitLO();
                        break;
                    case "oright":
                        ap.HitRO();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
