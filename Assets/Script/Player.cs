using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        private int _curHealth;
        public int maxHealth = 120;

        
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            curHealth = maxHealth;
        }
    }
    
    public PlayerStats playerStats = new PlayerStats();

    public int fallBoundry = -10;

    private AudioManager audiomanager;

    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        playerStats.Init();
        if (statusIndicator == null)
        {
            Debug.LogError("No status indicator on the player");
        }
        else
        {
            statusIndicator.SetHealth(playerStats.curHealth, playerStats.maxHealth);
        }

        audiomanager = AudioManager.instance;
    }
   
    void Update()
    {
        if (transform.position.y <= fallBoundry)
        {
           DamagePlayer(999);
        }
    }

    public IEnumerator DamagedColor()
    {
        SpriteRenderer graphics = transform.Find("Graphics").gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer arm = transform.Find("Arm").gameObject.GetComponent<SpriteRenderer>();
        for (int i = 0; i < 2; i++) {
            ChangeColor(graphics);
            ChangeColor(arm);
            yield return new WaitForSeconds(0.15f);
        }
        graphics.color = Color.white;
        yield break;
    }

    void ChangeColor(SpriteRenderer elmt)
    {
        if (elmt.color == Color.white)
        {
            elmt.color = Color.red;
        }
        else
        {
            elmt.color = Color.white;
        }
    }

    public void DamagePlayer(int damage)
   {
       
       playerStats.curHealth -= damage;
       if (playerStats.curHealth <= 0)
       {
           GameMaster.KillPlayer(this);
       }
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(playerStats.curHealth, playerStats.maxHealth);
        }
        StartCoroutine(DamagedColor());
    }
}
