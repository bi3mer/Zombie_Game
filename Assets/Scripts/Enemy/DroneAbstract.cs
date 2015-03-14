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

		private Vector3 previousPos;

		public void addToHive()
		{
			HiveMind.Instance.addDrone (this);		
		}

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

		public void updatePreviousPosition()
		{
			this.previousPos = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);	
		}

		public void checkSelf()
		{
			this.updatePreviousPosition ();
			InvokeRepeating ("checkPos", 10, 10);		
		}

		public void checkPos()
		{
			if(  (previousPos.x > this.transform.position.x - 3 && previousPos.x < this.transform.position.x + 3
			      && previousPos.y > this.transform.position.y - 3 && previousPos.y < this.transform.position.y + 3
			      && previousPos.z > this.transform.position.z - 3 && previousPos.z < this.transform.position.z + 3)
			      || transform.position.y < -100) //add z check later
			{
				makeExplosion();
				destroySelf();
			}
			this.updatePreviousPosition ();
		}

		public void generateNewTarget()
		{
			int walkRadius = 500;
			Vector3 randomDirection = Random.insideUnitSphere * walkRadius;// will need to be changed
			NavMeshHit hit;
			NavMesh.SamplePosition (randomDirection, out hit, walkRadius, 1);
			Vector3 finalPosition = hit.position;
			target = finalPosition;
		}
	

		public void moveRandomDirection()
		{
			if(   target.x > transform.position.x - walkStop && target.x < transform.position.x + walkStop
			   && target.y > transform.position.y - walkStop && target.y < transform.position.y + walkStop
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

		public virtual void getDamage(int dmg)
		{
			this.health -= dmg;
			if(health <= 0) // check if killed
			{
				makeExplosion();
				destroySelf();
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

		public virtual void makeExplosion()
		{
			Vector3 explosionPosition = new Vector3 (this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
			Instantiate(EventHandler.Instance.tinyExplosion,explosionPosition,Quaternion.identity);
		}

		public void destroySelf()
		{
			HiveMind.Instance.removeDrone(this); // remove drone from hivemind
			Destroy(this.gameObject);            // delete self
		}
	}
}

