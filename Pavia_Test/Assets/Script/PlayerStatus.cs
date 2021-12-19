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
        slider.maxValue = MaxHelth;
        CurrentHealth = MaxHelth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CurrentHealth;
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
