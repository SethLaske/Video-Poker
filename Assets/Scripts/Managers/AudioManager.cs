using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Manages audio for the game, specifically managing SFX
    /// Formerly used in several of my projects, this was simplified for this project
    /// 

    public class AudioManager : MonoBehaviour
    {
        private Queue<AudioSource> availableSources = new Queue<AudioSource>();

        public AudioSource sFXPrefab;

        private AudioSource referencedSource;

        private int spawnAmount = 8;

        [Header("Common Sound Clips")]
        public AudioClip winSound;
        public AudioClip buttonPress;
        public AudioClip cardPress;
        public AudioClip cardDeal;
        public AudioClip buttonReject;

        private void Awake()
        {

            if (availableSources.Count <= 0)
            {
                SpawnObjects();
            }
        }

        private void SpawnObjects()
        {
            if (sFXPrefab == null)
            {
                return;
            }
            for (int i = 0; i < spawnAmount; i++)
            {

                ReturnSourceToPool(Instantiate(sFXPrefab, transform));
            }
        }


        public void PlaySound(AudioClip clip)
        {
            referencedSource = GetSourceFromPool();
            referencedSource.clip = clip;
            referencedSource.Play();

            ReturnSourceToPool(referencedSource);
        }
        private void ReturnSourceToPool(AudioSource source)
        {
            availableSources.Enqueue(source);
            source.transform.parent = transform;
        }

        private AudioSource GetSourceFromPool()
        {
            if (availableSources.Count <= 0)
            {
                SpawnObjects();
            }

            referencedSource = availableSources.Dequeue();

            referencedSource.gameObject.SetActive(true);

            return referencedSource;
        }

    }

}