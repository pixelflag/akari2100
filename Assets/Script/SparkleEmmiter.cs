using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleEmmiter : MonoBehaviour
{
    [SerializeField]
    private GameObject heart;

    private int count = 0;
	
	void Update ()
    {
        count++;

        if(count == 1)
        {
            emmitSparkle();
        }
        else if (count == 5)
        {
            emmitSparkle();
        }
        else if (count == 8)
        {
            emmitSparkle();
        }
        else if (count == 12)
        {
            emmitSparkle();
            Destroy(gameObject);
        }
    }

    void emmitSparkle()
    {
        Vector3 force = new Vector3();
        force.x = Random.Range(-2.0f, 2.0f);
        force.y = Random.Range( 0.0f, 3.0f);

        GameObject h = Instantiate(heart, gameObject.transform.parent);

        h.GetComponent<Sparkle>().addForce(force);
        h.GetComponent<Sparkle>().randomOffset(10.0f);
    }
}
