using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

public class Utility : MonoBehaviour
{
    public class Projectile
    {

        public Transform target;
        public Transform throwingPoint;
        public GameObject obj;
        public float timeTilHit;

        public Projectile(Transform tar, float time)
        {
            target = tar;
            timeTilHit = time;
        }
        public void Throw(Transform thr, GameObject ob)
        {
            throwingPoint = thr;
            obj = ob;

            float xDis = target.position.x - throwingPoint.position.x;
            float yDis = target.position.y - throwingPoint.position.y;
            float throwAngle = Mathf.Atan((yDis + 4.905f) / xDis);

            float totalV = xDis / Mathf.Cos(throwAngle);
            float xV = totalV * Mathf.Cos(throwAngle);
            float yV = totalV * Mathf.Sin(throwAngle);

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1f;
            rb.velocity = new Vector2(xV, yV);
        }
    }

    public static void ResetScore()
    {
        MainValue.Instance.score = 0;
        MainValue.Instance.maxCombo = 0;
        MainValue.Instance.combo = 0;
        MainValue.Instance.good = 0;
        MainValue.Instance.ok = 0;
        MainValue.Instance.bad = 0;
    }

    public static void RequestBadge(string label)
    {
        MainValue.Instance.requestedBadge = true;
        MainValue.Instance.toDisplayOnBadge = label;
    }

    public static void HideBadge()
    {
        MainValue.Instance.requestedBadge = false;
    }


    public static void Save()
    {
        
        string saveJson = JsonConvert.SerializeObject(MainValue.Instance.saveObjects);
        SaveSystem.SaveGame(saveJson);
    }

    public static void Load()
    {
        string saveJson = SaveSystem.LoadGame();
        if (saveJson != null)
        {
            MainValue.Instance.saveObjects = JsonConvert.DeserializeObject<Dictionary<string, SaveObject>>(saveJson);

        }
        else MainValue.Instance.saveObjects =  new Dictionary<string, SaveObject>();
    }

    public static List<Song> LoadList()
    {
        string songList = SaveSystem.LoadSongList();
        return JsonConvert.DeserializeObject<List<Song>>(songList);
    }

    public static IEnumerator LoadSong(string fileName, AudioSource audioSource)
    {
        return SaveSystem.LoadSong(fileName, audioSource);
    }
}
