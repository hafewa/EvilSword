﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCharacter : MonoBehaviour {

	public float speed;
	//public float minDis, maxDis;
	public float range;

	public CharacterController controller;

	private PlayerCharacter player;

	public GameObject damageTextObject;

	public int damageTextDuring = 3;

	private List<GameObject> damageTexts = new List<GameObject>( );

	private Animator animator;

	private GameObject mainCamera;

	// Use this for initialization
	void Start( ) {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		animator = GetComponent<Animator>( );
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
		player.allEnemies.Add(this.gameObject);
	}

	// Update is called once per frame
	void Update( ) {
		updateDamageText( );

		if ( !InRange( ) ) {
			Chase( );
		} else {
			//animator.SetFloat("Forward", 0.0f);
			animator.SetBool("run", false);
		}
		//Debug.Log(InRange( ));
	}

	bool InRange( ) {
		var dis = Vector3.Distance(this.transform.position, player.transform.position);
		if ( dis < range )
			return true;
		return false;
	}

	void Chase( ) {
		Vector3 playerPos = player.transform.position;
		playerPos.y = 0;
		this.transform.LookAt(playerPos);
		controller.Move(this.transform.forward * speed * Time.deltaTime);
		//animator.SetFloat("Forward", 1.0f);
		animator.SetBool("run", true);
	}

	void updateDamageText( ) {
		
		//var transform = mainCamera.transform.position;
		damageTexts.RemoveAll(item => item == null);
		//return;
		foreach (var text in damageTexts ) {
			text.transform.Translate(new Vector3(0, 0.5f * Time.deltaTime, 0)); ;
			//text.transform.LookAt(mainCamera.transform.position);
			//text.transform.Rotate(new Vector3(0, 180, 0));
		}

	}
	public void ShowDamageText( string text ) {
		
		GameObject obj =  Instantiate(damageTextObject, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
		obj.GetComponent<TextMesh>( ).text = text;
		damageTexts.Add(obj);
		Destroy(obj, 2f);           // last only 2 seconds
	}	

	public void BeAttacked( ) {
		animator.SetBool("hurt", true);
		//if( state ) {
			
		//}
	}

}
