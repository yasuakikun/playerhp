using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class PlayerHP : MonoBehaviour
{
    private MoveEnemy moveEnemy;
    //private YureCamera yurecamera;
    private MyChara myChara;
    private Canvas canvas;
    private BloodRainCameraController blood;

    // ダメージエフェクト用テクスチャー(面倒なのでインスペクターから設定).
    [SerializeField] protected Texture tex;

    private Vector3 attacker;// 攻撃者の座標.
    private float timer; // エフェクト表示タイム


    //マックスHP
    public float maxHP = 1000;
    //受けるダメージ
    public int ZombieDamage = 20;
    public int WolfDamage = 30;
    public int HobgoDamage = 40;
    public int GoblinDamage = 30;
    public int trollDamage = 60;
    public int BossDamage = 50;

    //現在のプレイヤーのHP
    public int HP = 1000;

    LineRenderer HPGage;

    void Start()
    {
        moveEnemy = GetComponent<MoveEnemy>();
       //yurecamera = GetComponent<YureCamera>();
        myChara = GetComponent<MyChara>();
        canvas = GetComponent<Canvas>();
        blood= GetComponent<BloodRainCameraController>();

        HPGage = GameObject.Find("HPGage").GetComponent<LineRenderer>();
        //HPのマックスHP
       //HP = maxHP;
    }


    void Update()
    {
        if (HP >= 0)
        {
            HPGage.SetPosition(1, new Vector3(3 * (HP / maxHP), 0.0f, 0.0f));
        }
        else if (HP < 0)
        {
            Debug.Log("ゲームオーバー");
            SceneManager.LoadScene("Over");
        }
    }

    
    public void OnDamage(int damage, Vector3 sender)
    {
        //だめーじからHPを減らす
        HP -= damage;
        //攻撃者の座標を受け取る.
        attacker = sender;
        // タイマーをオンにする.
        timer = 4f;
    }

    void OnGUI()
    {
        //スクリーンの中心取得.
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);

        // GUI領域をsyutoku
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

        // タイマーが０より大きかったら処理
        if (timer > 0)
        {
            // 色,赤
            GUI.color = new Color(1, 0, 0, timer / 4f);

            //エフェクト表示
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

            // タイマー減らす
            timer -= Time.deltaTime;
        }

        // GUI領域を終了.
        GUILayout.EndArea();
    }

    public void OnTriggerEnter(Collider hit)
    {
        //エネミータグと会ったた時
        switch (hit.gameObject.tag)
        {
            case "EnemyHit":
                OnDamage(20,attacker);
                //blood.Attack(20);
                //Camera.main.gameObject.GetComponent<ShakeCamera>().Shake();             
                break;
            case "WolfHit":
                OnDamage(30, attacker);
                //HP -= WolfDamage;
                //blood.Attack(30);
                //Camera.main.gameObject.GetComponent<ShakeCamera>().Shake();
                break;
            case "HobgoHit":
                //HP -= HobgoDamage;
                //blood.Attack(40);
                OnDamage(40, attacker);
                //Camera.main.gameObject.GetComponent<ShakeCamera>().Shake();
                break;
            case "GoblinHit":
                //HP -= HobgoDamage;
                OnDamage(30, attacker);
                //blood.OnDamage(30);
                //Camera.main.gameObject.GetComponent<ShakeCamera>().Shake();
                break;
            case "TrollHit":
                //HP -= trollDamage;
                //blood.Attack(60);
                OnDamage(60, attacker);
               // Camera.main.gameObject.GetComponent<ShakeCamera>().Shake();
                break;
            case "BossHit":
                //HP -= BossDamage;
                //blood.Attack(50);
                OnDamage(50, attacker);
                //Camera.main.gameObject.GetComponent<ShakeCamera>().Shake();
                break;
        }

    }

}