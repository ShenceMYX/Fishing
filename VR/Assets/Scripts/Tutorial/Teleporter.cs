using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class Teleporter : MonoBehaviour
{

	public GameObject m_Pointer;

	public SteamVR_Action_Boolean m_TeleportAction;
	private SteamVR_Behaviour_Pose m_Pose = null;
	public SteamVR_Input_Sources handType;
	public SteamVR_Action_Boolean hookAction;
	private SpringJoint sj;
	private bool m_HasPosition = false;
	private float groundHeight = 0f;
	private float arcDistance = 10f;
    private float scale = 1;
	private bool isHooked = false;
	private Vector3 hookVelocity;

	void Start()
	{
		m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
		sj = GetComponent<SpringJoint>();
		sj.spring = 0;
	}

	void Update()
	{
		m_HasPosition = updatePointer();
		m_Pointer.SetActive(m_HasPosition);

		if (hookAction.GetState(handType) && isHooked) {
			sj.spring = 4f;
			StartCoroutine(switchScene());
		}

	}
	private bool updatePointer(){

		Vector3 gravity = Physics.gravity;
		Vector3 projectileVelocity = transform.forward * (arcDistance + (-transform.rotation.x * 10 > 0 ? -transform.rotation.x * 10 : 0));
		Vector3 arcPos = Vector3.zero;

		float endTime = 0;
		for (float time = 0; time < 1000; time += 0.01f){
			arcPos = transform.position + ((projectileVelocity * time) + (0.5f * time * time) * gravity) * scale;
			endTime = time;
			if (Mathf.Abs(arcPos.y - groundHeight) < 0.1f) {
				hookVelocity = (projectileVelocity + time * gravity)/7.5f;
				break;
			}
		}

		arcPos.y = 0;

		m_Pointer.transform.position = arcPos;

		return true;
		
	}

	private IEnumerator switchScene() {
		SteamVR_Fade.Start(Color.white, 1, true);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(1);
	}

	public void hooked(){
		isHooked = true;
	}

	public Vector3 getHookVelocity(){
		return hookVelocity;
	}

}
