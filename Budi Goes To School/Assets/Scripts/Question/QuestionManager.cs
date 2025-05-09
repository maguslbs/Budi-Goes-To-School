using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Struktur untuk menyimpan pertanyaan dan jawaban
[System.Serializable]
public struct Question
{
    public string questionNumber;
    [TextArea]
    public string questionText;
    public string[] answers; // Jawaban untuk setiap pertanyaan
    public int rightAnswer; // Index jawaban benar
}

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private GameObject questionCanvas; 
    [SerializeField] private TMP_Text questionNumberText; 
    [SerializeField] private TMP_Text questionText; 
    [SerializeField] private TMP_Text[] answerChoices; 
    [SerializeField] private Button[] choiceButtons; 
    [SerializeField] public Question[] questions;
    [SerializeField] private GameObject questionInteractText;
    [SerializeField] private GameObject player;
    [SerializeField] public GameObject _lulus;
    [SerializeField] public GameObject _gagal;

    private bool questionActivated;
    private int currentQuestionIndex = 0;
    private int score = 0; // Variabel untuk menyimpan skor pemain

    void Start()
    {
        // Menghubungkan tombol dengan metode OnAnswerSelected
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int choiceIndex = i; // Menggunakan variabel lokal untuk menghindari masalah closure
            choiceButtons[i].onClick.AddListener(() => OnAnswerSelected(choiceIndex));
        }
        
        questionCanvas.SetActive(false);
        questionInteractText.SetActive(false);
    }

    void Update()
    {
        if (questionActivated && Input.GetKeyDown(KeyCode.F))
        {
            DisplayQuestion();
            player.transform.localScale = new Vector3 (3.463446f, 3.3278f, 3.5f);
        }
    }

    public void DisplayQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            EndQuestion();
            if (questionCanvas != null) questionCanvas.SetActive(false);
        }
        else
        {
            if (questionCanvas != null) questionCanvas.SetActive(true);
            if (questionNumberText != null) questionNumberText.text = questions[currentQuestionIndex].questionNumber;
            if (questionText != null) questionText.text = questions[currentQuestionIndex].questionText;

            for (int i = 0; i < answerChoices.Length; i++)
            {
                if (answerChoices[i] != null && i < questions[currentQuestionIndex].answers.Length)
                {
                    answerChoices[i].text = questions[currentQuestionIndex].answers[i];
                }
                else
                {
                    answerChoices[i].text = "";
                }
            }
        }
    }

    private void OnAnswerSelected(int choiceIndex)
    {
        Debug.Log("Button clicked: " + choiceIndex);

        if (choiceIndex == questions[currentQuestionIndex].rightAnswer)
        {
            Debug.Log("Nice");
            score++;
        }
        else
        {
            Debug.Log("Chill");
        }

        currentQuestionIndex++;
        
        if (currentQuestionIndex >= questions.Length)
        {
            EndQuestion();
        }
        else
        {
            DisplayQuestion();
        }
    }

    public void EndQuestion()
    {
        questionActivated = false;
        if (questionCanvas != null) questionCanvas.SetActive(false);
        
        if (score >= 3)
        {
            _lulus.SetActive(true);
            StartCoroutine(CloseApplicationAfterDelay(5f));
        }
        else
        {
            _gagal.SetActive(true);
            StartCoroutine(AfterDelayGagal(5f));

        }
        
        score = 0;
        currentQuestionIndex = 0;

        questionInteractText.SetActive(true);
    }

    IEnumerator CloseApplicationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }

    IEnumerator AfterDelayGagal(float dela)
    {
        yield return new WaitForSeconds(dela);
        _gagal.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            questionActivated = true;
            questionInteractText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            questionActivated = false;
            if (questionCanvas != null) questionCanvas.SetActive(false);
            questionInteractText.SetActive(false);
        }
    }
}
