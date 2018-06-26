using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  private PlayerController m_PlayerController;
  private int m_Level = 0;
  //orignally wanted to put some powerups in such as faster firing speed/life pick ups but not enough time
  private float m_AsteroidSpawnDelay = 1.0f, m_AlienSpawnDelay = 45.0f, m_PowerUpDelay = 30.0f;

  public GameObject m_Asteroid1, m_Asteroid2, m_Asteroid3, m_Alien;

  //public for ease of adjusting, ideally would use public adjustment functions
  public int m_AsteroidAmount = 10, m_AsteroidCount;

  //init
  private void Awake()
  {
	m_PlayerController = GameObject.Find("PlayerShip").GetComponent<PlayerController>();
  }

  private void FixedUpdate()
  {
	//Alien Spawing
	if (m_AlienSpawnDelay > 0.0f)
	{
	  m_AlienSpawnDelay -= Time.deltaTime;
	}
	else
	{
	  float l_SpawnX = 0.0f, l_SpawnZ = 0.0f;
	  if (Random.value >= 0.5f)
	  {
		l_SpawnZ = 7.5f;
	  }
	  else
	  {
		l_SpawnZ = -7.5f;
	  }
	  l_SpawnX = Random.Range(-14.0f, 14.0f);

	  Instantiate(m_Alien, new Vector3(l_SpawnX, 0.0f, l_SpawnZ), new Quaternion());
	  m_AlienSpawnDelay = 45.0f;
	}


	//spawning asteroids
	if (m_AsteroidSpawnDelay > 0.0f)
	{
	  m_AsteroidSpawnDelay -= Time.deltaTime;
	}
	else if (m_AsteroidAmount > 0)
	{
	  float l_SpawnX = 0.0f, l_SpawnZ = 0.0f;

	  if (Random.value >= 0.5f)
	  {
		l_SpawnZ = 7.5f;
	  }
	  else
	  {
		l_SpawnZ = -7.5f;
	  }

	  l_SpawnX = Random.Range(-14.0f, 14.0f);

	  SpawnAsteroid(l_SpawnX, l_SpawnZ, Mathf.CeilToInt(Random.Range(0.5f, 2.5f)));
	  m_AsteroidSpawnDelay = 1.0f;
	  m_AsteroidAmount--;
	}
	else if (m_AsteroidCount <= 0)
	{
	  m_Level++;
	  m_PlayerController.GiveLife(1);
	  m_AsteroidAmount = 10 + m_Level;
	}
  }

  //Spawns an asteroid of a certian type on inputted cords, also returns the object to be used elsewhere
  public GameObject SpawnAsteroid(float _Xpos, float _Zpos, int _Type)
  {
	switch (_Type)
	{
	  case 1:
		GameObject l_Asteroid1 = Instantiate(m_Asteroid1, new Vector3(_Xpos, 0.0f, _Zpos), new Quaternion());
		l_Asteroid1.GetComponent<AsteroidController>().SetAsteroidType(1);
		m_AsteroidCount++;
		return l_Asteroid1;
	  case 2:
		GameObject l_Asteroid2 = Instantiate(m_Asteroid2, new Vector3(_Xpos, 0.0f, _Zpos), new Quaternion());
		l_Asteroid2.GetComponent<AsteroidController>().SetAsteroidType(2);
		m_AsteroidCount++;
		return l_Asteroid2;
	  case 3:
		GameObject l_Asteroid3 = Instantiate(m_Asteroid3, new Vector3(_Xpos, 0.0f, _Zpos), new Quaternion());
		l_Asteroid3.GetComponent<AsteroidController>().SetAsteroidType(3);
		m_AsteroidCount++;
		return l_Asteroid3;
	  default:
		//do nothing if no type provided
		return null;
	}
  }
}
