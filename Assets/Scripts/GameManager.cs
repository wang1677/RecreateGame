using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public PacStudentController pacStudent;
    public Transform pellets;

    private const int DefaultLives = 3;
    private const float ResetDelay = 2.5f;

    public int Score { get; private set; }
    public int Lives { get; private set; }

    private void Start()
    {
        StartNewGame();
    }

    private void Update()
    {
        if (IsGameOver() && Input.anyKeyDown)
        {
            StartNewGame();
        }
    }

    private bool IsGameOver()
    {
        return Lives <= 0;
    }

    private void StartNewGame()
    {
        InitializeScore();
        InitializeLives();
        StartNewRound();
    }

    private void StartNewRound()
    {
        ActivateAllPellets();
        ResetGameObjects();
    }

    private void ResetGameObjects()
    {
        foreach (Ghost ghost in ghosts)
        {
            ActivateGameObject(ghost.gameObject);
        }

        ActivateGameObject(pacStudent.gameObject);
    }

    private void ActivateAllPellets()
    {
        foreach (Transform pellet in pellets)
        {
            ActivateGameObject(pellet.gameObject);
        }
    }

    private void ActivateGameObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void DeactivateGameObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void EndGame()
    {
        foreach (Ghost ghost in ghosts)
        {
            DeactivateGameObject(ghost.gameObject);
        }

        DeactivateGameObject(pacStudent.gameObject);
    }

    private void InitializeScore()
    {
        SetScore(0);
    }

    private void InitializeLives()
    {
        SetLives(DefaultLives);
    }

    private void SetScore(int score)
    {
        Score = score;
    }

    private void SetLives(int lives)
    {
        Lives = lives;
    }

    public void OnGhostEaten(Ghost ghost)
    {
        AddScore(ghost.points);
    }

    public void OnPacStudentEaten()
    {
        DeactivateGameObject(pacStudent.gameObject);

        DecreaseLives();

        if (Lives > 0)
        {
            Invoke(nameof(ResetGameObjects), ResetDelay);
        }
        else
        {
            EndGame();
        }
    }

    private void AddScore(int points)
    {
        Score += points;
    }

    private void DecreaseLives()
    {
        SetLives(Lives - 1);
    }
}