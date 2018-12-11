//
// Mecanimのアニメーションデータが、原点で移動しない場合の Rigidbody付きコントローラ
// サンプル
// 2014/03/13 N.Kobyasahi
//
using UnityEngine;
using System.Collections;

// 必要なコンポーネントの列記
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class MyUnityChanControlScriptWithRgidBody : MonoBehaviour
{

    public float animSpeed = 1.5f;				// アニメーション再生速度設定
	public float lookSmoother = 3.0f;			// a smoothing setting for camera motion
	public bool useCurves = true;				// Mecanimでカーブ調整を使うか設定する
												// このスイッチが入っていないとカーブは使われない
	public float useCurvesHeight = 0.5f;		// カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）

	// 以下キャラクターコントローラ用パラメタ
	// 前進速度
	public float forwardSpeed = 7.0f;
	// 後退速度
	public float backwardSpeed = 2.0f;
	// 旋回速度
	public float rotateSpeed = 2.0f;
	// ジャンプ威力
	public float jumpPower = 3.0f; 
	// キャラクターコントローラ（カプセルコライダ）の参照
	private CapsuleCollider col;
	private Rigidbody rb;
	// キャラクターコントローラ（カプセルコライダ）の移動量
	private Vector3 velocity;
	// CapsuleColliderで設定されているコライダのHeiht、Centerの初期値を収める変数
	private float orgColHight;
	private Vector3 orgVectColCenter;
	
	private Animator anim;							// キャラにアタッチされるアニメーターへの参照
	private AnimatorStateInfo currentBaseState;			// base layerで使われる、アニメーターの現在の状態の参照

	private GameObject cameraObject;	// メインカメラへの参照


    // アニメーター各ステートへの参照
    static int idleState = Animator.StringToHash("Base Layer.Idle");
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");
	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	static int restState = Animator.StringToHash("Base Layer.Rest");

    public GameObject root;
    public bool first_shoot = true;
    public bool first_map = true;
    public bool first_x = true;
    public bool first_z = true;
    public bool first_f = true;
    public bool mapState = false;
    public bool xState = false;
    public bool zState = false;
    public XrayScript xray;
    public GameObject gun;
    public bool canShoot = false;
    public bool shooting = false;
    public BulletScript bullet;
    public AudioSource reload;
    public Camera playerCamera;
    public Camera mapCamera;
    public TextMesh bulletsNum;
    public int bullets_num = 0;
    public TextMesh zombiesNum;
    public int zombies_num = 0;
    public float healthBarMaxLength = 1.5f;
    public int health = 10;
    public float MaxHealth = 10;
    public GameObject healthBar;
    public TextMesh healthBarText;
    public Material green;
    public Material yellow;
    public Material red;
    public MeshRenderer ray;
    //private Vector3[] camPos = { new Vector3(0, 2.02f, -2.37f) , new Vector3(0, 2.02f, 2.37f)};
    //int i = 5;

    // 初期化
    void Start ()
	{
        

        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();
		// CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
		col = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();
		//メインカメラを取得する
		cameraObject = GameObject.FindWithTag("MainCamera");
		// CapsuleColliderコンポーネントのHeight、Centerの初期値を保存する
		orgColHight = col.height;
		orgVectColCenter = col.center;

        //playerCamera.transform.position = camPos[i];
        //Debug.Log(camPos[i]);
        mapCamera.enabled = false;
        playerCamera.enabled = true;
        bullet.player = gameObject;
}
	
	
// 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
	void FixedUpdate ()
    {
        //Debug.Log(camPos[i]);
        bulletsNum.text = bullets_num + "";
        zombiesNum.text = zombies_num + "";
        healthBar.gameObject.transform.localScale = new Vector3(healthBarMaxLength * health / MaxHealth, healthBar.gameObject.transform.localScale.y, healthBar.gameObject.transform.localScale.z);
        healthBarText.text = health + "/" + MaxHealth;
        if (health / MaxHealth <= 0.25f)
        {
            healthBar.GetComponent<MeshRenderer>().material = red;
        }
        else if (health / MaxHealth <= 0.5f)
        {
            healthBar.GetComponent<MeshRenderer>().material = yellow;
        }
        else
        {
            healthBar.GetComponent<MeshRenderer>().material = green;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (first_map)
            {
                mapState = !mapState;
            }
            else
            {
                first_map = true;
            }
        }
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            //Debug.Log(camPos[i] + "b4");
            if (first_f)
            {
                
                //i = i % camPos.Length;
                playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y, playerCamera.transform.position.z+i);//playerCamera.transform.position = camPos[i];
                playerCamera.transform.Rotate(new Vector3(0, 180, 0));
                i=-i;
            }
            else
            {
                first_f = true;
            }
            //Debug.Log(camPos[i]+"after");
            
        }*/
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (first_x)
            {
                xState = !xState;
            }
            else
            {
                first_x = true;
            }
        }
        if (!canShoot)
        {
            zState = false;
            ray.enabled = zState;
        }
        else
        {
            gun.GetComponent<MeshRenderer>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Z)&&canShoot)
        {
            if (first_z)
            {
                zState = !zState;
                ray.enabled = zState;
            }
            else
            {
                first_z = true;
            }
        }
        if (!mapState)
        {
            mapCamera.enabled = false;
            playerCamera.enabled = true;
        }
        else if (mapState)
        {
            mapCamera.enabled = true;
            playerCamera.enabled = false;
        }
        if (!xState)
        {
            //mapCamera.cullingMask = -1;
            playerCamera.cullingMask = -1;
            xray.XrayOff();
        }
        else if (xState)
        {
            //mapCamera.cullingMask = 7936;
            playerCamera.cullingMask = 7936;// 1 >> 8 + 1 >> 9 + 1 >> 10 + 1 >> 11 + 1 >> 12;
            xray.XrayOn();
        }
        //Debug.Log(playerCamera.cullingMask);

        float h = Input.GetAxis("Horizontal");				// 入力デバイスの水平軸をhで定義
		float v = Input.GetAxis("Vertical");				// 入力デバイスの垂直軸をvで定義
		anim.SetFloat("Speed", v);							// Animator側で設定している"Speed"パラメタにvを渡す
		anim.SetFloat("Direction", h); 						// Animator側で設定している"Direction"パラメタにhを渡す
		anim.speed = animSpeed;								// Animatorのモーション再生速度に animSpeedを設定する
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// 参照用のステート変数にBase Layer (0)の現在のステートを設定する
		rb.useGravity = true;//ジャンプ中に重力を切るので、それ以外は重力の影響を受けるようにする


        // 以下、キャラクターの移動処理
        velocity = new Vector3(0, 0, v);		// 上下のキー入力からZ軸方向の移動量を取得
		// キャラクターのローカル空間での方向に変換
		velocity = transform.TransformDirection(velocity);
		//以下のvの閾値は、Mecanim側のトランジションと一緒に調整する
		if (v > 0.1) {
			velocity *= forwardSpeed;		// 移動速度を掛ける
		} else if (v < -0.1) {
			velocity *= backwardSpeed;	// 移動速度を掛ける
		}
		
		if (Input.GetButtonDown("Jump")) {	// スペースキーを入力したら

			//アニメーションのステートがLocomotionの最中のみジャンプできる
			if (currentBaseState.nameHash == locoState){
				//ステート遷移中でなかったらジャンプできる
				if(!anim.IsInTransition(0))
				{
						rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
						anim.SetBool("Jump", true);		// Animatorにジャンプに切り替えるフラグを送る
				}
			}
		}
		

		// 上下のキー入力でキャラクターを移動させる
		transform.localPosition += velocity * Time.fixedDeltaTime;

		// 左右のキー入力でキャラクタをY軸で旋回させる
		transform.Rotate(0, h * rotateSpeed, 0);	
	

		// 以下、Animatorの各ステート中での処理
		// Locomotion中
		// 現在のベースレイヤーがlocoStateの時
		if (currentBaseState.nameHash == locoState){
			//カーブでコライダ調整をしている時は、念のためにリセットする
			if(useCurves){
				resetCollider();
			}
		}
		// JUMP中の処理
		// 現在のベースレイヤーがjumpStateの時
		else if(currentBaseState.nameHash == jumpState)
		{
			//cameraObject.SendMessage("setCameraPositionJumpView");	// ジャンプ中のカメラに変更
			// ステートがトランジション中でない場合
			if(!anim.IsInTransition(0))
			{
				
				// 以下、カーブ調整をする場合の処理
				if(useCurves){
					// 以下JUMP00アニメーションについているカーブJumpHeightとGravityControl
					// JumpHeight:JUMP00でのジャンプの高さ（0〜1）
					// GravityControl:1⇒ジャンプ中（重力無効）、0⇒重力有効
					float jumpHeight = anim.GetFloat("JumpHeight");
					float gravityControl = anim.GetFloat("GravityControl"); 
					if(gravityControl > 0)
						rb.useGravity = false;	//ジャンプ中の重力の影響を切る
										
					// レイキャストをキャラクターのセンターから落とす
					Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
					RaycastHit hitInfo = new RaycastHit();
					// 高さが useCurvesHeight 以上ある時のみ、コライダーの高さと中心をJUMP00アニメーションについているカーブで調整する
					if (Physics.Raycast(ray, out hitInfo))
					{
						if (hitInfo.distance > useCurvesHeight)
						{
							col.height = orgColHight - jumpHeight;			// 調整されたコライダーの高さ
							float adjCenterY = orgVectColCenter.y + jumpHeight;
							col.center = new Vector3(0, adjCenterY, 0);	// 調整されたコライダーのセンター
						}
						else{
							// 閾値よりも低い時には初期値に戻す（念のため）					
							resetCollider();
						}
					}
				}
				// Jump bool値をリセットする（ループしないようにする）				
				anim.SetBool("Jump", false);
			}
		}
		// IDLE中の処理
		// 現在のベースレイヤーがidleStateの時
		else if (currentBaseState.nameHash == idleState)
		{
			//カーブでコライダ調整をしている時は、念のためにリセットする
			if(useCurves){
				resetCollider();
			}
			// スペースキーを入力したらRest状態になる
			if (Input.GetButtonDown("Jump")) {
				anim.SetBool("Rest", true);
			}
            if (canShoot)
            {
                //gun.GetComponent<MeshRenderer>().enabled = true;
                if (Input.GetButton("Fire1"))
                {
                    
                    //Debug.Log(first_shoot+ " 1");
                    if (first_shoot&&!shooting)
                    {
                        
                        first_shoot = false;
                        //gun.GetComponent<MeshRenderer>().enabled = true;
                        anim.SetBool("Shoot", true);
                        //Debug.Log(first_shoot + " 2");
                        StartCoroutine(Shoot());
                        //Debug.Log(first_shoot + " 4");


                    }
                    else
                    {
                        //gun.GetComponent<MeshRenderer>().enabled = false;
                        anim.SetBool("Shoot", false);
                    }
                    shooting = true;
                }
                else
                {
                    //gun.GetComponent<MeshRenderer>().enabled = false;
                    anim.SetBool("Shoot", false);
                }
            }
            else
            {
                gun.GetComponent<MeshRenderer>().enabled = false;
            }
        }
		// REST中の処理
		// 現在のベースレイヤーがrestStateの時
		else if (currentBaseState.nameHash == restState)
		{
			//cameraObject.SendMessage("setCameraPositionFrontView");		// カメラを正面に切り替える
			// ステートが遷移中でない場合、Rest bool値をリセットする（ループしないようにする）
			if(!anim.IsInTransition(0))
			{
				anim.SetBool("Rest", false);
			}
		}
	}

	void OnGUI()
	{
        int hight = 180;
        if (canShoot)
        {
            hight = 220;
        }
		GUI.Box(new Rect(Screen.width -260, 10 ,250 ,hight), "Interaction");
		GUI.Label(new Rect(Screen.width -245,30,250,30),"Up/Down Arrow : Go Forwald/Go Back");
		GUI.Label(new Rect(Screen.width -245,50,250,30),"Left/Right Arrow : Turn Left/Turn Right");
		GUI.Label(new Rect(Screen.width -245,70,250,30),"Hit Space key while Running : Jump");
		GUI.Label(new Rect(Screen.width -245,90,250,30),"Hit Spase key while Stopping : Rest");
        GUI.Label(new Rect(Screen.width - 245, 110, 250, 30), "Left Control : Front Camera");
        GUI.Label(new Rect(Screen.width - 245, 130, 250, 30), "M : Map Mode");
        GUI.Label(new Rect(Screen.width - 245, 150, 250, 30), "X : Xray Mode");
        GUI.Label(new Rect(Screen.width - 245, 170, 250, 30), "Point Mouse to search for Zombies");

        if (canShoot)
        {
            GUI.Label(new Rect(Screen.width - 245, 190, 250, 30), "Left Ctrl/Mouse : shoot");
            //GUI.Label(new Rect(Screen.width - 245, 110, 250, 30), "Space : shoot");
            GUI.Label(new Rect(Screen.width - 245, 210, 250, 30), "Z : Gun Laser");
        }
	}


	// キャラクターのコライダーサイズのリセット関数
	void resetCollider()
	{
	// コンポーネントのHeight、Centerの初期値を戻す
		col.height = orgColHight;
		col.center = orgVectColCenter;
	}

    private IEnumerator Shoot()
    {
        //shooting = true;
        //Debug.Log(first_shoot + " 3");
        yield return new WaitForSeconds(0.1f);

        //Debug.Log(first_shoot + " 5");
        Instantiate(bullet);
        bullets_num -= 1;
        if (bullets_num <= 0)
            canShoot = false;
        StartCoroutine(Shoot1());
        
    }

    private IEnumerator Shoot1()
    {
        yield return new WaitForSeconds(0.2f);
        first_shoot = true;
        shooting = false;
    }

    public void GetGun()
    {
        bullets_num += 5;
        canShoot = true;
        reload.Play();
    }

    public GameObject getRoot()
    {
        return root;
    }
}
