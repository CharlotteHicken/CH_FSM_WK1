using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class RechargeAT : ActionTask {

        public Transform target;
        public float speed;
        public float arrivalDistance;
        public float maxEnergy;

        public float rechargeTime;

        float timeRecharging = 0f;
        

        Blackboard agentBlackboard;
        
		
		//Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            agentBlackboard = agent.GetComponent<Blackboard>();
            speed = agentBlackboard.GetVariableValue<float>("speed");

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            timeRecharging = 0f;
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

            float distanceToTarget = Vector3.Distance(target.position, agent.transform.position);

            float currentEnergy = agentBlackboard.GetVariableValue<float>("energy");

            currentEnergy = maxEnergy;

            agentBlackboard.SetVariableValue("energy", currentEnergy);

            

            if (distanceToTarget < arrivalDistance)
            {
                timeRecharging += Time.deltaTime;
                if (timeRecharging> rechargeTime)
                {
                    EndAction(true);
                }
            }
            else
            {
                Vector3 moveDirection = (target.position - agent.transform.position).normalized;
                agent.transform.position += moveDirection * speed * Time.deltaTime;
            }
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}