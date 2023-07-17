using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace AULib
{

    public enum eMixerGroup
    {
        Master,
        Interface,
        SFX,
        Prop,
        Character,
        Vehicle,
        Ambience,
        Instruments,
        InstrumentSharp,
        BGM,
        Voice,
        InterfaceBGM,

        EndOfEnum
    }

    public class SoundManager : MonoSingletonBase<SoundManager>
    {
        public enum eSoundType
        {
            WAV,
            OGG,
            MP3
        }

      

        public List<AudioClip> EffectSounds;    
        Dictionary<string, AudioClip> dicAudioClips2;

        public const int MAX_2D_AUDIO_SOURCE = 4;   // 2D 오디오 채널 갯수
        public const int MAX_3D_AUDIO_SOURCE = 8;   // 3D 오디오 채널 갯수
        public const int MAX_2D_UNIQUE_AUDIO_SOURCE = 4;   // 특정 2D 오디오 채널 갯수
        public const int MAX_2D_TRACK_AUDIO_SOURCE = 4;   // 음원(Track) 오디오 채널 갯수

        private float _volumeSound = 1f;            // 사운드 볼륨 내부 변수 
        private float _volumeBGM = 1f;            // BGM 볼륨 내부 변수 (뮤직 아일랜드 피아노 소리때문에 줄여둠)



        // 사운드 볼륨 설정
        public float soundVolume
        {
            get { return _volumeSound; }
            set
            {
                _volumeSound = value;

                // 2D Sound 볼륨
                for (int i = 0; i < audioSourcePool.Length; i++)
                {
                    if (audioSourcePool[i] != null)
                    {
                        audioSourcePool[i].volume = _volumeSound;
                    }
                }

                // 3D Sound 볼륨 (일반)
                for (int i = 0; i < audio3DSourcePool.Length; i++)
                {
                    if (audio3DSourcePool[i] != null)
                    {
                        audio3DSourcePool[i].volume = _volumeSound;
                    }
                }

                // 3D Sound 볼륨 (루프)
                for (int i = 0; i < audio3DLoopSourceList.Count; i++)
                {
                    if (audio3DLoopSourceList[i] != null)
                    {
                        audio3DLoopSourceList[i].volume = _volumeSound;
                    }
                }
            }
        }

        // BGM 볼륨 설정
        public float bgmVolume
        {
            get { return _volumeBGM; }
            set
            {
                _volumeBGM = value;
                if (_bgmAudioSource != null)
                {
                    _bgmAudioSource.volume = _volumeBGM;
                }
            }
        }

        // Audio Source
        

        
        AudioSource[] audioSourcePool;                  // 일반 2D Audio Source
        AudioSource[] audio3DSourcePool;                // 3D Audio Source
        
        GameObject[] obj3DSourcePool;                   // 3D Audio Attach 된 Object
        List<AudioSource> audio3DLoopSourceList;        // 3D Loop Source
        Dictionary<string, AudioClip> dicAudioClips;    // Audio Clip dictionary (Load 된 적이 있는지 체크용)

        AudioSource[] audioSourceUnique;                // 특정 사운드를 계속 출력하는 2D Audio Source


        private GameObject sourcePoolGameObject;        // 2D Audio Source Pool Object




        #region 개선
        [SerializeField] private AudioMixer _mixer;

        // [SerializeField] private ListAudioSource _audioSource3DList;



        [SerializeField] private AudioSource[] _uiAudioSources;
        [SerializeField] private AudioSource _bgmAudioSource;
        



        private Dictionary<eMixerGroup , AudioMixerGroup> _audioMixerGroupMap = new();

        public AudioMixer Mixer => _mixer;

        #endregion



        // 초기화
        public override void Init()
        {
            if (sourcePoolGameObject != null)
            {
                return;
            }

            InitAudioMixerGroups();

            // 2D Audio Source Pool Object
            sourcePoolGameObject = new GameObject("AudioSourcePool");
            sourcePoolGameObject.transform.SetParent(transform);
            sourcePoolGameObject.transform.localPosition = Vector3.zero;

            // BGM
            if (_bgmAudioSource == null)
            {
                _bgmAudioSource = sourcePoolGameObject.AddComponent<AudioSource>();
                _bgmAudioSource.spatialBlend = 0;    // 2D Sound로 설정
                _bgmAudioSource.outputAudioMixerGroup = GetAudioMixerGroup( eMixerGroup.BGM );
            }
            

            // 2D Audio Source Pool
            audioSourcePool = new AudioSource[MAX_2D_AUDIO_SOURCE];
            for (int i = 0; i < audioSourcePool.Length; i++)
            {
                audioSourcePool[ i ] = sourcePoolGameObject.AddComponent<AudioSource>();
                audioSourcePool[ i ].spatialBlend = 0;    // 2D Sound로 설정
                audioSourcePool[ i ].outputAudioMixerGroup = GetAudioMixerGroup( eMixerGroup.Interface );
            }

            
            // 특정인덱스용 2D Audio Source Pool
            audioSourceUnique = new AudioSource[MAX_2D_UNIQUE_AUDIO_SOURCE];
            for (int i = 0; i < audioSourcePool.Length; i++)
            {
                audioSourceUnique[ i ] = sourcePoolGameObject.AddComponent<AudioSource>();
                audioSourceUnique[ i ].spatialBlend = 0;    // 2D Sound로 설정
                audioSourceUnique[ i ].outputAudioMixerGroup = GetAudioMixerGroup( eMixerGroup.Interface );
            }

            
            // 3D Audio Source Pool
            audio3DSourcePool = new AudioSource[MAX_3D_AUDIO_SOURCE];
            obj3DSourcePool = new GameObject[MAX_3D_AUDIO_SOURCE];
            for (int i = 0; i < obj3DSourcePool.Length; i++)
            {
                obj3DSourcePool[ i ] = new GameObject();
                obj3DSourcePool[ i ].transform.SetParent( transform );
                obj3DSourcePool[ i ].transform.position = Vector3.zero;

                audio3DSourcePool[ i ] = obj3DSourcePool[ i ].AddComponent<AudioSource>();

                // 3D Sound Setting
                audio3DSourcePool[ i ].spatialBlend = 1;
                audio3DSourcePool[ i ].dopplerLevel = 0;
                audio3DSourcePool[ i ].rolloffMode = AudioRolloffMode.Linear;
                audio3DSourcePool[ i ].maxDistance = 20;
                audio3DSourcePool[ i ].outputAudioMixerGroup = GetAudioMixerGroup( eMixerGroup.SFX );
            }

            // 3D Audio Loop Sound Pool
            audio3DLoopSourceList = new List<AudioSource>();

            // Audio Clips Pool
            dicAudioClips = new Dictionary<string, AudioClip>();


            // Audio Clips Pool2
            dicAudioClips2 = new Dictionary<string, AudioClip>();

            foreach (var item in EffectSounds)
            {
                dicAudioClips2.Add(item.name, item);
            }
        }

        private void InitAudioMixerGroups()
        {
            if( _mixer == null )
            {
                Debug.Log( "SoundManager::InitAudioMixerGroups - mixer is null" );
                return;
            }

            AudioMixerGroup[] groups = _mixer.FindMatchingGroups( "Master" );

            foreach ( var mixerGroup in groups )
            {
                _audioMixerGroupMap.Add( StringUtil.ParseEnum<eMixerGroup>( mixerGroup.name ) , mixerGroup );
                Debug.Log( $"Add MixerGroup : {mixerGroup.name}" );
            }

            Debug.Log( $"Add MixerGroup Length : {groups.Length}" );


        }


        public AudioMixerGroup GetAudioMixerGroup( eMixerGroup mixerGroup ) => mixerGroup switch
        {
            eMixerGroup.Master      => _audioMixerGroupMap[ eMixerGroup.Master      ],
            eMixerGroup.Interface   => _audioMixerGroupMap[ eMixerGroup.Interface   ],
            eMixerGroup.SFX         => _audioMixerGroupMap[ eMixerGroup.SFX         ],
            eMixerGroup.Prop        => _audioMixerGroupMap[ eMixerGroup.Prop        ],
            eMixerGroup.Character   => _audioMixerGroupMap[ eMixerGroup.Character   ],
            eMixerGroup.Vehicle     => _audioMixerGroupMap[ eMixerGroup.Vehicle     ],
            eMixerGroup.Ambience    => _audioMixerGroupMap[ eMixerGroup.Ambience    ],
            eMixerGroup.Instruments => _audioMixerGroupMap[ eMixerGroup.Instruments ],
            eMixerGroup.InstrumentSharp => _audioMixerGroupMap[eMixerGroup.InstrumentSharp],
            eMixerGroup.BGM         => _audioMixerGroupMap[ eMixerGroup.BGM         ],
            eMixerGroup.Voice       => _audioMixerGroupMap[ eMixerGroup.Voice       ],
            _ => null
        };





        // 비어 있는 슬롯 구하기
        int GetEmptyAudioSource()
        {
            for (int i = 0; i < audioSourcePool.Length; i++)
            {
                if (audioSourcePool[i].isPlaying == false) return i;
                if (audioSourcePool[i].clip == null) return i;
            }

            // 비어있는 슬롯이 없으면
            // 제일 플레이를 오래한 슬롯을 제거
            int nIDX = GetOldAudioSource();
            return nIDX;
        }

        // 제일 오래된 오디오 슬롯 구하기
        int GetOldAudioSource()
        {
            int nIDX = 0;
            float progressMax = 0;
            float progress = 0;
            for (int i = 0; i < audioSourcePool.Length; i++)
            {
                if (audioSourcePool[i].isPlaying == false) continue;
                if (audioSourcePool[i].clip == null) continue;

                progress = audioSourcePool[i].time / audioSourcePool[i].clip.length;
                if (progress > progressMax)
                {
                    progressMax = progress;
                    nIDX = i;
                }
            }

            return nIDX;
        }

        // 오디오 클립 구하기(로딩하거나 이미 로딩된 클립 반환)
        public AudioClip GetAudioClip(string strBundleName, string strClipName, eSoundType soundType = eSoundType.WAV)
        {
            // 확장자
            string fileExtension = "wav";
            if (soundType == eSoundType.OGG) fileExtension = "ogg";
            else if (soundType == eSoundType.MP3) fileExtension = "mp3";

            // Key 생성
            string strKey = string.Format("{0}/{1}.{2}", strBundleName, strClipName, fileExtension);

            AudioClip audioClip;
            if (dicAudioClips.TryGetValue(strKey, out audioClip) == true)
            {
                return audioClip;
            }

            audioClip = AddressableManager.LoadAssetSync<AudioClip>(string.Format("{0}.{1}", strClipName, fileExtension));

            if (audioClip == null)
            {
                Debug.LogError("Can't load Audio Clip : " + strKey);
                return null;
            }

            dicAudioClips.Add(strKey, audioClip);
            return audioClip;
        }

        // 일반 2D 오디오 파일 플레이 (번들에서 읽어오기)
        public AudioSource PlayAudio(string strBundleName, string strClipName, float Pitch = 1.0f, bool bLoop = false, eSoundType soundType = eSoundType.WAV)
        {
            AudioClip clip = GetAudioClip(strBundleName, strClipName, soundType);
            return PlayAudio(clip, Pitch, bLoop);
        }

        // 일반 2D 오디오 클립 플레이
        public AudioSource PlayAudio(AudioClip clip, float Pitch = 1.0f, bool bLoop = false)
        {
            if ( clip == null )
                return null;

            int AudioSourceIndex = GetEmptyAudioSource();
            if ( AudioSourceIndex < 0 || AudioSourceIndex >= audioSourcePool.Length )
                return null;

            audioSourcePool[ AudioSourceIndex ].clip = clip;
            audioSourcePool[ AudioSourceIndex ].volume = _volumeSound;
            audioSourcePool[ AudioSourceIndex ].loop = bLoop;
            audioSourcePool[ AudioSourceIndex ].pitch = Pitch;
            audioSourcePool[ AudioSourceIndex ].Play();

            return audioSourcePool[ AudioSourceIndex ];
        }


        /// <summary>
        /// enum으로 효과음 재생
        /// </summary>
        /// <param name="soundEffect"></param>
        /// <returns></returns>
        public AudioSource PlayAudio(SoundEffectEnum soundEffect)
        {
            return PlayAudio(soundEffect.ToString());
        }


        public AudioSource PlayAudio(string soundEffect)
        {
            return PlayAudio(dicAudioClips2[soundEffect]);
        }

        
        public AudioSource PlayAudioUnique(AudioClip clip, int AudioSourceIndex)
        {
            if (clip == null) return null;


            if ( AudioSourceIndex < 0 || AudioSourceIndex >= audioSourceUnique.Length )
                return null;

            if ( audioSourceUnique[ AudioSourceIndex ].isPlaying == true )
                return audioSourceUnique[ AudioSourceIndex ];

            audioSourceUnique[ AudioSourceIndex ].clip = clip;
            audioSourceUnique[ AudioSourceIndex ].volume = _volumeSound;
            audioSourceUnique[ AudioSourceIndex ].Play();

            return audioSourceUnique[ AudioSourceIndex ];
        }


        // 비어 있는 3D 오디오 슬롯 오브젝트 구하기
        GameObject GetEmpty3DAudioSource()
        {
            for (int i = 0; i < audio3DSourcePool.Length; i++)
            {
                if (audio3DSourcePool[i].isPlaying == false) return obj3DSourcePool[i];
                if (audio3DSourcePool[i].clip == null) return obj3DSourcePool[i];
            }

            // 비어있는 슬롯이 없으면
            // 제일 플레이를 오래한 슬롯을 제거
            int nIDX = GetOld3DAudioSource();
            return obj3DSourcePool[nIDX];
        }

        // 제일 오래된 3D 오디오 슬롯 구하기
        int GetOld3DAudioSource()
        {
            int nIDX = 0;
            float progressMax = 0;
            float progress = 0;
            for (int i = 0; i < audio3DSourcePool.Length; i++)
            {
                if (audio3DSourcePool[i].isPlaying == false) continue;
                if (audio3DSourcePool[i].clip == null) continue;

                progress = audio3DSourcePool[i].time / audio3DSourcePool[i].clip.length;
                if (progress > progressMax)
                {
                    progressMax = progress;
                    nIDX = i;
                }
            }

            return nIDX;
        }


        // 3D 오디오 파일 플레이 (번들에서 읽어오기)
        // public GameObject PlayAudio3D(Vector3 vecPosition, string strBundleName, string strClipName, AudioMixerGroup audioMixerGroup, float Pitch = 1.0f, bool bLoop = false, eSoundType soundType = eSoundType.WAV)
        public GameObject PlayAudio3D( Vector3 vecPosition , string strBundleName , string strClipName , eMixerGroup mixerGroup , float Pitch = 1.0f , bool bLoop = false , eSoundType soundType = eSoundType.WAV )
        {
            return PlayAudio3D( vecPosition , GetAudioClip( strBundleName , strClipName , soundType ) , mixerGroup , Pitch , bLoop );
        }

        // 3D 오디오 클립 플레이
        // public GameObject PlayAudio3D(Vector3 vecPosition, AudioClip clip, AudioMixerGroup audioMixerGroup, float Pitch = 1.0f, bool bLoop = false, float maxDistance = 20.0f)
        public GameObject PlayAudio3D( Vector3 vecPosition , AudioClip clip , eMixerGroup mixerGroup = eMixerGroup.SFX , float Pitch = 1.0f , bool bLoop = false , float maxDistance = 20.0f )
        {
            if (clip == null) return null;

            GameObject obj;
            if (bLoop == true)
            {
                // 루프 사운드는 갯수 제한 없음
                obj = new GameObject();
            }
            else
            {
                // 일반 사운드는 풀링하여 사용
                obj = GetEmpty3DAudioSource();
            }
            obj.transform.position = vecPosition;
            AudioSource audioSource = obj.GetComponent<AudioSource>();
            if (audioSource == null) audioSource = obj.AddComponent<AudioSource>();

            audioSource.clip = clip;
            audioSource.volume = _volumeSound;
            audioSource.loop = bLoop;
            audioSource.pitch = Pitch;
            audioSource.maxDistance = maxDistance;
            audioSource.outputAudioMixerGroup = GetAudioMixerGroup( mixerGroup );

            audioSource.Play();

            if (bLoop == true)
            {
                // 루프 사운드는 따로 관리

                // 3D Sound Setting
                audioSource.spatialBlend = 1;
                audioSource.dopplerLevel = 0;
                audioSource.rolloffMode = AudioRolloffMode.Linear;

                audio3DLoopSourceList.Add(audioSource);
            }
            return obj;
        }



        // 플레이중인 모든 사운드 stop (씬 전환할때 호출해주면 좋을듯)
        public void StopAllSound()
        {
            StopAll2DSound();
            StopAll3DSound();
            ClearLoop3DSound();
        }

        void StopAll2DSound()
        {
            for (int i = 0; i < audioSourcePool.Length; i++)
            {
                if (audioSourcePool[i] != null)
                {
                    if (audioSourcePool[i].isPlaying == true) audioSourcePool[i].Stop();
                }
            }
        }

        void StopAll3DSound()
        {
            for (int i = 0; i < audio3DSourcePool.Length; i++)
            {
                if (audio3DSourcePool[i] != null)
                {
                    if (audio3DSourcePool[i].isPlaying == true) audio3DSourcePool[i].Stop();
                }
            }
        }

        // 3D Loop 사운드 클리어 
        void ClearLoop3DSound()
        {
            for (int i = 0; i < audio3DLoopSourceList.Count; i++)
            {
                if (audio3DLoopSourceList[i] != null)
                {
                    Destroy(audio3DLoopSourceList[i].gameObject);
                }
            }

            audio3DLoopSourceList.Clear();
        }


        #region For BGM
        //BGM 플레이 (from Bundle)
        public AudioSource PlayBGM(string strBundleName, string strClipName, bool bLoop = false, eSoundType soundType = eSoundType.OGG)
        {
            AudioClip clip = GetAudioClip(strBundleName, strClipName, soundType);
            return PlayBGM(clip, bLoop);
        }

        //BGM 플레이 (from AudioClip)
        public AudioSource PlayBGM(AudioClip clip, bool bLoop = false)
        {
            if ( clip == null )
                return null;
            _bgmAudioSource.clip   = clip;
            _bgmAudioSource.volume = _volumeBGM;
            _bgmAudioSource.loop   = bLoop;
            _bgmAudioSource.Play();

            return _bgmAudioSource;
        }

        public void StopBGM()
        {
            if (_bgmAudioSource && _bgmAudioSource.clip != null)
                _bgmAudioSource?.Stop();
        }
        #endregion // For BGM






        #region Volume Setting

        // 런타임 중에 바뀔 수 있을까?
        float _masterVolume;
        float _interfaceVolume;
        float _sfxVolume;
        float _bgmVolume;
        float _voiceVolume;
        float _propVolume;
        float _characterVolume;
        float _vehicleVolume;
        float _ambienceVolume;
        float _instrumentsVolume;

        bool _muteMaster;
        bool _muteInterface;
        bool _muteSfx;
        bool _muteBgm;
        bool _muteVoice;

        private string MixerGroupToMixerKeyName( eMixerGroup mixerGroup ) => mixerGroup switch
        {
            eMixerGroup.Master      => "MasterVolume",
            eMixerGroup.Interface   => "InterfaceVolume",
            eMixerGroup.SFX         => "SFXVolume",
            eMixerGroup.Prop        => "PropVolume",
            eMixerGroup.Character   => "CharacterVolume",
            eMixerGroup.Vehicle     => "VehicleVolume",
            eMixerGroup.Ambience    => "AmbienceVolume",
            eMixerGroup.Instruments => "InstrumentVolume",
            eMixerGroup.InstrumentSharp => "InstrumentVolume",
            eMixerGroup.BGM         => "BGMVolume",
            eMixerGroup.Voice       => "VoiceVolume",
            _ => "None"
        };

        //private bool SetMute( eMixerGroup mixerGroup , bool mute ) => mixerGroup switch
        //{
        //    eMixerGroup.Master      => _muteMaster = mute ,
        //    eMixerGroup.Interface   => _muteInterface = mute ,
        //    eMixerGroup.SFX         => _muteSfx = mute ,
        //    eMixerGroup.BGM         => _muteBgm = mute ,
        //    eMixerGroup.Voice       => _muteVoice = mute ,
        //    _ => false
        //};

        public bool SetVolume( eMixerGroup mixerGroup , float volume , bool mute = false )      // -80 ~ 0
        {
            return _mixer.SetFloat( MixerGroupToMixerKeyName( mixerGroup ) , mute ? 0 : volume );
        }

        public void SetMute( bool mute )
        {
            SetVolume( eMixerGroup.Master    , _masterVolume    , mute );
            SetVolume( eMixerGroup.Interface , _interfaceVolume , mute );
            SetVolume( eMixerGroup.SFX       , _sfxVolume       , mute );
            SetVolume( eMixerGroup.BGM       , _bgmVolume       , mute );
            SetVolume( eMixerGroup.Voice     , _voiceVolume     , mute );
        }

        public void SetMute( eMixerGroup mixerGroup , bool mute )
        {
            SetVolume( mixerGroup , GetVolume( mixerGroup ) , mute );
        }

        public float GetVolume( eMixerGroup mixerGroup ) => mixerGroup switch
        {
            eMixerGroup.Master      => _masterVolume ,
            eMixerGroup.Interface   => _interfaceVolume ,
            eMixerGroup.SFX         => _sfxVolume ,
            eMixerGroup.BGM         => _bgmVolume ,
            eMixerGroup.Voice       => _voiceVolume ,
            _ => 0.0f
        };

        #endregion


        #region Play Soud - OneShot 만 사용함.
        //public enum  eUISoundEffect
        //{
        //    Confirm,
        //    Cancel,
        //    Click,

        //    EndOfEnum
        //}

        //public AudioClip GetUISoundEffectClip( eUISoundEffect effectType )
        //{
        //    return null;
        //}

        //public void PlayUI( eUISoundEffect effectType )
        //{
        //    PlayUIOneShot( GetUISoundEffectClip( effectType ) );
        //}

        //public void PlayUI( string clipName , bool loop = false )
        //{
        //    if ( !loop )
        //        PlayUIOneShot( null );
        //    else
        //        PlayUILoop( null );
        //}

        //public void PlayUILoop( AudioClip clip )
        //{
        //    if ( clip == null )
        //        return;
        //}

        //public void StopUI()
        //{
        //}

        //public void PlayUIOneShot( AudioClip clip )
        //{
        //    if ( clip == null )
        //        return;
        //}




        //public void PlayFXSound( AudioClip clip , Vector3 pos )
        //{
        //}

        //public void PlayBGMSound()
        //{
        //    // loop
        //}

        //public void PlayInterfaceSound()
        //{
        //    // oneshot
        //}

        #endregion
    }
}