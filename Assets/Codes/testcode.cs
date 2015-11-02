using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class testcode : MonoBehaviour {

    public LinkedList<int> testList;

    public int TESTINT = 1;
	// Use this for initialization
	void Start () {
        testList = new LinkedList<int>();

        testList.AddLast(1);
        testList.AddLast(3);
        testList.AddLast(2);
        testList.AddLast(4);

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("a"))
        {
            Remove(0, 0, testList.First);
        }
        Debug.Log(testList.First.Value);
        if (Input.GetKeyDown("b"))
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown("d"))
        {
            Debug.Log("Gtween!");
            //Color temp = GameObject.Find("Image (1)").GetComponent<Image>().material.color;
            //temp = new Color
            Color Dark = new Color(.5f, .5f,.5f,.5f);
            GameObject.Find("Image (1)").GetComponent<Image>().CrossFadeColor(Dark, 0f, false,true);
            //GameObject.Find("Image (1)").GetComponent<Image>().color = testC;
            //GameObject temp = Instantiate(GameObject.Find("Image (1)"), Vector3.zero, Quaternion.identity) as GameObject;
            //temp.transform.SetParent(GameObject.Find("Image (1)").transform.parent);
        }
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("Gtween!");
            //Color temp = GameObject.Find("Image (1)").GetComponent<Image>().material.color;
            //temp = new Color
            Color Dark = new Color(2f, 2f, 2f, 2f);
            GameObject.Find("Image (1)").GetComponent<Image>().CrossFadeColor(Dark, 0f, false, true);
            //GameObject.Find("Image (1)").GetComponent<Image>().color = testC;
            //GameObject temp = Instantiate(GameObject.Find("Image (1)"), Vector3.zero, Quaternion.identity) as GameObject;
            //temp.transform.SetParent(GameObject.Find("Image (1)").transform.parent);
        }
	}

    private void Remove(int testNum, int num, LinkedListNode<int> node)
    {
        
        if (testNum != num)
        {
            Remove(testNum+1, num, node.Next);
        }
        else
        {
            testList.Remove(node);
        }
    }

    void OnDestroy()
    {
        //Debug.Log("DESTROYED");
        //GameObject test = Instantiate(gameObject, transform.position, transform.localRotation) as GameObject;
        //test.transform.parent = null;
        //test.SetActive(true);
        //test.GetComponent<testcode>().enabled = true;
    }

    public int GetNum()
    {
        return 3;
    }

}
