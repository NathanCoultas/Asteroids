using UnityEngine;

public class ZoneController : MonoBehaviour
{
  //Zone Controller tracks objects leaving the playing area and then either relocates them or destroys them
  void OnTriggerExit(Collider _LeavingObj)
  {
	//relocate player/asteroids to the opposite side 
	if (_LeavingObj.transform.tag == "Asteroid" || _LeavingObj.transform.tag == "Player" || _LeavingObj.transform.tag == "Alien")
	{
	  Vector3 l_ObjectPos = _LeavingObj.transform.position;
	  //check X
	  if (_LeavingObj.transform.position.x >= 13.9f)
	  {
		l_ObjectPos.x = -14f;
	  }
	  else if (_LeavingObj.transform.position.x <= -13.9f)
	  {
		l_ObjectPos.x = 14f;
	  }
	  //check Z
	  if (_LeavingObj.transform.position.z >= 8.4f)
	  {
		l_ObjectPos.z = -8.5f;
	  }
	  else if (_LeavingObj.transform.position.z <= -8.4f)
	  {
		l_ObjectPos.z = 8.5f;
	  }
	  _LeavingObj.transform.position = l_ObjectPos;
	}
	else
	{
	  Destroy(_LeavingObj.gameObject);
	}
	//clears objects leaving the gameplay zone
  }
}
