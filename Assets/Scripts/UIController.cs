using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
  //numbers
  private int m_PlayerScore;
  private int m_Highscore;
  private int m_VisualScore;

  //could've used public but this is how I prefer to grab things in the world when they are 'static' things.
  private TextMeshProUGUI m_Score;
  private TextMeshProUGUI m_Lives;

  //init, checking for a highscore. Assigning 
  private void Start()
  {
	m_Score = transform.Find("Score").GetComponent<TextMeshProUGUI>();
	m_Lives = transform.Find("Lives").GetComponent<TextMeshProUGUI>();

	if (PlayerPrefs.HasKey("HighScore") == false)
	{
	  m_Highscore = 0;
	}
	else
	{
	  m_Highscore = PlayerPrefs.GetInt("HighScore");
	}
  }

  private void FixedUpdate()
  {
	UpdateScore();
  }

  //handles the score displaying to the player
  private void UpdateScore()
  {
	if (m_VisualScore < m_PlayerScore && m_VisualScore != m_PlayerScore)
	{
	  if ((m_VisualScore + 1000) < m_PlayerScore || (m_VisualScore + 1000) == m_PlayerScore)
	  {
		m_VisualScore += 100;
		m_Score.text = m_VisualScore.ToString();
	  }
	  if ((m_VisualScore + 100) < m_PlayerScore || (m_VisualScore + 100) == m_PlayerScore)
	  {
		m_VisualScore += 15;
		m_Score.text = m_VisualScore.ToString();
	  }

	  if ((m_VisualScore + 1) < m_PlayerScore || (m_VisualScore + 1) == m_PlayerScore)
	  {
		m_VisualScore += 1;
		m_Score.text = m_VisualScore.ToString();
	  }
	}
  }
  //ease of adjusting score
  public void AlterScore(int _Points)
  {
	m_PlayerScore += _Points;
  }

  //ease of displaying lives
  public void SetLives(int _Lives)
  {
	m_Lives.text = _Lives.ToString();
  }

  //No more lives left :(
  public void GameOver()
  {
	GetComponent<Animator>().SetBool("GameOver", true);
	if (m_PlayerScore > m_Highscore)
	{
	  PlayerPrefs.SetInt("Highscore", m_PlayerScore);
	  GetComponent<Animator>().SetBool("Highscore", true);
	}
	else
	{
	  GetComponent<Animator>().SetBool("Highscore", false);
	}
  }

  //return btn
  public void BacktoMenu()
  {
	SceneManager.LoadScene(0);
  }
}
