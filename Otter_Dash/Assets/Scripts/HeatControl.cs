using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HeatControl : MonoBehaviour
{
    private BagScriptableObject bag; // a scriptable object.
    private float keepWarmStrength; // how many sec that goes by before the heat meter drops by drop rate.
    //private float foodHotness = 100; //every food starts with a 100 heat
    private const float HEAT_DROP_RATE = 0.02f;
    public Image parentImageComponent;
    float elapsedTime;
    

    [SerializeField] private Color red;
    [SerializeField] private Color yellow;
    [SerializeField] private Color blue;



    private Image heatCirlceImg;
    // Start is called before the first frame update
    

    void Start()
    {
        bag = Resources.Load<BagScriptableObject>("ScriptableOBJ/WhackAssBag");
        keepWarmStrength = bag.keepWarmStrength;
        parentImageComponent.sprite= bag.bagPicture; // adds bag picture
        parentImageComponent.enabled = true;
        heatCirlceImg = GetComponent<Image>();
        heatCirlceImg.enabled = true;
        
        red = heatCirlceImg.color; //save the initial red color.
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= keepWarmStrength)
        {
            elapsedTime = 0;
            reduceFoodHeat();
        }

        if (heatCirlceImg.fillAmount > 0.67f)
        {
            turnCirlcleColorRed();
        } 
        else if (heatCirlceImg.fillAmount <= 0.67f && heatCirlceImg.fillAmount > 0.34f)
        { 
            turnCirlcleColorYellow();
        }
        else if(heatCirlceImg.fillAmount <= 0.34f)
        {
            turnCirlcleColorBlue();
        }
    }

    void turnCirlcleColorYellow()
    {
        heatCirlceImg.color = Color.Lerp(heatCirlceImg.color, yellow,0.02f);
    }
    void turnCirlcleColorBlue()
    {
        heatCirlceImg.color = Color.Lerp(heatCirlceImg.color, blue,0.02f);
    }
    
    void turnCirlcleColorRed()
    {
        heatCirlceImg.color = Color.Lerp(heatCirlceImg.color, red,0.02f);
    }
    
    void resetCircle()
    {
        heatCirlceImg.color = red;
        heatCirlceImg.fillAmount = 1;

    }

    void reduceFoodHeat()
    {
        if (heatCirlceImg.fillAmount != 0)
        {
            heatCirlceImg.fillAmount -=HEAT_DROP_RATE;
        }
    }

    public void addMoreHeatToFood(float heatAmmount)
    {
        if (heatCirlceImg.fillAmount > 0)
        {
            heatCirlceImg.fillAmount +=heatAmmount;
            
        }
    }
}
