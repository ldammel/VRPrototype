using UnityEngine;
using VRKeys;


public class DemoScene : MonoBehaviour 
{
	public Keyboard keyboard;
	public Camera cam;
	
	private void OnEnable () {
		// Improves event system performance
		Canvas canvas = keyboard.canvas.GetComponent<Canvas> ();
		canvas.worldCamera = cam;

		keyboard.Enable ();
	}

	private void OnDisable () {
		keyboard.Disable ();
	}

	/// <summary>
	/// Press space to show/hide the keyboard.
	/// Press Q for Qwerty keyboard, D for Dvorak keyboard, and F for French keyboard.
	/// </summary>
	private void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (keyboard.disabled) {
				keyboard.Enable ();
			} else {
				keyboard.Disable ();
			}
		}

		if (keyboard.disabled) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			keyboard.SetLayout (KeyboardLayout.Qwerty);
		} else if (Input.GetKeyDown (KeyCode.F)) {
			keyboard.SetLayout (KeyboardLayout.French);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			keyboard.SetLayout (KeyboardLayout.Dvorak);
		}
	}
}