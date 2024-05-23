using UnityEngine;

namespace ProFramework
{
    public class ProBackgroundMusicManager : ProSingletonInSystem<ProBackgroundMusicManager>
    {
        //背景音乐播放组件
        private AudioSource _backgroundMusicAudioSource = null;

        //背景音乐大小
        private float _backgroundMusicVolume = 0.5f;

        private ProBackgroundMusicManager()
        {
        }

        //播放背景音乐
        public void Play(string name)
        {
            //动态创建播放背景音乐的组件 并且 不会过场景移除 
            //保证背景音乐在过场景时也能播放
            if (_backgroundMusicAudioSource == null)
            {
                GameObject backgroundMusicAudioSourceGameObject = new GameObject();
                backgroundMusicAudioSourceGameObject.name = "BackgroundMusicAudioSource";
                GameObject.DontDestroyOnLoad(backgroundMusicAudioSourceGameObject);
                _backgroundMusicAudioSource = backgroundMusicAudioSourceGameObject.AddComponent<AudioSource>();
            }

            //根据传入的背景音乐名字 来播放背景音乐
            ProAssetManager.Instance.LoadResource<AudioClip>(ProConst.Audios, name, (clip) =>
            {
                _backgroundMusicAudioSource.clip = clip;
                _backgroundMusicAudioSource.loop = true;
                _backgroundMusicAudioSource.volume = _backgroundMusicVolume;
                _backgroundMusicAudioSource.Play();
            });
        }

        //停止背景音乐
        public void Stop()
        {
            if (_backgroundMusicAudioSource == null)
                return;

            _backgroundMusicAudioSource.Stop();
        }

        //暂停背景音乐
        public void Pause()
        {
            if (_backgroundMusicAudioSource == null)
                return;
            _backgroundMusicAudioSource.Pause();
        }

        // 改变背景音乐音量
        public void ChangeVolume(float changeVolume)
        {
            // 将变化量添加到当前音量上
            _backgroundMusicVolume += changeVolume;

            // 将音量限制在 [0, 1] 范围内
            _backgroundMusicVolume = Mathf.Clamp01(_backgroundMusicVolume);

            // 设置背景音乐音量
            SetVolume(_backgroundMusicVolume);
        }


        //设置背景音乐大小
        public void SetVolume(float nowVolume)
        {
            _backgroundMusicVolume = nowVolume;
            if (_backgroundMusicAudioSource == null)
                return;
            _backgroundMusicAudioSource.volume = _backgroundMusicVolume;
        }
    }
}