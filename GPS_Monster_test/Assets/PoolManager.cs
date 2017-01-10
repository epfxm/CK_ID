using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PoolManager : MonoBehaviour
{
    public GameObject obj;
    public monsterpool pool_temp;
    public Transform[] init_pos;

    List<GameObject> monsterPool;
    List<monsterpool> _pool;

    public int monsterPool_size;

    // Use this for initialization
    void Awake()
    {
        init_pos = new Transform[9];                                        //몬스터가 보여질 위치
        monsterPool = new List<GameObject>();
        _pool = new List<monsterpool>();
    }

    void Start()
    {

        InitMonsterPool();
    }


    void InitMonsterPool()
    {
        for (int i = 0; i < init_pos.Length; i++)                           //몬스터가 보여질 위치를 가져옴
        {
            init_pos[i] = GameObject.Find("pos" + i).transform;
        }

        for (int i = 0; i < monsterPool_size; i++)                           //몬스터풀 생성 및 초기화
        {
            obj = new GameObject();

            pool_temp = new monsterpool();

            obj.AddComponent<monsterpool>();
                        
            pool_temp = obj.GetComponent<monsterpool>();

            pool_temp.getNum(i);
            pool_temp.InitPool();

            _pool.Add(pool_temp);
            _pool[i].getNum(i);
            _pool[i].InitPool();
           
            monsterPool.Add(obj);
        }
    }

    public void getMonsters(int[] monster_num)//GPS좌표를 기준으로 근접해 있는 영역의 몬스터의 번호를 받아옴
    {
        for (int j = 0; j < monster_num.Length; )
        {
            for (int i = 0; i < monsterPool.Count; i++)
            {
                if (_pool[i].setNum() == monster_num[j])
                {
                    if (_pool[i].isUse() == false)
                    {
                        monsterPool[i].transform.position = init_pos[j].transform.position;
                        _pool[i].MonsterPool_State(true);
                    }
                    else
                        newMonsterPool(monster_num[j],j);
                    j++;
                    break;
                }
            }
        }
    }

    public void newMonsterPool(int num, int pos_num)//미리 생성해 놓은 몬스터풀이 사용중일 경우 새롭게 생성
    {
        obj = new GameObject();

        pool_temp = new monsterpool();

        obj.AddComponent<monsterpool>();

        pool_temp = obj.GetComponent<monsterpool>();

        pool_temp.getNum(num);
        pool_temp.InitPool();

        _pool.Add(pool_temp);
        monsterPool.Add(obj);
        _pool[_pool.Count-1].MonsterPool_State(true);

        monsterPool[monsterPool.Count-1].transform.position = init_pos[pos_num].transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class asd
{
    public int monster_num;
    public int asdasdasdsa;
}
