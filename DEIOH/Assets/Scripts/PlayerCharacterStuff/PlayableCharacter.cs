using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterHealth))]
public class PlayableCharacter : MonoBehaviour {

	// class that defines whether a gameobject is a character that can be played by the player
	// character's death effects overall gamestate
	// could be considered a "pawn" if using unreal terminology
	// takes all input from player, tells scripts what to do

	public CharacterHealth health;
	public CharacterMovement movement;
	public Inventory inventory;
	public PlayerButtonInteraction interaction;

	public void Awake()
	{
		// get all references
	 	health = GetComponent<CharacterHealth>();
		movement = GetComponent<CharacterMovement>();
		inventory = GetComponent<Inventory>();
		interaction = GetComponent<PlayerButtonInteraction>();
	}

	public void Update()
	{
		if (DeiohGame.self.player == this) // if we are the chosen one
		{
			
		}
	}
}
