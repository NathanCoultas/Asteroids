using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  //checks if the player is alive or not
  private bool m_PlayerAlive = true;
  private int m_PlayerLives = 3;

  //regulates the thrust amount
  private float m_ThrustAmount = 4f;
  //wep reset
  private float m_WeaponReset = 0.2f;
  //Could've used 2d vectors but the world is built on the Z axis so using 3d.
  private Vector3 m_ThrustVector = new Vector3(0.0f, 0.0f, 0.0f);

  private UIController m_ScoreController;
  public GameObject m_BulletProject;

  private void Awake()
  {
	m_ScoreController = GameObject.Find("UI").GetComponent<UIController>();
  }

  //Main Player Control Loop
  private void FixedUpdate()
  {
	//input management 
	if (m_PlayerAlive)
	{
	  //give thrust in current forward direction 
	  if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
	  {
		m_ThrustVector += (transform.forward / m_ThrustAmount) * Time.deltaTime;
		GetComponent<Animator>().SetBool("Thrust", true);
	  }
	  else
	  {
		GetComponent<Animator>().SetBool("Thrust", false);
	  }

	  //right
	  if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
	  {
		transform.Rotate(new Vector3(0.0f, 3.0f, 0.0f));
	  }
	  //left
	  if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
	  {
		transform.Rotate(new Vector3(0.0f, -3.0f, 0.0f));
	  }

	  //fire system
	  if (Input.GetKey(KeyCode.Space) && m_WeaponReset <= 0.0f)
	  {
		GameObject l_Bullet = Instantiate(m_BulletProject, transform.position + (transform.forward * 0.2f), new Quaternion());
		l_Bullet.GetComponent<Rigidbody>().AddForce((transform.forward * 10.0f) + m_ThrustVector);

		m_WeaponReset = 0.2f;
	  }

	  //update player pos
	  transform.position += m_ThrustVector;

	  //caps the speed with decay rather than a hard limit
	  if (m_ThrustVector.x > 1.3f || m_ThrustVector.z > 1.3f)
	  {
		m_ThrustVector /= 5.0f;
	  }
	}
	//decay thrust
	m_ThrustVector /= 1.015f;

	if (m_WeaponReset > -1.0f)
	{
	  m_WeaponReset -= Time.deltaTime;
	}

  }

  //'kills' the player by playing a graphic 
  public void KillPlayer()
  {
	if (m_PlayerAlive)
	{
	  m_PlayerLives--;
	  GetComponent<Animator>().SetBool("PlayerDead", true);
	  if (m_PlayerLives > 0)
	  {
		StartCoroutine(RespawnPlayer());
		m_ScoreController.AlterScore(-500);
		m_ScoreController.SetLives(m_PlayerLives);
	  }
	  else
	  {
		m_ScoreController.GameOver();
	  }
	  m_PlayerAlive = false;
	}
  }

  //returns a bool on player alive state
  public bool IsPlayerAlive()
  {
	return m_PlayerAlive;
  }

  public void GiveLife(int _Lifes)
  {
	m_PlayerLives += _Lifes;
	m_ScoreController.SetLives(m_PlayerLives);
  }

  //respawns the player after a time
  private IEnumerator RespawnPlayer()
  {
	yield return new WaitForSeconds(3.5f);

	transform.SetPositionAndRotation(new Vector3(), new Quaternion());
	m_ThrustVector = new Vector3();
	m_PlayerAlive = true;
	GetComponent<Animator>().SetBool("PlayerDead", false);
  }

  private void OnTriggerEnter(Collider _EnterObj)
  {
	//relocate player/asteroids to the opposite side 
	if (_EnterObj.transform.tag == "AlienBullet")
	{
	  KillPlayer();
	  Destroy(_EnterObj);
	}
  }
}
