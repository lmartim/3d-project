using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using UnityEngine.UI;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;

    public List<CheckpointBase> checkpoints;

    public Text checkpointActivatedText;

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }

    public void SaveCheckpoint(int i)
    {
        if (i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
            if (checkpointActivatedText != null) StartCoroutine(FlashMessage());
        }
    }

    public Vector3 PlaceToRespawnPlayer()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }

    IEnumerator FlashMessage()
    {
        var loopCount = 0;
        var flashText = true;
        while (flashText)
        {
            checkpointActivatedText.gameObject.SetActive(true);

            yield return new WaitForSeconds(.5f);

            checkpointActivatedText.gameObject.SetActive(false);

            yield return new WaitForSeconds(.5f);

            loopCount++;

            if (loopCount >= 4) flashText = false;
        }
    }
}
