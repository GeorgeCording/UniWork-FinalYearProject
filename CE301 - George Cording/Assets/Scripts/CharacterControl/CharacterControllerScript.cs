using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour
{
	
	Animator anim;
	
	//Whether the player has inverted their axis.
	public int invertion = 1;
	
	//bool values that track the characters state. Which direction they are facing, If they are falling, If they are alive, If they are jumping, if they are punching, if they are kicking.
	public bool facingRight = true;
	public bool falling = false;
	public bool ALIVE = true;
	public bool jumping = false;
	public bool punching = false;
	public bool kicking = false;
	
	//Bools that say if the character is debuffed and by what.
	public bool canJump = true; 
	public bool burned = false;
	public bool shocked = false; 
	public bool poisoned = false;
	public bool aired = false;
	
	//variables that handle how long the character is debuffed and how much damage they take.
	public float burnedVal = 50;
	public float burnTime = 3.0f;
	public float poisonVal = 100;
	public float bleedVal = 30;
	public float bleedTime = 3.0f;
	public float shockedTime = 3.0f;
	public float groundedTime = 4.0f;
	
	//bleed states how many times the bleeding effect has been stacked on the player.
	public int bleed = 0;
	
	//List of each quad that is the debuff symbol above the character.
	public DebuffScript[] debuffQuad;
	
	//float that tracks the speed that the character is falling.
	public float fallingVelocity = 0;
	
	//variables that track the max health and the current health of the character.
	public float maxHealth = 1000;
	public float health = 1000;
	
	
	public CharacterController cc;
	
	//TEAM is the players team pNumber is the refference to which input the character recieves.
	public int TEAM = 0;
	public string pNumber = "P1_";
	
	//How high the character can jump and how fast they move horizontally. (Can be changed by external things) 
	public float jumpSpeed = 10.0f;
	public float hMovement = 0.0f;
	
	//three integers that are each type the player is. 
	public int mainType = 0;
	public int secondType = 0;
	public int thirdType = 0;
	
	//Two other scripts that are related to the character, assigned in the unity engine.
	public HealthBarController hbc;
	public ComboController cbc;

	/* 
		Use this for initialization
		Starts by calling each debuff script's hide() function.
		Assignes the healthbar's max health.
		Stores refference to two components on the gameObject for easy refference.
		Assignes the three types and the team to the combo manager.
	*/
	void Start ()
	{
		foreach(DebuffScript d in debuffQuad)
		{
			d.hide();
		}
		hbc.setMaxHealth(maxHealth);
		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		cbc.SetTypes(mainType,secondType,thirdType, TEAM);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Handles Horizontal Movement
		float move = Input.GetAxis (pNumber+"Horizontal")*invertion;
		/*
			A character is in one of three states, on the ground, jumping or falling.
			The transitioning between three states are handled in the FixedUpdate section.
			The animator controller has a falling state, a Falling boolean and a LandingSpeed.
			When the character starts falling the Boolean is set to true to enter the animation 
			and the LandingSpeed, which determines the speed of the animation, is set to 0 to hold it
			in a dropping position until it lands. The speed of the player is also set to 0 to prevent it
			moving into the running animations. Whe the character lands, detected here by if it is grounded,
			Falling is set to false, the Landing speed is set to 3 to play the falling animation quickly 
			and then the animation enters either the Idle or the Running state as apropriate to the speed of the player.
		*/
		
		//
		if(cc.isGrounded)
		{
			anim.SetBool("Falling",false);
			anim.SetFloat ("Speed", Mathf.Abs (move));
			anim.SetFloat ("LandingSpeed", 3.0f);
		}
		else if(!jumping)
		{
			anim.SetFloat("Speed",0.0f);
			anim.SetBool("Falling",true);
			anim.SetFloat ("LandingSpeed", 0.0f);
		}
		
		//Handles facing the character in the correct direction
		if(!jumping)
		{
			if (move > 0.4f && !facingRight)
			{//turning from left to right
				Quaternion face = Quaternion.Euler (0, 90, 0);
				transform.rotation = face;
				facingRight = true;
				cbc.SetFace(facingRight);
			} else if (move < -0.4f && facingRight)
			{//turning from right to left
				Quaternion face = Quaternion.Euler (0, 270, 0);
				transform.rotation = face;
				facingRight = false;
				cbc.SetFace(facingRight);
			}
		}

		//Updates hMovement to be used in fixed update, ignores false values from a joystick
		if (Mathf.Abs (move) > 0.6)
		{
			hMovement = move * 5;
			if (facingRight == false)
				hMovement = 0 - hMovement;
		}else{
			hMovement = 0;
		}
		//End of Horizontal Movement Handling
		
		/*
			2 If statements.
			Handles Player Attacks: Jab and HighKick
			This is done by checking the player can attack
			Then calling the combo controller's function
			Then starting the coroutine that waits for the animation to end
			Then starting the animation
		*/
		//Jab
		if (Input.GetButtonDown (pNumber+"Fire1")&& !punching && !kicking)
		{
			cbc.Punch();
			StartCoroutine(Punching());
			anim.SetTrigger("Jab");
		}
		
		//High Kick
		if (Input.GetButtonDown (pNumber+"Fire2")&& !punching && !kicking) 
		{
			cbc.Kick();
			StartCoroutine(Kicking());
			anim.SetTrigger("HighKick");
		}
		//End of Attacks
		
		//Handles player taking debuff damage 
		if (burned)
		{
			heal(-burnedVal*(Time.deltaTime/burnTime));
		}
		if(poisoned)
		{
			heal(-poisonVal*Time.deltaTime);
		}
		if(aired)
		{
			heal (-bleed*(bleedVal*(Time.deltaTime/bleedTime)));
		}
		
		
	}
	
	void FixedUpdate()
	{
		//Handles jumping
		if (cc.isGrounded)
		{
			/*
				if the character is grounded
				Set falling to false
				Set the falling velocity to be strength of gravity
				If the player is pressing the jump button
					Call the combo controller's jump function
					Set falling speed to character's current jump strength
					Start Jump animation
					Start coroutine that waits for end of jump animation
			*/
			if(cc.isGrounded)
			{
				falling = false;
			}
			fallingVelocity = Physics.gravity.y * Time.deltaTime;
			if (Input.GetButtonDown (pNumber + "Jump")&&canJump)
			{
				cbc.Jump();
				fallingVelocity = jumpSpeed;
				anim.SetTrigger ("Jump");
				StartCoroutine(HopForward());
			}
		}else{
			/*
				if not grounded
				check:
				if falling then add gravity force and adjust if the character is aired
					Then set falling to be true
				Add force of gravity
			*/
			if (fallingVelocity < 0) 
			{
				fallingVelocity += Physics.gravity.y * Time.deltaTime / (aired ? 3 : 1);
				falling = true;
			}
			fallingVelocity += Physics.gravity.y * Time.deltaTime / (aired ? 2 : 1);
		}
		//Calculates Movement in both x and y direction and applies it
		Vector3 movement = new Vector3 (0.0f, fallingVelocity,falling || shocked ? (2*hMovement)/3 : hMovement );
		movement = transform.rotation * movement;
		cc.Move (movement*Time.deltaTime);
	}

	//Handles player being hit by an attack
	public void hit(int type)
	{
		//Base damage is 60
		int normal = 60;
		
		//if a person is burned they take 20 more damage or 10 if they are a fire main
		if (burned && mainType != 1)
			normal += 20;
		else if(burned)
			normal += 10;
		
		//if their main type is water or fire they take an extra 30 from lightning attacks
		if((mainType == 5 || mainType == 1) && type == 4)
		{
			normal += 30;
		}
		
		//if their main type and the shots type both are not the same or normal then the damage is adjusted
		if (mainType != 0 && type != 0 && type != mainType) 
		{
			if (WeakTo (type, mainType)) 
			{
				//if their main type is weak to the shot type they take an extra 30 damage
				normal += 30;
			} else 
			{
				//if their main type resists the shot type they take 20 less damage
				normal -= 20;
			}
		}else{
			
		}
		health -= normal;
		hbc.UpdateHealth (health);
		if(health<1)
			ALIVE = false;
		
	}
	
	public void Debuff(int type)
	{
		if(type == 0)
		{
			health-=30;
			return;
		}
		
		debuffQuad[type-1].show();
		switch(type)
		{
			case 1:
				StartCoroutine(FireDebuff(burnTime));
				break;
			case 2:
				StartCoroutine(AirDebuff(bleedTime));
				break;
			case 3:
				StartCoroutine(EarthDebuff(groundedTime));
				break;
			case 4:
				StartCoroutine(LightningDebuff(shockedTime));
				break;
			case 5:
				StartCoroutine(WaterDebuff(2));
				break;
			default:
				break;
		}
	}
	
	public void heal(float amount)
	{
		health += amount;
		if (health > maxHealth)
			health = maxHealth;
		hbc.UpdateHealth(health);
		if(health<1)
			ALIVE = false;
	}

 	static bool WeakTo(int attack, int target)
	{
		bool toReturn = false;
		if (attack + 2.5f > (float)target) 
		{
			toReturn = true;
		}
		if ((attack + 2.5f) % 5.0f < target) 
		{
			toReturn = false;
		}
		return toReturn;
	}
	
	public void CalculateDebuffs()
	{
		switch(mainType)
		{
			case 0:
			break;
			case 1:
				burnedVal = 30;
				burnTime = 2.0f;
				poisonVal = 120;
				bleedVal = 20;
				bleedTime = 3.0f;
				shockedTime = 4.0f;
				groundedTime = 3.0f;
				break;
			case 2:
				burnedVal = 80;
				burnTime = 4.0f;
				poisonVal = 120;
				bleedVal = 10;
				bleedTime = 2.0f;
				shockedTime = 2.0f;
				groundedTime = 3.0f;
				break;
			case 3:
				burnedVal = 80;
				burnTime = 3.0f;
				poisonVal = 70;
				bleedVal = 30;
				bleedTime = 4.0f;
				shockedTime = 1.5f;
				groundedTime = 2.0f;
				break;
			case 4:
				burnedVal = 20;
				burnTime = 3.0f;
				poisonVal = 70;
				bleedVal = 30;
				bleedTime = 4.0f;
				shockedTime = 1.0f;
				groundedTime = 6.0f;
				break;
			case 5:
				burnedVal = 20;
				burnTime = 2.0f;
				poisonVal = 80;
				bleedVal = 20;
				bleedTime = 3.0f;
				shockedTime = 6.0f;
				groundedTime = 5.0f;
				break;
			default:
				break;
		}
	}

	//Waits for the character to finish the Jumping animation.
	IEnumerator HopForward()
	{
		jumping = true;
		anim.SetBool ("Jumping", true);
		while(anim.GetBool("Jumping"))
		{
			yield return new WaitForFixedUpdate();
		}
		if (facingRight) 
		{
			transform.position = new Vector3(transform.position.x+0.8f,transform.position.y,transform.position.z);
		} else 
		{
			transform.position = new Vector3(transform.position.x-0.8f,transform.position.y,transform.position.z);
		}
		jumping = false;
		cbc.CountTimeOut();
	}
	
	IEnumerator Punching()
	{
		punching = true;
		anim.SetBool("Punching", true);
		yield return new WaitForSeconds (0.1f);
		while (anim.GetBool("Punching"))
		{
			yield return new WaitForFixedUpdate();
		}
		punching = false;
		cbc.CountTimeOut();
	}
	
	IEnumerator Kicking()
	{
		kicking = true;
		anim.SetBool("Kicking", true);
		yield return new WaitForSeconds (0.1f);
		while (anim.GetBool("Kicking"))
		{
			yield return new WaitForFixedUpdate();
		}
		kicking = false;
		cbc.CountTimeOut();
	}
	
	//prevents jumping
	IEnumerator EarthDebuff(float delay)
	{
		/*
			Sets the debuff boolean to denote debuff is active
			Waits for the given delay
			deactivates the debuff bolean
			hides the debuff quad.
		*/
		
		canJump = false;
		yield return new WaitForSeconds(delay);
		canJump = true;
		debuffQuad[2].hide();
	}
	
	//Takes more damage from attacks, moderate damage over time
	IEnumerator FireDebuff(float delay)
	{
		/*
			Sets the debuff boolean to denote debuff is active
			Waits for the given delay
			deactivates the debuff bolean
			hides the debuff quad.
		*/
		
		burned = true;
		yield return new WaitForSeconds(delay);
		burned = false;
		debuffQuad[0].hide();
	}
	
	//Prevents healing slow damage over time
	IEnumerator WaterDebuff(float delay)
	{
		/*
			Sets the debuff boolean to denote debuff is active
			Waits for the given delay
			deactivates the debuff bolean
			hides the debuff quad.
		*/
		
		poisoned = true;
		yield return new WaitForSeconds(delay);
		poisoned = false;
		debuffQuad[4].hide();
	}
	
	//Slows falling, stacking bleed
	IEnumerator AirDebuff(float delay)
	{
		/*
			Sets the debuff boolean to denote debuff is active
			Waits for the given delay
			deactivates the debuff bolean
			hides the debuff quad.
		*/
		
		bleed ++;
		aired = true;
		yield return new WaitForSeconds(delay);
		aired = false;
		bleed --;
		debuffQuad[1].hide();
	}
	
	//Slows Horizontal movement
	IEnumerator LightningDebuff(float delay)
	{
		/*
			Sets the debuff boolean to denote debuff is active
			Waits for the given delay
			deactivates the debuff bolean
			hides the debuff quad.
		*/
		
		shocked = true;
		yield return new WaitForSeconds(delay);
		shocked = false;
		debuffQuad[3].hide();
	}
}
