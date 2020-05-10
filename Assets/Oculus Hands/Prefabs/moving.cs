using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class moving : MonoBehaviour
{
    public Rigidbody rb;
    public bool move;
    float v;
    float limit;
    // Start is called before the first frame update
    void Start()
    {
        move = true;
        v = -0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        rb.velocity = new Vector3(0, 0, -10f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (limit < 3 && collision.gameObject.layer == 2)
        {
            Debug.Log(collision.gameObject);

            move = !move;
            SlicedHull hull = SliceObject(collision.gameObject, null);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            if (hull != null)
            {
                //GameObject bottom = hull.CreateLowerHull(gameObject, null);
                GameObject top = hull.CreateUpperHull(gameObject, null);
                //AddHullComponents(bottom);
                AddHullComponents(top);
            }
            //gameObject.transform.parent = collision.gameObject.transform;
            limit++;
        }
    }

    public void AddHullComponents(GameObject go)

    {

        Rigidbody rb = go.AddComponent<Rigidbody>();

        rb.interpolation = RigidbodyInterpolation.Interpolate;

        MeshCollider collider = go.AddComponent<MeshCollider>();

        collider.convex = true;



        rb.AddExplosionForce(100, go.transform.position, 20);

    }

    public SlicedHull SliceObject(GameObject obj, Material cm = null)
    {
        return gameObject.Slice(obj.transform.position, obj.transform.up, cm);
    }
}
