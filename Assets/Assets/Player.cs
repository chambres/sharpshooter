using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public static Player Create( KeyCode kc, int sign, System.Action<float> MotionCallback)
	{
		Player p = new GameObject().AddComponent<Player>();

		p.name = System.String.Format( "Player: {0}", kc);

		p.keyCode = kc;
		p.sign = sign;
		p.MotionCallback = MotionCallback;

		return p;
	}

	KeyCode keyCode;
	int sign;
	System.Action<float> MotionCallback;

	float timeOfLastPress;

	public bool IHaveTapped { get; private set; }

	void Update()
	{
		if (Input.GetKeyDown( keyCode))
		{
			IHaveTapped = true;
			
			
			timeOfLastPress = Time.time;

			
		}
		float since = Time.time - timeOfLastPress;
		if (since < .5f)
		{
			// one-over-time for a very non-linear payoff for rapid tapping
			float motion = 1.0f / since;

			// and lets make it even more non-linear by squaring it!
			motion *= motion;

			// call back and tell them we have some input
			MotionCallback( motion * sign);
		}
	}
}
