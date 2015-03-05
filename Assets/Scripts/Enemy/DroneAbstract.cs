//using System;
using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public abstract class DroneAbstract : MonoBehaviour
	{
		public int health;
		public int moveSpeed;
		public int searchRange;
		
		protected int walkStop;  // distance till find new move random direction
		protected Vector3 target;
		protected Transform player;
		protected Vector3 bulletSpawn;

		public bool search ()
		{
			if(   player.position.x+searchRange > transform.position.x && player.position.x - searchRange < transform.position.x
			   && player.position.y+searchRange > transform.position.y && player.position.y - searchRange < transform.position.y
			   && player.position.z+searchRange > transform.position.z && player.position.z - searchRange < transform.position.z)
			{
				return true;
			}		
			return false;
		}

		public void generateNewTarget()
		{
			int walkRadius = 1000;
			Vector3 randomDirection = Random.insideUnitSphere * walkRadius;// will need to be changed
			NavMeshHit hit;
			NavMesh.SamplePosition (randomDirection, out hit, walkRadius, 1);
			Vector3 finalPosition = hit.position;
			target = finalPosition;
		}
	

		public void moveRandomDirection()
		{
			if(   target.x > transform.position.x - walkStop && target.x < transform.position.x + walkStop
			   && target.z > transform.position.z - walkStop && target.z < transform.position.z + walkStop)
			{
				generateNewTarget ();
			}
			
			
			if(this.GetComponent<NavMeshAgent>().enabled == false )//&& transform.position.y < 10
			{
				this.GetComponent<NavMeshAgent>().enabled = true;
				if(this.GetComponent<NavMeshAgent>().pathStatus != NavMeshPathStatus.PathComplete)
				{
					this.GetComponent<NavMeshAgent>().enabled = false;
				}
			}
			
			if(this.GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete)
			{
				this.GetComponent<NavMeshAgent>().SetDestination(this.target);
			}
			
		}

		public void moveTowardsPlayer()
		{
			moveTowards (player);
		}

		public void getDamage(int dmg)
		{
			print ("here");
			this.health -= dmg;
			if(health <= 0) // check if killed
			{
				HiveMind.Instance.removeDrone(this); // remove drone from hivemind
				Destroy(this.gameObject);            // delete self
			}
		}

		public void moveTowards(Transform movePlace)
		{
			if(transform.position.y < 10){ // if component is still in the air, don't find a path
				this.GetComponent<NavMeshAgent>().enabled = true; // enable navmesh agent
				if (this.GetComponent<NavMeshAgent> ().pathStatus == NavMeshPathStatus.PathComplete) 
				{ // if a path has been found, move. if not do nothing.
					this.GetComponent<NavMeshAgent> ().SetDestination (movePlace.position);
				}
			}
		}
	}
}

