using UnityEngine;

public class Dog : MonoBehaviour
{
    #region 欄位區域
    [Header("跳躍次數")]      //標題
    [Range(1, 2)]             //範圍
    public int jumpCount = 2;     //欄位 field (屬性)
    [Header("跳躍高度"), Range(1, 20)]
    public int jumpHigh = 5;
    [Header("速度"), Range(1.0f, 30.0f)]
    public float speed = 10.5f;
    [Header("滑行距離"), Range(1.0f, 10.0f)]
    public float slide = 5.0f;
    [Tooltip("判斷角色是否在地面")]       //提示
    public bool isGround;
    [Header("名稱")]
    public string dogName = "Jaga";
    //變型欄位
    public Transform man, cam;
    #endregion

    // 起始事件: 開始時執行一次
    private void Start()
    {
        
    }
    // 更新事件: 每一禎執行一次 unity預設為60fps
    private void Update()
    {
        MoveMan();
        MoveCamera();
        
    }

    /// <summary>
    /// 角色移動方法
    /// </summary>
    private void MoveMan() {
        // Time.deltaTime為裝置每一禎的時間,如60fps則為1/60秒
        man.Translate(speed*Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// 鏡頭移動方法
    /// </summary>
    private void MoveCamera() {
        cam.Translate(speed*Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// 角色跳躍方法
    /// </summary>
    public void DoJump() {
        man.Translate(0, jumpHigh*Time.deltaTime, 0);
    }

    /// <summary>
    /// 角色滑行方法
    /// </summary>
    public void DoSlide() {
        man.Translate(slide*Time.deltaTime, 0 ,0);
    }
}
