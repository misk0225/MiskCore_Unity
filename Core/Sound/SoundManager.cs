using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace MiskCore
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Create(string name, Transform parent)
        {
            GameObject obj = new GameObject(name);
            SoundManager manager = obj.AddComponent<SoundManager>();
            manager.transform.parent = parent;
            return manager;
        }

        private Dictionary<string, AudioSource> _ChannelMap = new Dictionary<string, AudioSource>();
        private Dictionary<AudioSource, PlayingChannel> _PlayingChannel = new Dictionary<AudioSource, PlayingChannel>();
        private Dictionary<AudioSource, IDisposable> _ChannelScheduleMap = new Dictionary<AudioSource, IDisposable>();

        public float Volume
        {
            get
            {
                return _Volume;
            }
            set
            {
                _Volume = Mathf.Clamp01(value);
                foreach (PlayingChannel channel in _PlayingChannel.Values)
                {
                    channel.Source.volume = _Volume * channel.Volume;
                }
            }
        }
        private float _Volume = 1f;

        public void Play(AudioClip clip, bool loop = false, float volume = 1f, float pitch = 1f)
        {
            AudioSource source = AudioSourcePool.Instance.Get();
            StartPlayingClip(source, clip, loop, volume, pitch);
        }
        public void Play(string name, AudioClip clip, bool loop = false, float volume = 1f, float pitch = 1f)
        {
            if (!_ChannelMap.ContainsKey(name))
                _ChannelMap[name] = AudioSourcePool.Instance.Get();
            else
                RemovePlayingSource(_ChannelMap[name]);

            StartPlayingClip(_ChannelMap[name], clip, loop, volume, pitch);
        }
        public void SetVolume(string name, float volume)
        {
            if (_ChannelMap.ContainsKey(name))
            {
                AudioSource source = _ChannelMap[name];
                _PlayingChannel[source].Volume = volume;
                source.volume = _PlayingChannel[source].Volume * Volume;
            }

        }
        public void Stop(string name)
        {
            if (_ChannelMap.ContainsKey(name)) { 
                RemovePlayingSource(_ChannelMap[name]);
                _ChannelMap.Remove(name);
            }
        }
        public void StopAll()
        {
            List<PlayingChannel> channels = _PlayingChannel.Values.ToList();
            foreach (PlayingChannel channel in channels)
            {
                RemovePlayingSource(channel.Source);
            }
        }

        private void StartPlayingClip(AudioSource source, AudioClip clip, bool loop = false, float volume = 1f, float pitch = 1f)
        {
            source.loop = loop;
            source.clip = clip;
            source.volume = volume * Mathf.Clamp01(Volume);
            source.pitch = pitch;
            source.transform.parent = transform;

            IDisposable scheduler = Utils.Updater(() =>
            {
                if (source.time >= clip.length && !source.loop)
                {
                    RemovePlayingSource(source);
                }
            });

            _PlayingChannel.Add(source, new PlayingChannel{ Source = source, Volume = volume });
            _ChannelScheduleMap.Add(source, scheduler);

            source.Play();
        }

        private void RemovePlayingSource(AudioSource source)
        {
            if (_ChannelScheduleMap.ContainsKey(source))
            {
                _ChannelScheduleMap[source].Dispose();
                AudioSourcePool.Instance.Recycle(source);

                if (_PlayingChannel.ContainsKey(source))
                    _PlayingChannel.Remove(source);

                _ChannelScheduleMap.Remove(source);
            }
        }



        private class PlayingChannel
        {
            public AudioSource Source;
            public float Volume
            {
                get
                {
                    return _Volume;
                }

                set
                {
                    _Volume = Mathf.Clamp01(value);
                }
            }

            private float _Volume = 1;
        }
    }
}
