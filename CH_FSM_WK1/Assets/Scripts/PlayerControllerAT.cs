using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class PlayerControllerAT : ActionTask {


        float acceleration;
        float deceleration;
        public BBParameter<float> maxSpeed = 5;
        public BBParameter<float> currentSpeed;
        public float accelerateTime = 1;
        public float decelerateTime = 1;
        Vector3 velocity;
        
		//Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            acceleration = maxSpeed.value / accelerateTime;
            deceleration = maxSpeed.value / decelerateTime;
            currentSpeed.value = maxSpeed.value;
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            float leftRightInput = Input.GetAxis("Horizontal");
            float upDownInput = Input.GetAxis("Vertical");
            Vector3 inputValues = new Vector3(leftRightInput, 0, upDownInput);

            if (inputValues.magnitude != 0)
            {
                velocity += inputValues * acceleration * Time.deltaTime;
            }
            else
            {
                velocity -= velocity * deceleration * Time.deltaTime;
            }

            if (velocity.magnitude < 0.001f)
            {
                velocity = Vector3.zero;
            }

            velocity = Vector3.ClampMagnitude(velocity, currentSpeed.value);

            agent.transform.position += velocity * Time.deltaTime;
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}