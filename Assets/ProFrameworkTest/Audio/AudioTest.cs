using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;


namespace ProFrameworkTest
{
    public class AudioTest : MonoBehaviour
    {
        private void Update()
        {
            // 按下 Q 键，测试播放音效
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ProSoundEffectManager.Instance.Play("SoundEffectsTest", false, false,
                    (source) => { ProLog.LogDebug("Sound effect is playing"); });
            }
    
            // 按下 W 键，测试改变音效大小
            if (Input.GetKeyDown(KeyCode.W))
            {
                var changeVal = Random.Range(-0.5f, 0.5f);
                ProLog.LogDebug($"改变多少音量{changeVal}");
                ProSoundEffectManager.Instance.ChangeVolume(changeVal);
            }
    
            // 按下 E 键，测试改变音效大小
            if (Input.GetKeyDown(KeyCode.E))
            {
                ProSoundEffectManager.Instance.SetVolume(1f);
            }
    
            // 按下 R 键，测试继续播放所有音效
            if (Input.GetKeyDown(KeyCode.R))
            {
                ProSoundEffectManager.Instance.PlayOrPauseAll(true);
            }
    
            // 按下 T 键，测试暂停所有音效
            if (Input.GetKeyDown(KeyCode.T))
            {
                ProSoundEffectManager.Instance.PlayOrPauseAll(false);
            }
    
            // 按下 Y 键，测试清空音效相关记录
            if (Input.GetKeyDown(KeyCode.Y))
            {
                ProSoundEffectManager.Instance.Clear();
            }
    
            // 按下 U 键，测试播放背景音乐
            if (Input.GetKeyDown(KeyCode.U))
            {
                ProBackgroundMusicManager.Instance.Play("BackgroundMusicTest");
            }
    
            // 按下 I 键，测试停止背景音乐
            if (Input.GetKeyDown(KeyCode.I))
            {
                ProBackgroundMusicManager.Instance.Stop();
            }
    
            // 按下 O 键，测试暂停背景音乐
            if (Input.GetKeyDown(KeyCode.O))
            {
                ProBackgroundMusicManager.Instance.Pause();
            }
    
            // 按下 P 键，测试设置背景音乐大小
            if (Input.GetKeyDown(KeyCode.P))
            {
                ProBackgroundMusicManager.Instance.ChangeVolume(Random.Range(-0.5f,0.5f));
            }
        }
    }
}
