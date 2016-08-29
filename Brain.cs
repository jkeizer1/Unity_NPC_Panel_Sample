using UnityEngine;
using System.Collections;

public class Brain {

	static int count = 0;

	private HealthNeeds health;
	private MentalNeeds mental;
	private Capabilities capabilities;
	private Component jkController;
	private enum AwakeState { awake, sleep };
	private AwakeState awakestate;


	private class HealthNeeds {
		public int energy;
		public int sleep;

		public HealthNeeds () {
						energy = 100;
						sleep = 100;
				}
	}

	private class MentalNeeds {
		public int freindship;
		public int feelingsafe;
		public int wantingsex;

		public MentalNeeds () {
			freindship = 100;
			feelingsafe = 100;
			wantingsex = 100;
		}
	}

	private class Capabilities {
		// want to have something like:
		// I can "walk", and have the brain determiine it is time to walk.
		// And then, since walking is an attribute of the character object outside the brain,
		// it should send a walk message to the character with some relative priority.
		// The brain could maintain a message Q with messages and priorities, the body can then
		// query the queue and simply act.  This gives the possibility that the character can
		// first process messages from the system that calculates responses (reactions) without "thought",
		// and them processes messages in priority from the brain.
		// Hence the Brain-Character interface is especially important
		// The interface is between the Character Controler and the Brain
		public Capabilities() {}
	}


	public Brain (JKController p_controller) {
		health = new HealthNeeds ();
		mental = new MentalNeeds (); 
		capabilities = new Capabilities ();
		awakestate = AwakeState.awake;

		jkController = p_controller;

		count = count + 1;
		Debug.Log ("Brain Constructor: " + count.ToString ());
	}

	public void showValues () {
		jkController.SendMessage ("setText", jkController.gameObject.name + "\n");
		jkController.SendMessage ("appendText", "sleep: " + health.sleep.ToString ()  + "\n");
		jkController.SendMessage ("appendText", "energy: " + health.energy.ToString ()  + "\n");
		jkController.SendMessage ("appendText", "awake?: " + awakestate.ToString ()  + "\n");
	}

	public void update () {
		if (--health.sleep < 1)
			awakestate = AwakeState.sleep;
	}
}
