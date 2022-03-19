using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    public List<GameObject> endGameObjects;

    private bool _endGame = false;

    public int currentLevel = 0;

    private int _setupLastLevel = 0;

    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));

        if (_setupLastLevel > 0)
        {
            _setupLastLevel = SaveManager.Instance.Setup.lastLevel;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController p = other.GetComponent<PlayerController>();

        if (p != null && !_endGame && _setupLastLevel != currentLevel)
        {

            ShowEndGame();
        }
    }

    private void ShowEndGame()
    {
        _endGame = true;

        endGameObjects.ForEach(i => i.SetActive(true));

        foreach(var i in endGameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();

            SaveManager.Instance.SaveLastLevel(currentLevel);
        }
    }
}
