using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject norm;
    public GameObject note;
    public GameObject[] notes;
    public Conductor cd;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnNorm", 0f, 2f);
        //InvokeRepeating("SpawnNote", 0f, 0.5f);
    }
    public void SpawnNorm()
    {
        norm.GetComponentInChildren<Note>().velocity = cd.GetVelocity();
        Instantiate(norm, transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(2);
        //StartCoroutine(SpawnNorm());
    }
    public void SpawnNote(int[] note)
    {

            for(int i = 0; i < note.Length; i++)
            {
                if(note[i] != -1)
                {
                GameObject n = notes[note[i]];
                n.GetComponent<Note>().velocity = cd.GetVelocity();
                //n.GetComponent<Note>().velocity = 2f;
                n.GetComponent<SpriteRenderer>().sortingOrder = -i;
                Instantiate(n, transform.position + new Vector3(i*MainValue.Instance.crrSecPerBeat* cd.GetVelocity()/2, 0,0), Quaternion.identity);
                }
            }
    }
        public void SpawnNoteX(int idx, int orderInLayer)
    {
        //int idx = Random.Range(0, 4);
        //Debug.Log(idx);
        GameObject n = notes[idx];
        //may have to change this to singleton
        n.GetComponent<Note>().velocity = cd.GetVelocity();
        //n.GetComponent<Note>().velocity = 2f;
        n.GetComponent<SpriteRenderer>().sortingOrder = -orderInLayer;
        Instantiate(n, transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(0.5f);
        //StartCoroutine(SpawnNote());
    }

    public void SpawnNotek()
    {
        int idx = Random.Range(0, 4);
        //Debug.Log(idx);
        GameObject n = notes[idx];
        //may have to change this to singleton
        n.GetComponent<Note>().velocity = cd.GetVelocity();
        //n.GetComponent<Note>().velocity = 2f;
        Instantiate(n, transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(0.5f);
        //StartCoroutine(SpawnNote());
    }
}
