using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject[] pipePrefabs;
    private int poolSize = 6;
    private float spawnInterval = 4f;
    GameObject unActivePipe;
    private List<GameObject> pipePool;
    private void Start()
    {
        pipePool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject pipe;
            if (i % 2 == 0)
            {
                pipe = Instantiate(pipePrefabs[0]);
            }
            else
            {
                pipe = Instantiate(pipePrefabs[1]);
            }

            pipe.SetActive(false);
            pipe.transform.position = new Vector2(5, 0);
            pipePool.Add(pipe);
        }

        StartCoroutine(SpawnPipes());
    }
    private IEnumerator SpawnPipes()
    {
        while (true)
        {

            ActivateRandomPipe();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private void ActivateRandomPipe()
    {
        // Keep reference to active pipe
        GameObject activePipe = GetActivePipe();
        GameObject pipeToActivate;

        do
        {
            int randomIndex = Random.Range(0, pipePool.Count);
            pipeToActivate = pipePool[randomIndex];

        } while (pipeToActivate == activePipe); // The selected pipe must not be the same as the active pipe

        pipeToActivate.SetActive(true);
        pipeToActivate.GetComponent<PipeMovement>().Initialize(this);
    }
    private GameObject GetActivePipe()
    {
        foreach (var pipe in pipePool)
        {
            if (pipe.activeSelf)
            {
                return pipe;
            }
        }
        return null;
    }
    public void ReturnPipeToPool(GameObject pipe)
    {
        pipe.SetActive(false);
        pipe.transform.position = new Vector2(5, 0);
    }
}