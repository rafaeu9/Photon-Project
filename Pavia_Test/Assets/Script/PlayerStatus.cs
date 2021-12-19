using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviourPunCallbacks, IPunObservable
{

    public int MaxHelth = 100;

    [Range(0,100)]
    public int CurrentHealth = 100;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
            slider = GameObject.Find("Canvas").transform.Find("InGame").Find("Health").GetComponent<Slider>();
 
        slider.maxValue = MaxHelth;
        CurrentHealth = MaxHelth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CurrentHealth;

        

        if(CurrentHealth <= 0)
        {
            transform.position = Vector3.zero;
            CurrentHealth = MaxHelth;
        }
    }

    public void Damage()
    {
        CurrentHealth -= 10;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            // We own this player: send the others our data

            stream.SendNext(CurrentHealth);

        }
        else
        {
            // Network player, receive data
            this.CurrentHealth = (int)stream.ReceiveNext();

        }
    }    

    
}
