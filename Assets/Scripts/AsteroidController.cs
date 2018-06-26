using UnityEngine;

public class AsteroidController : MonoBehaviour
{
  //explosion effect, could load in prefabs by using the resource system but just using public for now.
  public GameObject m_Explosion;

  //vectors controlling spinning/movement direction
  private Vector3 m_ThrustVector = new Vector3(0.0f, 0.0f, 0.0f);
  private Vector3 m_Rotation;

  //type of asteroid, 1 = small 2 = med 3 = large
  private int m_AsteroidType = 1;

  //init
  private void Start()
  {
	m_Rotation = Random.rotation.eulerAngles;

	m_ThrustVector.x = Random.Range(-5.0f, 5.0f);
	m_ThrustVector.z = Random.Range(-5.0f, 5.0f);
  }

  //constant movement
  private void FixedUpdate()
  {
	transform.position += m_ThrustVector * Time.deltaTime;
	transform.Rotate((m_Rotation / 5) * Time.deltaTime);

	if (transform.position.x < -100 || transform.position.x > 100 || transform.position.z < -100 || transform.position.z > 100)
	{
	  DestroyAsteroid();
	}
  }

  //destroys the asteroid, depending on type, rewards certian score and/or spawns more small asteroids
  private void DestroyAsteroid()
  {
	GameController l_Controller = GameObject.Find("GameController").GetComponent<GameController>();
	UIController l_ScoreController = GameObject.Find("UI").GetComponent<UIController>();
	switch (m_AsteroidType)
	{
	  case 2:
		l_Controller.SpawnAsteroid(transform.position.x, transform.position.z, 1).GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(1.0f, 5.0f), 0.0f, Random.Range(5.0f, 20.0f)));
		l_Controller.SpawnAsteroid(transform.position.x, transform.position.z, 1).GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(1.0f, 5.0f), 0.0f, Random.Range(5.0f, 20.0f)));
		l_Controller.m_AsteroidCount--;
		l_ScoreController.AlterScore(100);
		Instantiate(m_Explosion, transform.position, new Quaternion());
		Destroy(gameObject);
		return;
	  case 3:
		l_Controller.SpawnAsteroid(transform.position.x, transform.position.z, 2).GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(1.0f, 5.0f), 0.0f, Random.Range(5.0f, 20.0f)));
		l_Controller.SpawnAsteroid(transform.position.x, transform.position.z, 2).GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(1.0f, 5.0f), 0.0f, Random.Range(5.0f, 20.0f)));
		l_Controller.m_AsteroidCount--;
		l_ScoreController.AlterScore(150);
		Instantiate(m_Explosion, transform.position, new Quaternion());
		Destroy(gameObject);
		return;
	  default:
		l_Controller.m_AsteroidCount--;
		l_ScoreController.AlterScore(50);
		Instantiate(m_Explosion, transform.position, new Quaternion());
		Destroy(gameObject);
		return;
	}
  }

  private void OnTriggerEnter(Collider _EnterObj)
  {
	//relocate player/asteroids to the opposite side 
	if (_EnterObj.transform.tag == "Player")
	{
	  _EnterObj.GetComponent<PlayerController>().KillPlayer();
	  DestroyAsteroid();
	}
	else if (_EnterObj.transform.tag == "Bullet")
	{
	  DestroyAsteroid();
	  Destroy(_EnterObj);
	}
  }

  public void SetAsteroidType(int _Type)
  {
	m_AsteroidType = _Type;
  }
}
