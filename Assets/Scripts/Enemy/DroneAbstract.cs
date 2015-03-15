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
		private static float terrainLeft, terrainRight, terrainTop, terrainBottom, terrainWidth, terrainLength;

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
			if(  (   previousPos.x > this.transform.position.x - 3 && previousPos.x < this.transform.position.x + 3
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
			Terrain terrain = EventHandler.Instance.terrain;;
			terrainLeft = terrain.transform.position.x;
			terrainBottom = terrain.transform.position.z;
			terrainWidth = terrain.terrainData.size.x;
			terrainLength = terrain.terrainData.size.z;
			terrainRight = terrainLeft + terrainWidth;
			terrainTop = terrainBottom + terrainLength;

			float randX, randZ;
			randX = Random.Range (terrainLeft, terrainRight);
			randZ = Random.Range (terrainBottom, terrainTop);

			Vector3 randomDirection = new Vector3 (randX,10,randZ); // 10 is erroneous
			NavMeshHit hit;
			NavMesh.SamplePosition (randomDirection, out hit, 500, 1);
			Vector3 finalPosition = hit.position;
			target = finalPosition;
		}
	

		public void moveRandomDirection()
		{
			if(transform.position.x < 20 && transform.position.z < 20)
			{
				generateCenterTarget();
			}
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

		public void generateCenterTarget()
		{
			Vector3 position = new Vector3 (EventHandler.Instance.spawners [2].transform.position.x, 10, EventHandler.instance.spawners [2].transform.position.z);
			NavMeshHit hit;
			NavMesh.SamplePosition (position, out hit, 500, 1);
			Vector3 finalPosition = hit.position;
			target = finalPosition;
		}

		public void moveTowardsPlayer()
		{
			moveTowards (player);
		}

		public virtual void getDamage(int dmg)
		{
			this.player.gameObject.GetComponent<Player> ().incRage (1);
			this.health -= dmg;
			if(health <= 0) // check if killed
			{
				destroySelf();
			}
		}

		public virtual void moveTowards(Transform movePlace)
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
			makeExplosion ();
			HiveMind.Instance.removeDrone(this); // remove drone from hivemind
			Destroy(this.gameObject);            // delete self
		}
	}
}

