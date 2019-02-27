using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visitors : MonoBehaviour {

    public GameObject attractionvisitor;
    public GameObject parcvisitor;
    public Transform spawnTransform;

    public int numberOfVisitors;
    private int i=0;

	void Start () {
        StartCoroutine("SpawnCats");
    }

    /*
     * Couroutine that waits before spawning 2 new visitors until it reached the number of visitors
     * A visitor can be just walking on the park or using the attractions, it's chosen randomly
    */
    IEnumerator SpawnCats()
    {
        while (true)
        {
            if (i < numberOfVisitors / 2)
            {
                int offset = 0;
                for(int i = 0; i<2; ++i)
                {
                    int rand = Random.Range(0, 2);

                    if(rand == 0)
                    {
                        Instantiate(attractionvisitor, spawnTransform.position + new Vector3(offset, 0, 0), attractionvisitor.transform.rotation);
                    }
                    else
                    {
                        Instantiate(parcvisitor, spawnTransform.position + new Vector3(offset, 0, 0), parcvisitor.transform.rotation);
                    }
                    offset += 10;
                }
                yield return new WaitForSeconds(3);
                ++i;
            }
            else break;
        }
    }
}
