using UnityEngine;

public class AlienControllerScript : MonoBehaviour
{
  //explosion effect, could load in prefabs by using the resource system but just using public for now.
  public GameObject m_Explosion;
  public GameObject m_BulletProject;

  //vectors controlling movement direction
  private Vector3 m_ThrustVector = new Vector3(2.0f, 0.0f, 2.0f);
  private Vector3 m_Rotation = new Vector3(0.0f, 90.0f, 0.0f);

  private float m_WeaponReset = 0.25f;

  //main movement/weapon updating
  private void FixedUpdate()
  {
	//movement
	transform.position += m_ThrustVector * Time.deltaTime;
	transform.Rotate((m_Rotation) * Time.deltaTime);

	//wep stuff
	if (m_WeaponReset <= 0.0f)
	{
	  GameObject l_Bullet = Instantiate(m_BulletProject, transform.position + (transform.forward * 0.2f), new Quaternion());
	  l_Bullet.GetComponent<Rigidbody>().AddForce((transform.forward * 10.0f) + m_ThrustVector);
	  m_WeaponReset = 0.25f;
	}

	if (m_WeaponReset > -1.0f)
	{
	  m_WeaponReset -= Time.deltaTime;
	}

	//dist check
	if (transform.position.x < -100 || transform.position.x > 100 || transform.position.z < -100 || transform.position.z > 100)
	{
	  DestroyAlien();
	}
  }

  private void OnTriggerEnter(Collider _EnterObj)
  {
	//relocate player/asteroids to the opposite side 
	if (_EnterObj.transform.tag == "Player")
	{
	  _EnterObj.GetComponent<PlayerController>().KillPlayer();
	  DestroyAlien();
	}
	else if (_EnterObj.transform.tag == "Bullet")
	{
	  DestroyAlien();
	  Destroy(_EnterObj);
	}
  }

  //killingTime
  private void DestroyAlien()
  {
	UIController l_ScoreController = GameObject.Find("UI").GetComponent<UIController>();
	l_ScoreController.AlterScore(1000);
	Instantiate(m_Explosion, transform.position, new Quaternion());
	Destroy(gameObject);
  }
}
