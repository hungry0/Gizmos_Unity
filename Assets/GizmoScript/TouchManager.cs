using UnityEngine;
using System.Collections;

/*
 * 	rotate camera && move the camera 
 */

[RequireComponent(typeof(Camera))]
public class TouchManager : MonoBehaviour {

	public Transform m_GO;

	private Camera m_MainCamera;

	private float scrollWheel;

	void Awake()
	{
		m_MainCamera = GetComponent<Camera> ();
	}

	void OnEnable()
	{
		Lean.LeanTouch.OnFingerDrag += OnFingerDrag;
	}

	void OnDisable()
	{
		Lean.LeanTouch.OnFingerDrag -= OnFingerDrag;
	}

	void Update () {
		scrollWheel = Input.GetAxis ("Mouse ScrollWheel");

		m_MainCamera.transform.position += m_MainCamera.transform.forward * scrollWheel * Time.deltaTime * 1000.0f;
	}

	void OnFingerDrag(Lean.LeanFinger finger)
	{
		m_GO.localRotation *= 
			Quaternion.Euler (finger.DeltaScreenPosition.y * Time.deltaTime * 10.0f,- finger.DeltaScreenPosition.x * Time.deltaTime * 10.0f,0) ;

		if (ShowGizmosManager.PositionChangeCallback != null) {
			ShowGizmosManager.PositionChangeCallback ();
		}
	}
}
