using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;
    ScoreKeeper scoreKeeper;

    StartScreen startScreen;

    void Awake()
    {
      quiz = FindObjectOfType<Quiz>();
      endScreen = FindObjectOfType<EndScreen>();
      startScreen = FindObjectOfType<StartScreen>();
    }

    void Start()
    {
      startScreen.gameObject.SetActive(true);
      quiz.gameObject.SetActive(false);
      endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
      // if(quiz.isComplete)
      // {
      //   quiz.gameObject.SetActive(false);
      //   endScreen.gameObject.SetActive(true);
      //   endScreen.ShowFinalScore();
      // }
      // else
      // {
      // StartQuiz();
      // }
    }

    public void OnReplayLevel()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void StartQuiz()
    {
      startScreen.gameObject.SetActive(false);
      quiz.gameObject.SetActive(true);
      endScreen.gameObject.SetActive(false);

    }


}
