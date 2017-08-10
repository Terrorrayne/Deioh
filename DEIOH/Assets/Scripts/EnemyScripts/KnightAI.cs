using UnityEngine;
using UnityEngine.AI;

public class KnightAI : MonoBehaviour
{
	public float sightDistance; // how far can we see
	public float stalkDistance; // how close are we once we start planning to attack
	public float slowWalkSpeed = 2f;
	public float fastWalkSpeed = 5f;

	public InventoryItem primaryItem;
	public InventoryItem secondaryItem;

	EnemyHealth myHealth;
	NavMeshAgent myAgent;

	public enum EnemyMindState
	{
		patrolling, engaged, scared, searching, idle
	}

	public enum EnemyEngagedState
	{
		chasing, stalking, attacking
	}

	Vector3 _dir;

	EnemyMindState myMind;
	EnemyEngagedState myEngagement;

	private void Awake()
	{
		myHealth = GetComponent<EnemyHealth>();
		myAgent = GetComponent<NavMeshAgent>();
	}

	void Start()
	{
		myMind = EnemyMindState.idle;
	}

	// Update is called once per frame
	void Update()
	{
		switch (myMind)
		{
			case EnemyMindState.patrolling:
				// we don't have a path to follow, so we will wander around in this general area until we see something

				break;
			case EnemyMindState.engaged:
				_dir = DeiohGame.self.player.transform.position - transform.position;

				EngagedUpdate(_dir, Vector3.Magnitude(_dir), true);
				break;
			case EnemyMindState.scared:
				// we have seen an enemy, its fucking scary, lets run away till we never see it again

				break;
			case EnemyMindState.searching:
				// we have seen an enemy, the bitch ran away. where the fuck he go?

				break;
			case EnemyMindState.idle:
				// i feel nothing. i am nothing.
				_dir = DeiohGame.self.player.transform.position - transform.position;
				if (Vector3.Magnitude(_dir) < sightDistance)
				{
					myMind = EnemyMindState.engaged;
				}
				break;

		}
	}

	void EngagedUpdate(Vector3 dir, float dist, bool cansee)
	{
		// we see an enemy, and we are trying to attack it
		// move toward it, rotation follows movement
		myAgent.destination = DeiohGame.self.player.transform.position;
		// once we get within a certain range. begin holding up our shield, stalking
		// that is, if we have a shield, if we don't have one:
		// check our HP, can we take a hit? if we can, try lunging the backing off

		if (myAgent.remainingDistance < stalkDistance)
		{
			// stalking
			// set our speed to something slow
			myAgent.speed = slowWalkSpeed;
			//myAgent.radius = 1.3f;

			if (dist < 2f)
			{
				myAgent.destination = transform.position;
			}
		}
		else
		{
			// chasing
			// set our speed to something faster
			myAgent.speed = fastWalkSpeed;
			//myAgent.radius = 0.5f;
		}

		//switch (myEngagement)
		//{
		//	case EnemyEngagedState.chasing:
		//		break;
		//	case EnemyEngagedState.stalking:
		//		break;
		//	case EnemyEngagedState.attacking:
		//		break;
		//}

	}
}
