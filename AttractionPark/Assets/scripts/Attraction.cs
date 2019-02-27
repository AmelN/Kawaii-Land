using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour {

    public float attractionDuration;
    public int capacity;
    public int queuecapacity;
    public Transform entry;
    public Transform exit;

    public Queue<GameObject> attractionQueue = new Queue<GameObject>();
    private Queue<GameObject> insideAttraction = new Queue<GameObject>();

    GameObject visitor;
   
    public bool CanAccess()
    {
        if(attractionQueue.Count == queuecapacity)
        {
            return false;
        }
        return true;
    }
   

    void Awake()
    {
        entry = this.gameObject.transform.GetChild(0);
        exit = this.gameObject.transform.GetChild(1);
    }

    void Update()
    {
        if (attractionCoroutine == null)
        {
            if (attractionQueue.Count > 0)
            {
                visitor = attractionQueue.Dequeue();
                //if (visitor.GetComponent<VisitorAttraction>().myAnimator == false)
                //{
                    visitor.SetActive(false);
                    insideAttraction.Enqueue(visitor);
                //}
                if (insideAttraction.Count == capacity)
                {
                    DoTheAttraction();
                }
            }
            else
            {
                if (attractionQueue.Count < 1 && insideAttraction.Count >0)
                {
                    DoTheAttraction();
                }
            }
        }
    }

    private void DoTheAttraction()
    {
        attractionCoroutine = StartCoroutine(InAttraction());
    }

    Coroutine attractionCoroutine = null;

    IEnumerator InAttraction()
    {
        yield return new WaitForSeconds(attractionDuration);

        if (insideAttraction.Count > 0)
        {
            GameObject visitorInAttraction = insideAttraction.Dequeue();
            visitorInAttraction.transform.position = exit.position;
            visitorInAttraction.SetActive(true);
            visitorInAttraction.GetComponent<VisitorAttraction>().FindRandomPos();
        }
        else
        {
            attractionCoroutine = null;
        }
    }

    public void ResetQueue(Queue<GameObject> attractionQueue)
    {

        GameObject[] tempList = new GameObject[attractionQueue.Count];
        attractionQueue.CopyTo(tempList, 0);

        for (int i = 0; i < tempList.Length; ++i)
        {
            VisitorAttraction Visitor;
            Visitor = tempList[i].transform.GetComponent<VisitorAttraction>();
            Visitor.agent.destination = new Vector3(-(float)i / 2, 0);
        }
    }
}

