using System.Collections;
using UnityEngine;

//simple script for cleaning up objects that might go out of scope
public class KillScript : MonoBehaviour
{
  //adjustable for kill time
  public float m_KillTime = 5.0f;

  private void Start()
  {
	StartCoroutine(KillObject());
  }

  private IEnumerator KillObject()
  {
	yield return new WaitForSeconds(5.0f);

	Destroy(gameObject);
  }
}
