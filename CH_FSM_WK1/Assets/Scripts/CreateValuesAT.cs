using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class CreateValuesAT : ActionTask {

		private Vector3 size;
		public float smallestSize;
		public float largestSize;
		public float lightestMass;
		public float heaviestMass;
		Rigidbody rb;
		float weight;

		float speedModifer;

		Blackboard agentBlackboard;
		

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
            agentBlackboard = agent.GetComponent<Blackboard>();
            rb = agent.GetComponent<Rigidbody>();
			size = new Vector3(Random.Range(smallestSize, largestSize), Random.Range(smallestSize, largestSize), Random.Range(smallestSize, largestSize));
			agent.transform.localScale = size;
			weight = Random.Range(lightestMass, heaviestMass);
			rb.mass = weight;
			rb.drag = Random.Range(0, 3);
			speedModifer = 5.0f / weight;
            agentBlackboard.SetVariableValue("speed", speedModifer);
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}