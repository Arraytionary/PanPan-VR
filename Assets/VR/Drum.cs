using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.Interaction.Toolkit
{

    public class Drum : MonoBehaviour
    {
        public ActionProcesser ap;
        public string side;
        public SoundFeedback sound;
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
            if (collision.gameObject.tag == "Stick" && Mathf.Abs(collision.relativeVelocity.y) >= 0.5f)
                
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
                        if(Time.time - MainValue.Instance.FloatValue["right"] > 0.15f)
                        {
                            sound.playSound(0);
                            ap.HitRI();
                            MainValue.Instance.FloatValue["right"] = Time.time;
                        }

                        break;
                    case "left":
                        if (Time.time - MainValue.Instance.FloatValue["left"] > 0.15f)
                        {
                            sound.playSound(0);
                            ap.HitLI();
                            MainValue.Instance.FloatValue["left"] = Time.time;
                        }
                        break;
                    case "oleft":
                        if (Time.time - MainValue.Instance.FloatValue["left"] > 0.15f)
                        {
                            sound.playSound(1);
                            ap.HitLO();
                            MainValue.Instance.FloatValue["left"] = Time.time;
                        }
                        break;
                    case "oright":
                        if (Time.time - MainValue.Instance.FloatValue["right"] > 0.15f)
                        {
                            sound.playSound(1);
                            ap.HitRO();
                            MainValue.Instance.FloatValue["right"] = Time.time;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.tag == "Stick")
        //        other.GetComponent<XRGrabInteractable>().movementType = XRBaseInteractable.MovementType.VelocityTracking;
        //    {
        //        switch (side)
        //        {
        //            case "right":
        //                ap.HitRI();
        //                break;
        //            case "left":
        //                ap.HitLI();
        //                break;
        //            case "oleft":
        //                ap.HitLO();
        //                break;
        //            case "oright":
        //                ap.HitRO();
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}
    }
}
