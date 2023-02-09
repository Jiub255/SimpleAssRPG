using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingDamageNumbers : MonoBehaviour
{

    [SerializeField] private float textRiseSpeed = 1f;
    [HideInInspector] public int damageNumber;
    [SerializeField] private Text displayText;
    private float timeToDestroy = 1f;
    [SerializeField] private float timerLength = 1f;

    private void OnEnable()
    {
        timeToDestroy = timerLength;
    }

    private void Update()
    {
      /*  if (damageNumber == 0)//or use -1 to be extra sure?
        {
            displayNumber.text = "Blocked!";
        }
        else
        {*/
        displayText.text = damageNumber.ToString();
       // }
        
        transform.position = new Vector3(transform.position.x, transform.position.y + (textRiseSpeed * Time.deltaTime), transform.position.z);
        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
