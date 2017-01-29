using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatField : InteractableTile {

    public List<Sprite> stageSprites = new List<Sprite>();
    public List<GameObject> dropList = new List<GameObject>();

    public int maxStage = 3;
    public bool grown;
    public SpriteRenderer sprRender;

    public int growingStage = 0;
    public bool growing;

    void Start()
    {
        sprRender = GetComponent<SpriteRenderer>();

        CheckStage();
    }

    public override void Break(GameObject source)
    {
        base.Break(source);

        if (grown)
        {
            growingStage = 0;
            sprRender.sprite = stageSprites[growingStage];

            foreach(GameObject drop in dropList)
            {
                GameObject item = Instantiate(drop, transform.position, Quaternion.identity);
            }

            grown = false;

            CheckStage();
        }
    }

    void Update()
    {
        //if (!growing)
        //    Timer(5f);

    }

    public void CheckStage()
    {
        sprRender.sprite = stageSprites[growingStage];
        if (growingStage == maxStage)
        {
            grown = true;
        }
        else
        {
            StartCoroutine(Timer(5f));
        }

    }

    IEnumerator Timer(float time)
    {
        Debug.Log("StartTimer");
        yield return new WaitForSeconds(time);
        growingStage += 1;
        CheckStage();
    }
}
