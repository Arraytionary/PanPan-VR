using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insertCoin : MonoBehaviour
{
    AudioSource aS;
    public AudioClip insert;
    public delegate void PublishEvent();
    public static event PublishEvent inserted;
    public GameObject holePos;
    // Start is called before the first frame update
    void Start()
    {
        aS = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "coin")
    //    {
    //        aS.PlayOneShot(insert);
    //        Destroy(collision.gameObject);
    //        if (inserted != null)
    //        {
    //            inserted();
    //        }
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            other.gameObject.transform.position = holePos.transform.position;
            other.transform.rotation = holePos.transform.rotation;
            StartCoroutine(StartGame());
            other.gameObject.GetComponent<Rigidbody>().AddForce(0, 0, 4);
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3( Mathf.PI * 2, 0, 0);
            
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.7f);
        aS.PlayOneShot(insert);
        if (inserted != null)
        {
            inserted();
        }
    }
}
