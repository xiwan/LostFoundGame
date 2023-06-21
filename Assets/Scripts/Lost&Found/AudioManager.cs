using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostAndFound
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                    DontDestroyOnLoad(instance);
                }
                return instance;
            }
        }

        private AudioListener mAudioListener;
        private AudioSource mMusicSource = null;   //背景音乐
        private AudioSource source = null;
        private void CheckAudioListener()
        {
            if (!mAudioListener)
            {
                this.gameObject.AddComponent<AudioListener>();
            }
        }


        public void StopSound()
        {
            if (source != null)
            source.Stop();
        }
        /// <summary>
        /// 播放音效
        /// </summary>
        public void PlaySound(string str)
        {
            //yield return new WaitForSeconds(0.1f);
            AudioClip clip = Resources.Load<AudioClip>(str);//调用Resources方法加载AudioClip资源
            PlayAudioClip(clip);
        }

        public void PlayAudioClip(AudioClip clip)
        {
            if (clip== null)
                 return;
            source = gameObject.GetComponent<AudioSource>();
            if (source == null)
            {
                source =gameObject.AddComponent<AudioSource>();
            }

            if (!source.isPlaying)
            {
                source.clip = clip;
                //source.minDistance= 1.0f;
                //source.maxDistance= 50;
                source.rolloffMode = AudioRolloffMode.Linear;
                source.transform.position = transform.position;
                source.Play();
                //AudioSource.PlayClipAtPoint(clip, Vector3.zero);
            } 
        }

        //播放背景音乐
        public void PlayMusic(string musicName,bool loop)
        {

            CheckAudioListener();

            if (!mMusicSource)
            {
                mMusicSource = this.gameObject.AddComponent<AudioSource>();
            }

            AudioClip audioClip = Resources.Load<AudioClip>(musicName);

            mMusicSource.clip = audioClip;
            mMusicSource.loop = loop;
            mMusicSource.Play();
        }
    }
}

