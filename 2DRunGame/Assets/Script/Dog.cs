using UnityEngine;
using UnityEngine.UI;

public class Dog : MonoBehaviour
{
    #region 欄位區域
    [Header("跳躍次數")]      //標題
    [Range(1, 2)]             //範圍
    public int jumpCount = 2;     //欄位 field (屬性)
    [Header("跳躍高度"), Range(0, 2000)]
    public int jumpHigh = 300;
    [Header("速度"), Range(0, 30.0f)]
    public float speed = 10.5f;
    [Header("滑行距離"), Range(1.0f, 10.0f)]
    public float slide = 5.0f;
    [Tooltip("判斷角色是否在地面")]       //提示
    public bool isGround;
    [Header("名稱")]
    public string dogName = "Jaga";
    //變型欄位
    private Transform cam;
    // 動畫控制器元件
    private Animator ani;
    // 2D膠囊碰撞器元件
    private CapsuleCollider2D cc2d;
    // 剛體元件
    private Rigidbody2D r2d;
    
    public AudioClip SoundJump, SoundSlide;
    private AudioSource audioSource;       //音源

    private SpriteRenderer sr;

    [Header("生命值")]
    public float hp = 500;
    private float maxHp;
    [Header("障礙物傷害值")]
    public float damage = 20;
    // 生命條
    public Image hpBar;
    #endregion

    // 起始事件: 開始時執行一次
    private void Start()
    {
        maxHp = hp;

        // 取得攝影機物件
        cam = GameObject.Find("Main Camera").transform;
        // 取得元件, GetComponent<T>() 泛型
        ani = GetComponent<Animator>();
        cc2d = GetComponent<CapsuleCollider2D>();
        r2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
    }
    // 更新事件: 每一禎執行一次 unity預設為60fps
    private void Update()
    {
        MoveMan();
        MoveCamera();
        
    }
    /// <summary>
    /// 碰撞地板監聽
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "地板")
        {
            isGround = true;
            Debug.Log(collision.gameObject.name);
        }
    }

    /// <summary>
    /// 角色受傷
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "障礙物")
        {
            PlayerDamage();
            sr.enabled = false;
            Invoke("ShowSprite", .2f);  // 延遲調用
        }
    }
    /// <summary>
    /// 顯示Sprite
    /// </summary>
    private void ShowSprite()
    {
        sr.enabled = true;
    }

    private void PlayerDamage()
    {
        Debug.Log("受傷~~");
        hp -= damage;
        hpBar.fillAmount = hp / maxHp;
    }

    /// <summary>
    /// 角色移動方法
    /// </summary>
    private void MoveMan() {
        // Time.deltaTime為裝置每一禎的時間,如60fps則為1/60秒
        transform.Translate(speed*Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// 鏡頭移動方法
    /// </summary>
    private void MoveCamera() {
        cam.Translate(speed*Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// 角色跳躍方法, 音效
    /// </summary>
    public void DoJump() {
        if (isGround == true)
        {
            ani.SetBool("跳躍開關", true);
            r2d.AddForce(new Vector2(0, jumpHigh));
            isGround = false;
            audioSource.PlayOneShot(SoundJump, 0.6f);
        }
    }

    /// <summary>
    /// 角色滑行方法,設定滑行時collider大小, 音效
    /// </summary>
    public void DoSlide() {
        transform.Translate(slide*Time.deltaTime, 0 ,0);
        ani.SetBool("滑行開關", true);

        cc2d.offset = new Vector2(-0.52f, -0.85f);
        cc2d.size = new Vector2(2.2f, 2.2f);

        audioSource.PlayOneShot(SoundSlide);
    }
    /// <summary>
    /// 重置角色跳躍與滑行,重置collider大小
    /// </summary>
    public void ResetAnimator()
    {
        ani.SetBool("跳躍開關", false);
        ani.SetBool("滑行開關", false);

        cc2d.offset = new Vector2(-0.52f, 0.15f);
        cc2d.size = new Vector2(2.2f, 4.2f);
    }
}
