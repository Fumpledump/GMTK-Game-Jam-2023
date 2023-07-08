using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OGFB
{
    public class OGFB_PipePooler : MonoBehaviour
    {
        [SerializeField] private int lastSpawnedPipeIndex = 0;
        [SerializeField] private float maxSpawnTime = 1.5f;
        [SerializeField] private float heightRange = 0.45f;

        private float timer;

        //Start/Endpoints
        [SerializeField] private Transform spawnPoint;
        //Pool
        [SerializeField] private List<GameObject> pipePool;

        private void Start()
        {
            SpawnPipe();
        }

        private void Update()
        {
            if(timer > maxSpawnTime)
            {
                SpawnPipe();
                timer = 0;
            }

            timer += Time.deltaTime;
        }

        private void SpawnPipe()
        {
            float spawnHeight = Random.Range(-heightRange, heightRange);
            Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y + spawnHeight, spawnPoint.position.z);
            if(lastSpawnedPipeIndex < pipePool.Count)
            {
                GameObject spawnedPipe = pipePool[lastSpawnedPipeIndex];
                spawnedPipe.transform.position = spawnPos;
                spawnedPipe.SetActive(true);
                IncrementIndex();
            }

        }

        private void IncrementIndex()
        {
            lastSpawnedPipeIndex++;
            if (lastSpawnedPipeIndex >= pipePool.Count) lastSpawnedPipeIndex = 0;
        }

        public void StopPipes()
        {
            foreach(GameObject pipe in pipePool)
            {
                OGFB_MovePipe movePipeScript = pipe.GetComponent<OGFB_MovePipe>();
                if (movePipeScript)
                    movePipeScript.SetMoving(false);
            }
        }

        public void ResetPipes()
        {
            foreach(GameObject pipe in pipePool)
            {
                pipe.SetActive(false);
                OGFB_MovePipe movePipeScript = pipe.GetComponent<OGFB_MovePipe>();
                if (movePipeScript)
                    movePipeScript.SetMoving(true);
            }
        }
    }
}
