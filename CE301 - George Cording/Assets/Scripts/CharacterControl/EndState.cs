using UnityEngine;
using System.Collections;

public class EndState : StateMachineBehaviour
{
	public string boolean;
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetBool (boolean, false);
	}
}
