using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.Serialization;
using STOP_MODE = FMOD.Studio.STOP_MODE;

 public class MusicManager : MonoBehaviour
//  Bools så att inte låten som spelas startar om när man går igenom AudioTrigger
// V-V-V-V-V-V-V-V-V-V-V-V-V-V-V-V-V-V-V-V-V
{
    private bool NoRestart01 = true;
    private bool NoRestart02 = false;
    private bool NoRestartBoss = false;
    
    public static MusicManager instance { get; private set; }

    private bool inCombat = false;
    private Coroutine combatCoroutineVariable;
    
    private int currentBossMusicStage;
    
    private float timer = 10;
    
    // Visible in inspector----v-v-v-v-----------------------------------------
    // Alla instanser och referenser så att utvecklaren kan lägga in sina Fmod-event. Det finns även så att man kan ändra parameters. Finns även så att man kan lägga in buses och VCA's.
    
    public float combatWaitTime = 3;
    
    [Header("Music01")]
    public EventInstance music01Instance;
    public EventReference music01Reference;
    
    [Header("Music02")]
    public EventInstance music02Instance;
    public EventReference music02Reference;
    
    [Header("MusicBoss")]
    public EventInstance musicBossInstance;
    public EventReference musicBossReference;
    
    [Header("StingerGameOver")]
    public EventReference stingerGameOverReference;
    
    public string paramName;
    public int paramValue;
    
    [Header("VCAs")]
    [SerializeField] private string vcaPath;
    private VCA vcaVariable;

    [Header("buses")]
    [SerializeField] private string busPath;
    private Bus busVariable;

    [Header("Events")]
    [SerializeField] private EventReference music;
    private EventInstance musicInstance;

    // SINGLETON-------------------------------------------------------
    // Gör så att bara en instans existerar samtidigt. gör även så att scriptet når att gå från andra scripts.
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(obj: this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(target:this);
        }
        
      
    }
	//IN COMBAT?--v-v-v-v---------------------------------------------
    // Combat script som styr en parameter automatiskt ifall man är i combat med vanliga enemies och inte Grenadier
    public void Combat()
    {
        if (!inCombat)
        {
            inCombat = true;
            music01Instance.setParameterByName("Combat", 5f);
            music02Instance.setParameterByName("Combat", 5f);
            combatCoroutineVariable = StartCoroutine(CombatTimer());
            Debug.Log("Combat Started");
        }
        else
        {
            StopCoroutine(combatCoroutineVariable);
            combatCoroutineVariable = StartCoroutine(CombatTimer());
        }
    }
    private IEnumerator CombatTimer() 
    {
        yield return new WaitForSeconds(combatWaitTime);
        Debug.Log("I`m safe!");
        inCombat = false;
        music01Instance.setParameterByName("Combat", 0f);
        music02Instance.setParameterByName("Combat", 0f);
    }
    
    public void GenerateSettings()
    {
        Debug.Log("woooo music!");
    }

    

    // Uppdaterar parametern i boss eventet i Fmod så att musiken också ändrar fas.
    public void UpdateBossMusicStage(int bossStage)
    {
        currentBossMusicStage = bossStage;
        musicBossInstance.setParameterByName("BossPhase", currentBossMusicStage);
        
        Debug.Log(currentBossMusicStage);
    }

    
    // Kör första eventet i Fmod och gör de andra eventen redo för att spelas upp
    private void Start()
    {
       music01Instance = RuntimeManager.CreateInstance(music01Reference);
       music01Instance.start();
       music02Instance = RuntimeManager.CreateInstance(music02Reference);
       musicBossInstance = RuntimeManager.CreateInstance(musicBossReference);
    }
    
    //Vet faktiskt inte vad denna gör, kan ha att göra med combat kanske?
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            music01Instance.setParameterByName(paramName, paramValue);
        }
    }

    
    // Bestämmer vad alla actions gör för varje instans/referens. Kan göras smidigare och enklare att kolla på, men vet inte riktigt hur det skall göras isåfall men så som det är nu funkar utmärkt enligt mig!
    public void SetAudioSettings(AudioSetting[] audioSettings)
    {
        foreach (AudioSetting audioSetting in audioSettings)
        {
            // Track 1 ------------------------------------------------------
            if (audioSetting.audioAction == AudioAction.SetParameter1)
            {
                music01Instance.setParameterByName(audioSetting.parameter, audioSetting.targetValue);
            }
            else if (audioSetting.audioAction == AudioAction.Play1)
            {
                if (NoRestart01 == true)
                {
                    return;
                }
                else
                {
                    music01Instance.start();
                    NoRestart01 = true;
                }
            }
            else if (audioSetting.audioAction == AudioAction.Stop1)
            {
                music01Instance.stop(STOP_MODE.ALLOWFADEOUT);
                NoRestart01 = false;
            }
            // Track 2 -------------------------------------------------------
            else if (audioSetting.audioAction == AudioAction.SetParameter2)
            {
                music02Instance.setParameterByName(audioSetting.parameter, audioSetting.targetValue);
            }
            else if (audioSetting.audioAction == AudioAction.Play2)
            {
                if (NoRestart02 == true)
                {
                    return;
                }
                else
                {
                    music02Instance.start();
                    NoRestart02 = true;
                }
            }
            else if (audioSetting.audioAction == AudioAction.Stop2)
            {
                music02Instance.stop(STOP_MODE.ALLOWFADEOUT);
                NoRestart02 = false;
            }
            // Track Boss------------------------------------------------------
            else if (audioSetting.audioAction == AudioAction.SetParameter3)
            {
                musicBossInstance.setParameterByName(audioSetting.parameter, audioSetting.targetValue);
            }
            else if (audioSetting.audioAction == AudioAction.Play3)
            {
                if (NoRestartBoss == true)
                {
                    return;
                }
                else
                {
                    musicBossInstance.start();
                    NoRestartBoss = true;
                }
            }
            else if (audioSetting.audioAction == AudioAction.Stop3)
            {
                musicBossInstance.stop(STOP_MODE.ALLOWFADEOUT);
                NoRestartBoss = false;
            }
        }
	}

    // Stinger script. Spelas upp när spelaren dör.
    public void StingerGameOver()
    {
        if (stingerGameOverReference.IsNull)
        {
            Debug.Log("huh? :(");
            return;
        }
        else
        {
            RuntimeManager.PlayOneShot(stingerGameOverReference);
        }
    }
}