using UnityEngine;
using UnityEngine.AI;

public class VisitorAttraction : MonoBehaviour {
	public GameObject[] attractions;
	int randomDestinationIndex;
    public Animator myAnimator;

    //this is just for test purpose
    public string state; 
    public string walkingTo;


    Attraction myAttraction;
    public NavMeshAgent agent;


    void Start () {
		agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        randomDestinationIndex = Random.Range(0, attractions.Length);
        FindRandomPos();
    }
	
	void Update () {
		// Check if attraction reached
		if (!agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
                myAnimator.enabled = false;
                state = "Waiting";
            }
        }
	}
    public void FindRandomPos() {
        state = "Walking";
        
        randomDestinationIndex = Random.Range(0, attractions.Length);
        myAttraction = attractions[randomDestinationIndex].GetComponent<Attraction>();
        if(myAttraction.CanAccess() == false)
        {
            FindRandomPos();
        }
        else
        {
            walkingTo = attractions[randomDestinationIndex].name;
            Vector3 EntryPosition = myAttraction.entry.position;
            int orderOnQueue = myAttraction.attractionQueue.Count;
            float offset = 15f;
            agent.destination = EntryPosition + new Vector3(offset * orderOnQueue+1, 0f, 0f);
            myAnimator.enabled = true;
            //add visitor to attraction queue
            myAttraction.attractionQueue.Enqueue(this.gameObject);
        }
    }
 
}
