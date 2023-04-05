using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmWrestling : MonoBehaviour
{
	// Select the keys you wish to destroy on your keyboard: choose wisely.
	KeyCode[] keyCodesToAssign = new KeyCode[] {
		KeyCode.Alpha1,
		KeyCode.L,
	};

	// the score state of the game: -90 and 90 are the wins/losses
	float angle;
	
	// this is the arm we rotate from side to side;
	GameObject armRoot;
	
	// how much we reduce the raw motion before applying it to angle
	const float MotionFactor1 = 0.01f;

	// how much further we reduce the consumed motion each frame
	const float MotionFactor2 = 0.1f;

	// how quickly we consume excessive motion commands
	const float MotionDamping = 0.99f;

	// accumulated motion from both players, which will be "consumed"
	// and turned into update angle in the Update() function
	float motion;
	
	List<Player> players;

	void Start()
	{
		players = new List<Player>();

		Light l = new GameObject ("Light").AddComponent<Light> ();
		l.type = LightType.Directional;
		l.transform.localRotation = Quaternion.Euler (50, -30, 0);
		l.intensity = 0.5f;

		GameObject.CreatePrimitive (PrimitiveType.Plane);

		armRoot = new GameObject("ArmRoot");
		GameObject viz = GameObject.CreatePrimitive(PrimitiveType.Cube);
		viz.transform.parent = armRoot.transform;
		float height = 6.0f;
		viz.transform.localScale = new Vector3( 1, height, 1);
		viz.transform.localPosition = Vector3.up * (height * 0.5f - 0.5f);
		armRoot.transform.position = Vector3.up * 0.5f;

		int sign = 1;
		foreach( KeyCode kc in keyCodesToAssign)
		{
			players.Add ( Player.Create( kc, sign, MotionCallback));
			sign *= -1;
		}
	}

	void MotionCallback( float amount)
	{
		motion += amount;
	}

	void Update()
	{
		// everybody has to have hit the keyboard once before we allow motion
		bool allHaveTapped = true;
		foreach( Player p in players)
		{
			if (!p.IHaveTapped)
			{
				allHaveTapped = false;
			}
		}

		// hold motion at zero until both players have tapped
		if (!allHaveTapped)
		{
			motion = 0;
		}

		if (Mathf.Abs ( motion) > 0.1f)
		{
			// how much motion shall we consider/consume this frame?
			float thisframe = 0.05f + Mathf.Abs ( motion * MotionFactor1);

			// which direction is it in?
			float sign = Mathf.Sign ( motion);

			// damp out the motion
			motion *= MotionDamping;

			// subtract what we used
			motion -= thisframe * sign;

			// apply what we used
			angle += (thisframe * sign) * MotionFactor2;

			armRoot.transform.localRotation = Quaternion.Euler( 0, 0, angle);

			if (angle >= 90)
			{
				Debug.LogWarning( "player 1 has won!");
				Debug.Break ();
			}
			if (angle <= -90)
			{
				Debug.LogWarning( "player 2 has won!");
				Debug.Break ();
			}
		}
	}
}
