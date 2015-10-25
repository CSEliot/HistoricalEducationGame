using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class testcode : MonoBehaviour {

    public LinkedList<int> testList;
    public Action SpecialAbility;

    public int TESTINT = 1;
	// Use this for initialization
	void Start () {
        testList = new LinkedList<int>();

        testList.AddLast(1);
        testList.AddLast(3);
        testList.AddLast(2);
        testList.AddLast(4);
        SpecialAbility = gameObject.GetComponent<AllSpecialFunctions>().SpecialAbilityList[6];
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
            SpecialAbility();
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
        Go.Do("DESTROYED");
        GameObject test = Instantiate(gameObject, transform.position, transform.localRotation) as GameObject;
        test.transform.parent = null;
        test.SetActive(true);
        test.GetComponent<testcode>().enabled = true;
    }

}
