using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
  [Header("Questions")]
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
  QuestionSO currentQuestion;

  [Header("Answers")]
  [SerializeField] GameObject[] answerButtons;
  int correctAnswerIndex;
  bool hasAnsweredEarly;

  [Header("Buttons")]
  [SerializeField] Sprite defaultAnswerSprite;
  [SerializeField] Sprite correctAnswerSprite;

  [Header("Timer")]
  [SerializeField] Image timerImage;
  Timer timer;

  [Header("Scoring")]
  [SerializeField] TextMeshProUGUI scoreText;
  ScoreKeeper scoreKeeper;


  void Start()
    {
      timer = FindObjectOfType<Timer>();
      scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
      timerImage.fillAmount = timer.fillFraction;
      if(timer.loadNextQuestion)
      {
        hasAnsweredEarly = false;
        GetNextQuestion();
        timer.loadNextQuestion = false;
      }
      else if(!hasAnsweredEarly && timer.isAnsweringQuestion == false )
      {
        DisplayAnswer(-1);
        SetButtonState(false);

      }

    }

    void GetNextQuestion()
    {
      if(questions.Count > 0)
      {
        SetButtonState(true);
        SetDefaultButtonSprite();
        GetRandomQuestion();
        DisplayQuestion();
        scoreKeeper.IncrementQuestionsSeen();
      }
    }

    void GetRandomQuestion()
    {
      int index = Random.Range(0, questions.Count);
      currentQuestion = questions[index];
      if(questions.Contains(currentQuestion))
      {
        questions.Remove(currentQuestion);
      }
    }



  void SetDefaultButtonSprite()
  {
    for (int i = 0; i < answerButtons.Length; i++)
    {
      Image buttonImage;
      buttonImage = answerButtons[i].GetComponent<Image>();
      buttonImage.sprite = defaultAnswerSprite;
    }
  }


  public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    void DisplayQuestion()
    {
      questionText.text = currentQuestion.GetQuestion();

      for (int i = 0; i < answerButtons.Length; i++)
      {
        TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = currentQuestion.GetAnswer(i);
      }
    }

    void DisplayAnswer(int index)
    {
      Image buttonImage;
      if (index == currentQuestion.GetCorrectAnswerIndex())
        {
          questionText.text = "Correct";
          buttonImage = answerButtons[index].GetComponent<Image>();
          buttonImage.sprite = correctAnswerSprite;
          scoreKeeper.IncrementCorrectAnswers();
        }
      else
        {
          correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
          string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
          questionText.text = $"Incorrect, the correct answer is {correctAnswer}";
          buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
          buttonImage.sprite = correctAnswerSprite;
        }
    }

    void SetButtonState(bool state)
    {
      for (int i = 0; i < answerButtons.Length; i++)
      {
          Button button = answerButtons[i].GetComponent<Button>();
          button.interactable = state;
      }
    }

}
