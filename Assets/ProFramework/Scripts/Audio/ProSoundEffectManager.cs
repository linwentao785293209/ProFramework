using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProFramework
{
    /// <summary>
    /// 音乐音效管理器
    /// </summary>
    public class ProSoundEffectManager : ProSingletonInSystem<ProSoundEffectManager>
    {
        //管理正在播放的音效
        private List<AudioSource> _audioSourceList = new List<AudioSource>();

        //音效音量大小
        private float _soundEffectVolume = 0.5f;

        //音效是否在播放
        private bool _isPlaying = true;


        private ProSoundEffectManager()
        {
            ProMonoManager.Instance.AddFixedUpdateListener(OnFixedUpdate);
        }


        private void OnFixedUpdate()
        {
            CheckSounds();
        }

        private void CheckSounds()
        {
            if (!_isPlaying)
                return;

            //不停的遍历容器 检测有没有音效播放完毕 播放完了 就移除销毁它
            //为了避免边遍历边移除出问题 我们采用逆向遍历
            for (int i = _audioSourceList.Count - 1; i >= 0; --i)
            {
                if (!_audioSourceList[i].isPlaying)
                {
                    //音效播放完毕了 不再使用了 我们将这个音效切片置空
                    _audioSourceList[i].clip = null;
                    ProGameObjectPoolManager.Instance.Push(_audioSourceList[i].gameObject);
                    _audioSourceList.RemoveAt(i);
                }
            }
        }


        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="name">音效名字</param>
        /// <param name="isLoop">是否循环</param>
        /// <param name="isSync">是否同步加载</param>
        /// <param name="callBack">加载结束后的回调</param>
        public void Play(string name, bool isLoop = false, bool isSync = false,
            UnityAction<AudioSource> callBack = null)
        {
            //加载音效资源 进行播放
            ProAssetManager.Instance.LoadResource<AudioClip>(ProConst.Audios, name, (clip) =>
            {
                //从缓存池中取出音效对象得到对应组件
                ProGameObjectPoolManager.Instance.Get(ProConst.Audios, "SoundEffectAudioSource",
                    (gameObject) =>
                    {
                        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                        
                        //如果取出来的音效是之前正在使用的 我们先停止它
                        audioSource.Stop();

                        audioSource.clip = clip;
                        audioSource.loop = isLoop;
                        audioSource.volume = _soundEffectVolume;
                        audioSource.Play();
                        
                        //存储容器 用于记录 方便之后判断是否停止
                        //由于从缓存池中取出对象 有可能取出一个之前正在使用的（超上限时）
                        //所以我们需要判断 容器中没有记录再去记录 不要重复去添加即可
                        if (!_audioSourceList.Contains(audioSource))
                            _audioSourceList.Add(audioSource);
                        //传递给外部使用
                        callBack?.Invoke(audioSource);
                    }, isSync);
            }, isSync);
        }

        /// <summary>
        /// 停止播放音效
        /// </summary>
        /// <param name="audioSource">音效组件对象</param>
        public void Stop(AudioSource audioSource)
        {
            if (_audioSourceList.Contains(audioSource))
            {
                //停止播放
                audioSource.Stop();
                //从容器中移除
                _audioSourceList.Remove(audioSource);
                //不用了 清空切片 避免占用
                audioSource.clip = null;
                //放入缓存池
                ProGameObjectPoolManager.Instance.Push(audioSource.gameObject);
            }
        }
        
        public void ChangeVolume(float changeVolume)
        {
            // 将变化量添加到当前音量上
            _soundEffectVolume += changeVolume;

            // 将音量限制在 [0, 1] 范围内
            _soundEffectVolume = Mathf.Clamp01(_soundEffectVolume);

            // 设置背景音乐音量
            SetVolume(_soundEffectVolume);
        }

        /// <summary>
        /// 改变音效大小
        /// </summary>
        /// <param name="nowVolume"></param>
        public void SetVolume(float nowVolume)
        {
            _soundEffectVolume = nowVolume;
            for (int i = 0; i < _audioSourceList.Count; i++)
            {
                _audioSourceList[i].volume = nowVolume;
            }
        }

        /// <summary>
        /// 继续播放或者暂停所有音效
        /// </summary>
        /// <param name="isPlaying">是否是继续播放 true为播放 false为暂停</param>
        public void PlayOrPauseAll(bool isPlaying)
        {
            if (isPlaying)
            {
                _isPlaying = true;
                for (int i = 0; i < _audioSourceList.Count; i++)
                    _audioSourceList[i].Play();
            }
            else
            {
                _isPlaying = false;
                for (int i = 0; i < _audioSourceList.Count; i++)
                    _audioSourceList[i].Pause();
            }
        }

        /// <summary>
        /// 清空音效相关记录 过场景时在清空缓存池之前去调用它
        /// 重要的事情说三遍！！！
        /// 过场景时在清空缓存池之前去调用它
        /// 过场景时在清空缓存池之前去调用它
        /// 过场景时在清空缓存池之前去调用它
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < _audioSourceList.Count; i++)
            {
                _audioSourceList[i].Stop();
                _audioSourceList[i].clip = null;
                ProGameObjectPoolManager.Instance.Push(_audioSourceList[i].gameObject);
            }

            //清空音效列表
            _audioSourceList.Clear();
        }
    }
}