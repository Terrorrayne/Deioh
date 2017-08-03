using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DummyAI_shieldofhonor : MonoBehaviour
{

	[Tooltip("distance from player that enemy will stop chasing and go idle")]
	public float idleDistance = 20f;
	[Tooltip("max dist from player that enemy will try to shoot him")]
	public float engagedDistance = 10f;
	[Tooltip("enemy will try to position itsself this far away from player while shooting")]
	public float comfyShootingDist = 5f;

	public LayerMask layerMask;

	public float moveSpeed = 10f;
	public float repositionMoveSpeed = 15f;
	[Tooltip("Time between shots while burst firing")]
	public float burstSpacing = 0.2f;
	[Tooltip("Max time spent in one position shooting")]
	public float shootingTimeLimit = 3f;
	float shootingTimer = 0f;

	public enum MoveState
	{
		idle,
		chasing,
		shooting,
		reposition,
	}


	public MoveState moveState;

	public GameObject THINGY;

	float _idleDist;
	float _engagedDist;
	float _comfyShootingDist;

	bool _shooting = false;
	bool _repositioning = false;

	Vector3 _dir;
	RaycastHit _hit;
	bool _result;

	public float stunTimer = 0;

	NavMeshAgent agent;
	DummyShooting gun;
	DummyHealth health;

	public Transform target; // usually the player

	// Use this for initialization
	void Start()
	{
		// cache squared distances
		// so we can use SqrMagnitude instead of Magnitude when checking distances
		_idleDist = idleDistance * idleDistance;
		_engagedDist = engagedDistance * engagedDistance;
		_comfyShootingDist = comfyShootingDist * comfyShootingDist;

		moveState = MoveState.idle;

		agent = GetComponent<NavMeshAgent>();
		gun = GetComponent<DummyShooting>();
		health = GetComponent<DummyHealth>();
	}

	// Update is called once per frame
	void Update()
	{
		if (stunTimer < 0) // can only do anything if you arn't stunned
		{
			switch (moveState)
			{
				case MoveState.idle:
					agent.destination = transform.position;

					// check to see if player is anywhere near us (idleDistance)
					// if he is, change to chasing

					_dir = target.position - transform.position;
					if (Vector3.SqrMagnitude(_dir) < _idleDist)
					{
						moveState = MoveState.chasing;
					}

					break;
				case MoveState.chasing:
					// todo: chasing
					agent.destination = target.position;
					agent.speed = 20f;

					_dir = target.position - transform.position;


					// are we within a certain radius of him? (engagedDistance)
					float MYDIST = Vector3.SqrMagnitude(_dir);
					if (MYDIST < _engagedDist)
					{
						// if we can consider switching to shooting/repositioning cycle
						// can we see him (raycast)?
						_result = false;
						if (Physics.Raycast(transform.position, _dir, out _hit, engagedDistance, layerMask))
						{
							_result = _hit.collider.CompareTag("Player"); // IDK
						}
						if (_result)
						{
							moveState = MoveState.shooting;
							shootingTimer = 0; // prob a bad time to mention it but you need to do this every time you switch to Movestate.shooting
						}
					}
					if (MYDIST > _idleDist)
					{
						// go idle
						moveState = MoveState.idle;
					}




					break;
				case MoveState.shooting:
					// shoot a burst of like, 3 bullets

					// if we can still see player consider shooting another burst at him

					// we don't like staying still for long, if we over do our limit go to reposition

					agent.destination = transform.position;
					if (!_shooting) // if we arn't shooting
					{
						// check distance to see if we're in engaged space
						_dir = target.position - transform.position;
						if (Vector3.SqrMagnitude(_dir) < _engagedDist)
						{
							if (shootingTimer < shootingTimeLimit) // we get restless if we stay in the same position too long
							{
								// can see player?

								_result = false;
								if (Physics.Raycast(transform.position, _dir, out _hit, engagedDistance, layerMask))
								{
									_result = _hit.collider.CompareTag("Player"); // IDK
								}

								// shoot again
								if (_result)
								{
									StartCoroutine(ShootBurst());

								}


							}
							else
							{
								// go to reposition
								moveState = MoveState.reposition;
								_repositioning = false;
							}
						}
						else
						{
							moveState = MoveState.chasing;
						}


					}
					shootingTimer += Time.deltaTime;



					// if not wait and shoot him if you see him

					// if you've waited too long reposition


					break;
				case MoveState.reposition:
					// can we see him?
					// if not we might want to switch to chasing
					// switch to chasing if he's beyond a certain radius(engaged distance)

					//else... choose a new point to move to
					// this point should give us line of sight to him
					// should also be at least a certain distance away from him
					if (!_repositioning)
					{
						_repositioning = true;
						StartCoroutine(Reposition());
					}



					// congrats we now have a point that we can move to fuck me this is too much work for this fucking dumbass game



					//GameObject go = Instantiate(THINGY, point, Quaternion.identity) as GameObject;
					//Destroy(go, 2f);

					break;
			}

		}
		else
		{
			stunTimer -= Time.deltaTime;
			agent.destination = transform.position;
		}

	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position, idleDistance);
		Gizmos.DrawWireSphere(transform.position, engagedDistance);
		Gizmos.DrawWireSphere(transform.position, comfyShootingDist);

	}

	IEnumerator ShootBurst()
	{
		_shooting = true;
		yield return new WaitForSeconds(burstSpacing * 5);

		Vector3 targetPos = target.position;

		gun.SHOOT(0.1f, targetPos);
		yield return new WaitForSeconds(burstSpacing);
		gun.SHOOT(0.25f, targetPos);
		yield return new WaitForSeconds(burstSpacing);
		gun.SHOOT(0.4f, targetPos);
		yield return new WaitForSeconds(burstSpacing);
		//gun.SHOOT(0.4f, targetPos);
		_shooting = false; // we are done shooting
	}

	void Shoot()
	{

	}

	Vector3 GetRepositionPoint()
	{
		Vector3 point = Vector3.zero;
		bool comfyDistTest = true;
		bool lineOfSightTest = true;
		bool overlapTest = false;

		do
		{
			point = Random.insideUnitCircle * 6; // random point * max move dist(must be greater than comfydist)
			point = new Vector3(transform.position.x + point.x, transform.position.y, transform.position.z + point.y);
			// we now have a real world point to test

			comfyDistTest = true;
			lineOfSightTest = true;
			overlapTest = false;

			_dir = target.position - point;

			// gotta be a certain dist away from player
			comfyDistTest = Vector3.SqrMagnitude(_dir) < _comfyShootingDist;
			// have to be able to see player from this position
			if (Physics.Raycast(point, _dir, out _hit, 12, layerMask))
			{
				lineOfSightTest = !_hit.collider.CompareTag("Player");
			}
			// don't want to be inside of anything
			if (Physics.Linecast(transform.position, point, layerMask))
			{
				overlapTest = true;
			}

		}
		while (comfyDistTest || lineOfSightTest || overlapTest);

		return point;
	}

	IEnumerator Reposition()
	{
		Vector3 newPoint = GetRepositionPoint();
		while (Vector3.SqrMagnitude(transform.position - newPoint) > 1f)
		{
			// move
			agent.destination = newPoint;
			agent.speed = 50f;
			yield return 0;
		}
		agent.destination = transform.position;
		moveState = MoveState.shooting;
		shootingTimer = 0;
	}

	public void ExitStunned()
	{
		moveState = MoveState.reposition;
		_repositioning = false;
	}
}
