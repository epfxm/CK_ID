using UnityEngine;
using System.Collections;

public class pos : MonoBehaviour {

    public GameObject[] monster;

    Vector3 _pos;

    float max_x;
    float max_z;
    float min_x;
    float min_z;

    int num = 999;
    private void Start()
    {
        monster = new GameObject[20];
        max_x = transform.position.x + 25f;
        min_x = transform.position.x - 25f;

        max_z = transform.position.z + 25f;
        min_z = transform.position.z - 25f;
        _pos = new Vector3(Random.Range(min_x, max_x), 50f, Random.Range(min_z, max_z));
    }


    public void RegenMonster(int _num)
    {
        if (num != _num)
        {
            if (num != 999)
            {
                for(int i = 0; i < monster.Length; i++)
                {
                    monster[i].SetActive(false);
                }
            }
            num = _num;
            monster[0] = (GameObject)Instantiate(Resources.Load("monster" + num), _pos, Quaternion.identity);
            monster[0].transform.parent = this.gameObject.transform;
            for (int i = 1; i < monster.Length; i++)
            {

                _pos = new Vector3(Random.Range(min_x, max_x), 50f, Random.Range(min_z, max_z));
                monster[i] = (GameObject)Instantiate(monster[0], _pos, Quaternion.identity);
                monster[i].transform.parent = this.gameObject.transform;
            }
        }
    }
}
