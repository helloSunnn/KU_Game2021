using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConRegisbody : MonoBehaviour

{
    Rigidbody rb;
    public float speed = 2f;
    float newRotY = 0;
    public float rotSpeed = 20f;
    public GameObject prefabBullet;
    public Transform gunPosition;
    public float jumpPower = 10f;
   

    public bool hasGun = false;
    public float gunPower = 15f;
    public float gunCooldown = 1f;
    public float gunCooldownCount = 0;

    public int bulletCount = 0;
    public int coinCount = 0;
    public PlaygroundScenesManager manager;
    public AudioSource audioCoin;
    public AudioSource audioFire;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<PlaygroundScenesManager>();
        if(manager == null)
        {
            print("Manager not found");
        }

        //อ่านจน.เหรียญที่เซฟไว้
        if (PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }
        manager.SetTextCoin(coinCount);
    }

    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;
        if(horizontal > 0)
        {
            newRotY = 90;
        }
        else if(horizontal < 0 )
        {
            newRotY = -90;
        }
        if (vertical > 0)
        {
            newRotY = 0;
        }
        else if(vertical < 0)
        {
            newRotY = 180;
        }

        rb.AddForce(horizontal, 0, vertical, ForceMode.VelocityChange);
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, newRotY, 0),
                            transform.rotation, Time.deltaTime * rotSpeed );
    }

    private void Update()
    {
        gunCooldownCount += Time.deltaTime;
        //ยิงปืน
        if (Input.GetKeyDown(KeyCode.LeftControl) && (bulletCount>0) && (gunCooldownCount >= gunCooldown))
        {
            gunCooldownCount = 0;
            GameObject bullet = Instantiate(prefabBullet, gunPosition.position, gunPosition.rotation);
            Rigidbody bRb = bullet.GetComponent<Rigidbody>();
            bRb.AddForce(transform.forward * gunPower, ForceMode.Impulse);
            Destroy(bullet, 2f);

            bulletCount--;
            manager.SetTextBullet(bulletCount);
            audioFire.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // เพิ่มเก็บคะเเนนเหรียญ
        if (other.gameObject.tag == "Collectable")
        {
            Destroy(other.gameObject);
            coinCount ++;
            manager.SetTextCoin(coinCount);
            audioCoin.Play();
            //เซฟจน.เหรียญ
            PlayerPrefs.SetInt("CoinCount", coinCount);
        }

        if (other.gameObject.name == "GunItem")
        {
            hasGun = true;
            Destroy(other.gameObject);
            bulletCount += 10;
            manager.SetTextBullet(bulletCount);
           
        }
    }

}

