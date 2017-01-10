using UnityEngine;
using System.Collections;

public class monsterpool : MonoBehaviour {
    
    GameObject[] monster;

    int pool_num;
    bool check_Use = false;

    void Awake()
    {
        monster = new GameObject[15]; 
    }

	// Use this for initialization
	void Start () {	}

    // Update is called once per frame
    void Update() { }

    public void InitPool()
    {
        this.gameObject.name = "monster" + pool_num + "_pool";
        for (int i = 0; i < monster.Length; i++)
        {
            monster[i] = (GameObject)Instantiate(Resources.Load("monster" + pool_num), transform.position, transform.rotation);
            monster[i].transform.parent = this.gameObject.transform;
        }
        gameObject.SetActive(false);
    }

    public void getNum(int num)                     // 어떤 몬스터를 가지고 있는 풀인지 설정
    {
        pool_num = num;
    }

    public int setNum()
    {
        return pool_num;
    }

    public void MonsterPool_State(bool use)         //몬스터 풀의 상태를 변경함
    {

        check_Use = use;
        Debug.Log(check_Use);
        this.gameObject.SetActive(check_Use);
    }

    public bool isUse()
    {
        return check_Use;
    }
}
