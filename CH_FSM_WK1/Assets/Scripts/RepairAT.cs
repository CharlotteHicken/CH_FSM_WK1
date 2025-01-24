using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class RepairAT : ActionTask {

		public BBParameter<Transform> targetTransform;
		Blackboard LightTowerBlackboard;
		float maxRepairValue;


        public float repairRate;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			LightTowerBlackboard = targetTransform.value.GetComponentInParent<Blackboard>();
            maxRepairValue = LightTowerBlackboard.GetVariableValue<float>("activeThreshold");
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			float currentRepairValue = LightTowerBlackboard.GetVariableValue<float>("repairValue");
			currentRepairValue += repairRate * Time.deltaTime;
			LightTowerBlackboard.SetVariableValue("repairValue", currentRepairValue);

			
			if(maxRepairValue < currentRepairValue)
			{
				EndAction(true);
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