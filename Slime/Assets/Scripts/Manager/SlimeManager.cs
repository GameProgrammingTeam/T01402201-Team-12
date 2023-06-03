using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlimeManager : MonoBehaviour
{
    int x = 0;
    int y = 1;
    int z = 0;

    [SerializeField] public GameObject levelUpPannel;
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


    private void Start()
    {
        _gameManager = gameObject.GetComponent<GameManager>();
        _slimeObject = GameObject.FindGameObjectWithTag("Slime");
        Camera cam = Camera.main;
        cam.transform.SetParent(_slimeObject.transform);
        health = maxHealth;

        Transform pannelTransform = levelUpPannel.transform.Find("Panel");
        _choice01Label = pannelTransform.Find("choice01").Find("choice01label").GetComponent<TextMeshProUGUI>();
        _choice02Label = pannelTransform.Find("choice02").Find("choice02label").GetComponent<TextMeshProUGUI>();
        _choice03Label = pannelTransform.Find("choice03").Find("choice03label").GetComponent<TextMeshProUGUI>();
        // _choice01Image = levelUpPannel.transform.Find("choice01label").GetComponent<Image>();
        // _choice02Image = levelUpPannel.transform.Find("choice02label").GetComponent<Image>();
        // _choice03Image = levelUpPannel.transform.Find("choice03label").GetComponent<Image>();


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
        levelUpPannel.gameObject.SetActive(true);
    }

    public void ChoiceStatus01()
    {
        Upgrade(1);
        CloseLevelUpPannel();
    }

    public void ChoiceStatus02()
    {
        Upgrade(2);
        CloseLevelUpPannel();
    }

    public void ChoiceStatus03()
    {
        Upgrade(3);
        CloseLevelUpPannel();
    }

    private void CloseLevelUpPannel()
    {
        levelUpPannel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetLevelUpUI()
    {
        _choice01Label.text = GetChoiceString(1);
        _choice02Label.text = GetChoiceString(2);
        _choice03Label.text = GetChoiceString(3);
    }

    private string GetChoiceString(int choiceNum)
    {
        string result = "";
        switch (_statusSets[choiceNum - 1])
        {
            case StatusSet.MoveSpeed:
                result = string.Format("Move Speed Lv.{0:F0}", _damageLv + 1);
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