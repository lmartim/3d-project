using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public SFXType sfxType;

    public MeshRenderer meshRenderer;

    private bool _checkpointActived = false;

    public int key = 01;
    //private string _checkpointKey = "CheckpointKey";

    private void OnTriggerEnter(Collider other)
    {
        if (!_checkpointActived && other.gameObject.tag == "Player") CheckCheckpoint();
    }

    private void CheckCheckpoint()
    {
        SaveCheckpoint();
        TurnItOn();
    }

    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
    }

    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    private void SaveCheckpoint()
    {
        /*if (PlayerPrefs.GetInt(_checkpointKey, 0) > key)
            PlayerPrefs.SetInt(_checkpointKey, key);*/

        CheckpointManager.Instance.SaveCheckpoint(key);

        _checkpointActived = true;
        SFXPool.Instance.Play(sfxType);
    }
}
