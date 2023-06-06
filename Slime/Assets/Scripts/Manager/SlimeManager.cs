using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlimeManager : MonoBehaviour
{
    int x = 0;
    int y = 1;
    int z = 0;

    [SerializeField] public GameObject levelUpPanel;
    [SerializeField] public int Levelexp = 10;
    [SerializeField] public SlimeSet slime;
    [SerializeField] public Attacker attackerPrefab;
    [SerializeField] public Attack attackPrefab;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float range;
    [SerializeField] public float immuneTime;
    [SerializeField] public bool immune;

    [SerializeField] public int exp;
    [SerializeField] public int[] nextExp;
    [SerializeField] public int level;
    [SerializeField] public int score;

    private GameManager _gameManager;
    private GameObject _slimeObject;
    public GameObject Gameover;
    public GameObject Slime;
    public int PlayerID;
    

    private int _moveSpeedLv = 0;
    private int _attackSpeedLv = 0;
    private int _damageLv = 0;
    private int _maxHealthLv = 0;

    private TextMeshProUGUI _choice01Label;
    private TextMeshProUGUI _choice02Label;
    private TextMeshProUGUI _choice03Label;
    private Image _choice01Image;
    private Image _choice02Image;
    private Image _choice03Image;

    private StatusSet[] _statusSets =
    {
        StatusSet.MoveSpeed,
        StatusSet.AttackSpeed,
        StatusSet.Damage,
        StatusSet.MaxHealth
    };
    Sprite moveSpeedImage;
    Sprite attackSpeedImage;
    Sprite damageImage;
    Sprite maxHealthImage;



    private void Start()
    {
        _gameManager = gameObject.GetComponent<GameManager>();
        _slimeObject = GameObject.FindGameObjectWithTag("Slime");
        Camera cam = Camera.main;
        cam.transform.SetParent(_slimeObject.transform);
        health = maxHealth;

        Sprite[] statusSprites = Resources.LoadAll<Sprite>("StatusIcon");
        foreach (var statusSprite in statusSprites)
        {
            
            switch (statusSprite.name)
            {
                case "MoveSpeed":
                    moveSpeedImage = statusSprite;
                    print(statusSprite.name);
                    break;
                case "AttackSpeed":
                    attackSpeedImage = statusSprite;
                    print(statusSprite.name);
                    break;
                case "Damage":
                    damageImage = statusSprite;
                    print(statusSprite.name);
                    break;
                case "MaxHealth":
                    maxHealthImage = statusSprite;
                    print(statusSprite.name);
                    break;
            }
        }
        
        Transform panelTransform = levelUpPanel.transform.Find("Panel");
        _choice01Label = panelTransform.Find("choice01").Find("choice01label").GetComponent<TextMeshProUGUI>();
        _choice02Label = panelTransform.Find("choice02").Find("choice02label").GetComponent<TextMeshProUGUI>();
        _choice03Label = panelTransform.Find("choice03").Find("choice03label").GetComponent<TextMeshProUGUI>();
        _choice01Image = panelTransform.Find("choice01").Find("choice01image").GetComponent<Image>();
        _choice02Image = panelTransform.Find("choice02").Find("choice02image").GetComponent<Image>();
        _choice03Image = panelTransform.Find("choice03").Find("choice03image").GetComponent<Image>();


        for (int i = 0; i < 30; i++)
        {
            z = x + y;
            x = y;
            y = z;
            nextExp[i] = x * 10;
        }
    }

    public void AddHealth(float value)
    {
        health += value;

        if (health <= 0)
        {
            health = 0;
            moveSpeed = 0;
            Gameover.gameObject.SetActive(true);
        }

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void AddExp(int value)
    {
        exp += value;
        score++;

        health += 5;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        ShuffleSet();
        SetLevelUpUI();
        Time.timeScale = 0;
        levelUpPanel.gameObject.SetActive(true);
    }

    public void ChoiceStatus01()
    {
        Upgrade(1);
        CloseLevelUpPanel();
    }

    public void ChoiceStatus02()
    {
        Upgrade(2);
        CloseLevelUpPanel();
    }

    public void ChoiceStatus03()
    {
        Upgrade(3);
        CloseLevelUpPanel();
    }

    private void CloseLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetLevelUpUI()
    {
        _choice01Label.text = GetChoiceString(1);
        _choice02Label.text = GetChoiceString(2);
        _choice03Label.text = GetChoiceString(3);
        _choice01Image.sprite = GetChoiceImage(1);
        _choice02Image.sprite = GetChoiceImage(2);
        _choice03Image.sprite = GetChoiceImage(3);
    }

    private string GetChoiceString(int choiceNum)
    {
        string result = "";
        switch (_statusSets[choiceNum - 1])
        {
            case StatusSet.MoveSpeed:
                result = string.Format("Move Speed Lv.{0:F0}", _moveSpeedLv + 1);
                break;
            case StatusSet.AttackSpeed:
                result = string.Format("Attack Speed Lv.{0:F0}", _attackSpeedLv + 1);
                break;
            case StatusSet.Damage:
                result = string.Format("Attack Damage Lv.{0:F0}", _damageLv + 1);
                break;
            case StatusSet.MaxHealth:
                result = string.Format("Max Health  Lv.{0:F0}", _maxHealthLv + 1);
                break;
        }

        return result;
    }
    
    private Sprite GetChoiceImage(int choiceNum)
    {
        Sprite result = moveSpeedImage;
        switch (_statusSets[choiceNum - 1])
        {
            case StatusSet.MoveSpeed:
                result = moveSpeedImage;
                break;
            case StatusSet.AttackSpeed:
                result = attackSpeedImage;
                break;
            case StatusSet.Damage:
                result = damageImage;
                break;
            case StatusSet.MaxHealth:
                result = maxHealthImage;
                break;
        }

        return result;
    }

    private void ShuffleSet()
    {
        int random1, random2;
        StatusSet temp;

        for (int i = 0;
             i < _statusSets.Length;
             ++i)
        {
            random1 = Random.Range(0, _statusSets.Length);
            random2 = Random.Range(0, _statusSets.Length);

            temp = _statusSets[random1];
            _statusSets[random1] = _statusSets[random2];
            _statusSets[random2] = temp;
        }

        print(_statusSets);
    }

    private void Upgrade(int choiceNum)
    {
        switch (_statusSets[choiceNum - 1])
        {
            case StatusSet.MoveSpeed:
                UpgradeMoveSpeed();
                break;
            case StatusSet.AttackSpeed:
                UpgradeAttackSpeed();
                break;
            case StatusSet.Damage:
                UpgradeDamage();
                break;
            case StatusSet.MaxHealth:
                UpgradeMaxHealth();
                break;
        }
    }

    private void UpgradeMoveSpeed()
    {
        _moveSpeedLv += 1;
        moveSpeed += 1;
    }

    private void UpgradeAttackSpeed()
    {
        _attackSpeedLv += 1;
        attackSpeed *= 1.2f;
    }

    private void UpgradeDamage()
    {
        _damageLv += 1;
        damage *= 1.2f;
    }

    private void UpgradeMaxHealth()
    {
        _maxHealthLv += 1;
        maxHealth *= 1.2f;
    }
}