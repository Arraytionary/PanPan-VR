using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ActionProcesser : MonoBehaviour
{
    //Input action
    DefaultControl inputAction;

    public Animator animatorRI;
    public Animator animatorRO;
    public Animator animatorLI;
    public Animator animatorLO;

    public Animator hitAccuracy;
    public GameObject[] hitParticles; 

    GameObject note;

    bool entered;

    bool lI;
    bool lO;
    bool rI;
    bool rO;

    //last pressed time
    float lastLI;
    float lastLO;
    float lastRI;
    float lastRO;

    public float allowDuration;

    Utility.Projectile projectile;
    public GameObject target;

    AudioSource audioSource;
    public AudioClip center;
    public AudioClip rim;

    public GameObject hitText;

    public ScoringSystem scoringSystem;

    int passed;

    public Transform register;
    public GameObject inner;
    public GameObject outer;

    Queue<GameObject> enteredQueue;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        inputAction = new DefaultControl();

        Drum.rightInner += HitRI;
        Drum.rightOuter += HitRO;
        Drum.leftInner += HitLI;
        Drum.leftOuter += HitLO;

        inputAction.Gameplay.rightInner.performed += ctx => HitRI();
        inputAction.Gameplay.rightOuter.performed += ctx => HitRO();
        inputAction.Gameplay.leftInner.performed += ctx => HitLI();
        inputAction.Gameplay.leftOuter.performed += ctx => HitLO();
        //inputAction.Gameplay.DoubleInner.performed += ctx => HitQuality(note.GetComponent<Note>().collideTime);

        lI = false;
        lO = false;
        rI = false;
        rO = false;

        projectile = new Utility.Projectile(target.transform, 1.5f);
        Utility.ResetScore();
        enteredQueue = new Queue<GameObject>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9 || collision.gameObject.layer == 11)
        {
            entered = true;
            collision.gameObject.GetComponent<Note>().StampTime();
            enteredQueue.Enqueue(collision.gameObject);
            note = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (enteredQueue.Count != 0)
        {
            enteredQueue.Dequeue();
            entered = enteredQueue.Count != 0;
        }

        //if (collision.gameObject.layer == 9 || collision.gameObject.layer == 11)
        //{
        //    if (passed > 1) { passed--; }
        //    else entered = false;
        //}
    }

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("hmmmmm");
    //    if (rI)
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}

    // Update is called once per frame

    public void HitQuality(float time)
    {
        Debug.Log("quality measured got called");
        //goodHit
        if (Time.time - time <= MainValue.Instance.crrSecPerBeat / 3)
        {
            hitText.GetComponent<TextMeshPro>().text = "GOOD";
            hitText.GetComponent<Animator>().SetTrigger("good");
            hitAccuracy.SetTrigger("Good");
            Instantiate(hitParticles[0], hitAccuracy.transform.position, Quaternion.identity);
            scoringSystem.SubmitScore(1f);
            MainValue.Instance.good += 1;
        }
        else {
            hitText.GetComponent<TextMeshPro>().text = "OK";
            hitText.GetComponent<Animator>().SetTrigger("ok");
            hitAccuracy.SetTrigger("Ok");
            Instantiate(hitParticles[1], hitAccuracy.transform.position, Quaternion.identity);
            scoringSystem.SubmitScore(0.5f);
            MainValue.Instance.ok += 1;
        }
        
    }

    public void ReportMiss(GameObject noteToDesTroy)
    {
        if(note == noteToDesTroy)
        {
            entered = false;
        }
        hitText.GetComponent<TextMeshPro>().text = "BAD";
        hitText.GetComponent<Animator>().SetTrigger("bad");
        scoringSystem.SubmitScore(-1f);
        MainValue.Instance.bad += 1;
    }
    //TODO implement method to give a score to drum roll note
    int throwed;
    public void HitRI()
    {

        //audioSource.PlayOneShot(center);
        //animator.SetTrigger("pressed");
        PlayAnim(animatorRI);
        lastRI = Time.time;
        if (entered && note.GetComponent<Note>().type.Equals("0"))
        {
            Debug.Log("before call for measurement");
            HitQuality(note.GetComponent<Note>().collideTime);
            //note.GetComponent<Note>().Decrease();
            projectile.Throw(note.transform, note);
        }
        if (entered && note.GetComponent<Note>().type.Equals("2") && Time.time - lastLI <= allowDuration)
        {
            HitQuality(note.GetComponent<Note>().collideTime);
            projectile.Throw(note.transform, note);
        }
        if (entered && note.GetComponent<Note>().type.Equals("4"))
        {
            //HitQuality(note.GetComponent<Note>().collideTime);
            //GameObject toThrow = inner;
            //toThrow.GetComponent<Note>().enabled = false;
            //toThrow.GetComponent<SpriteRenderer>().sortingOrder = 8;
            StartCoroutine(spr());
        }
        //Debug.Log("right inner");
        //if (!ctx.performed)
        //    return;
        //animator.SetBool("Pressed", false);
    }
    IEnumerator spr()
    {
        yield return new WaitForSeconds(0.07f);
        GameObject toThrow = Instantiate(inner, register.position, Quaternion.identity);
        Debug.Log(note);
        projectile.Throw(toThrow.transform, toThrow);

    }
    public void HitRO()
    {

        //audioSource.PlayOneShot(rim);
        PlayAnim(animatorRO);
        lastRO = Time.time;
        if (entered && note.GetComponent<Note>().type.Equals("1"))
        {
            //note.GetComponent<Note>().Decrease();
            HitQuality(note.GetComponent<Note>().collideTime);
            projectile.Throw(note.transform, note);
        }
        if (entered && note.GetComponent<Note>().type.Equals("3") && Time.time - lastLO <= allowDuration)
        {
            HitQuality(note.GetComponent<Note>().collideTime);
            projectile.Throw(note.transform, note);
        }
        if (entered && note.GetComponent<Note>().type.Equals("4"))
        {
            HitQuality(note.GetComponent<Note>().collideTime);
            GameObject toThrow = Instantiate(outer, register.position, Quaternion.identity);
            toThrow.GetComponent<SpriteRenderer>().sortingOrder = 8;
            projectile.Throw(toThrow.transform, toThrow);
        }
        //Debug.Log("right outer");
    }

    public void HitLI()
    {
  
        //audioSource.PlayOneShot(center);
        PlayAnim(animatorLI);
        lastLI = Time.time;
        if (entered && note.GetComponent<Note>().type.Equals("0"))
        {
            //note.GetComponent<Note>().Decrease();
            HitQuality(note.GetComponent<Note>().collideTime);
            projectile.Throw(note.transform, note);
        }
        if (entered && note.GetComponent<Note>().type.Equals("2") && Time.time - lastRI <= allowDuration)
        {
            HitQuality(note.GetComponent<Note>().collideTime);
            projectile.Throw(note.transform, note);
        }
        if (entered && note.GetComponent<Note>().type.Equals("4"))
        {
            HitQuality(note.GetComponent<Note>().collideTime);
            GameObject toThrow = Instantiate(inner, register.position, Quaternion.identity);
            toThrow.GetComponent<SpriteRenderer>().sortingOrder = 8;
            projectile.Throw(toThrow.transform, toThrow);
        }

        //Debug.Log("left inner");
    }

    public void HitLO()
    {

        //audioSource.PlayOneShot(rim);
        PlayAnim(animatorLO);
        lastLO = Time.time;
        if (entered && note.GetComponent<Note>().type.Equals("1"))
        {
            //note.GetComponent<Note>().Decrease();
            HitQuality(note.GetComponent<Note>().collideTime);
            projectile.Throw(note.transform, note);
        }
        if (entered && note.GetComponent<Note>().type.Equals("3") && Time.time - lastRO <= allowDuration)
        {
            HitQuality(note.GetComponent<Note>().collideTime);
            projectile.Throw(note.transform, note);
        }
        if (entered && note.GetComponent<Note>().type.Equals("4"))
        {
            HitQuality(note.GetComponent<Note>().collideTime);
            GameObject toThrow = Instantiate(outer, register.position, Quaternion.identity);
            toThrow.GetComponent<SpriteRenderer>().sortingOrder = 8;
            projectile.Throw(toThrow.transform, toThrow);
        }
        //Debug.Log("left outer");
    }

    private void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Hitted");
    }

    IEnumerator Trigger(bool position)
    {
        position = true;
        yield return new WaitForSeconds(0.2f);
        position = false;
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnFreporty()
    {
        inputAction.Disable();
        Drum.rightInner -= HitRI;
        Drum.rightOuter -= HitRO;
        Drum.leftInner -= HitLI;
        Drum.leftOuter -= HitLO;
    }
}
