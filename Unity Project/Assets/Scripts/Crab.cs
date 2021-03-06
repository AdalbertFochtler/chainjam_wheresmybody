﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crab : MonoBehaviour {
	
	public ChainJam.PLAYER playerID;	// Useful to remember which player it is :)
	
	public float speed;					// movement speed
	
	public List<LegSlot> legSlots;		// list of leg slots
	public GameObject claws;	
	
	public Transform leftPoint;
	public Transform rightPoint;
	
	public bool isDead; 				// whether the crab is dead
	
	public float attackThreshold;		// max. distance for attack
	
	int x;
	
	void Start () {
		isDead = false;
		foreach(LegSlot slot in legSlots)
			slot.leg.SetActive(true);
		claws.SetActive(true);
		
		x=0;
	}
	
	void Update () {
		if(!isDead)
		{
			if(ChainJam.GetButtonPressed(playerID, ChainJam.BUTTON.A))
			{
				transform.Rotate(new Vector3(0,0,-1f));
			}
			if(ChainJam.GetButtonPressed(playerID, ChainJam.BUTTON.B))
			{
				transform.Rotate(new Vector3(0,0,1f));
			}
			
			if(ChainJam.GetButtonPressed(playerID, ChainJam.BUTTON.LEFT))
			{
				transform.Translate(Vector3.right * speed);
			}
				
			if(ChainJam.GetButtonPressed(playerID, ChainJam.BUTTON.RIGHT))
			{
				transform.Translate(Vector3.left * speed);
			}
			
			if(ChainJam.GetButtonPressed(playerID, ChainJam.BUTTON.DOWN))
			{
				;
			}
			
			if(ChainJam.GetButtonJustPressed(playerID, ChainJam.BUTTON.UP))
			{
				foreach(GameObject go in GameObject.FindGameObjectsWithTag("crab"))
				{
					Crab enemy = go.GetComponent<Crab>();
					if(CanAttack(enemy))
					{
						Attack(enemy);
						Debug.Log ("KILL " + x.ToString());
						x++;
						break;
					}
				}
			}
		}
	}
	
	private bool CanAttack(Crab enemy)
	{
		return (enemy != this 
			&& Vector3.Distance(transform.position, enemy.transform.position)<attackThreshold);
	}
	
	/**
	 * execute a single claw attack on another crab.
	 * if possible, remove a leg.
	 * otherwise, if possible, remove the claws.
	 * otherwise, kill.
	 */
	public void Attack(Crab enemy)
	{
		// TODO: enemy has to be close to me
		if(claws.activeSelf && !enemy.isDead)
		{
			// play animation and go back to idle
			/*if(left)
				claws.animation.PlayQueued("attackLeft", QueueMode.CompleteOthers, PlayMode.StopSameLayer);
			else
				claws.animation.PlayQueued("attackRight", QueueMode.CompleteOthers, PlayMode.StopSameLayer);
			claws.animation.PlayQueued("idle", QueueMode.CompleteOthers, PlayMode.StopSameLayer);*/
			
			if (! enemy.RemoveLeg())
			{
				if(enemy.claws.activeSelf)
					enemy.claws.SetActive(false);
				else
				{
					enemy.isDead = true;
					enemy.gameObject.SetActive(false);
					ChainJam.AddPoints(playerID, 1);
				}
			}		
			
		}
	}
	
	/**
	 * adds a leg on a slot to the crab, if empty slot found. Return true on success
	 */
	public bool AddLeg()
	{
		foreach(LegSlot slot in legSlots)//Algorithms.Shuffle<LegSlot>(legSlots))
		{
			if(!slot.HasLeg())
			{
				slot.leg.SetActive(true);
				return true;
			}
		}
		return false;
	}
	
	/**
	 * removes a leg on a slot of the crab, if crab has at least one leg. Return true on success
	 */
	public bool RemoveLeg()
	{
		foreach(LegSlot slot in legSlots)
		{
			if(slot.HasLeg())
			{
				slot.leg.SetActive(false);
				return true;
			}
		}
		return false;
	}
}
