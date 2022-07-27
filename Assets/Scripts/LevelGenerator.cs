using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;

    [SerializeField] private GameObject[] trees;
    [SerializeField] private GameObject[] grass;

    private void Start()
    {
        PlaceTrees();
        //PlaceGrass();
    }

    private void PlaceTrees()
    {
        int totalTrees = Random.Range(1000, 2001);

        int lastTree = 0;
        Vector2 lastPos = Vector2.zero;

        for (int i = 0; i < totalTrees; i++)
        {
            Vector2 newPos = new Vector2(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY));

            int randomTree = Random.Range(0, 8);
            
            int repeatTree = Random.Range(0, 10);

            if (repeatTree <= 8 && i != 0)
            {
                int x = Random.Range(-1, 2);
                int y = Random.Range(-1, 2);

                Instantiate(trees[lastTree]).transform.position = lastPos + new Vector2(x * 0.5f, y * 0.5f);

                lastPos += new Vector2(x * 0.25f, y * 0.25f);
            }
            else
            {
                Instantiate(trees[randomTree]).transform.position = newPos;
                lastTree = randomTree;
                lastPos = newPos;
            }
        }
    }

    private void PlaceGrass()
    {
        int totalGrasses = Random.Range(100, 200);
        for(int i = 0; i < totalGrasses; i++)
        {
            Vector2 newPos = new Vector2(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY));
            int randomTree = Random.Range(0, 3);
            GameObject newGrass = Instantiate(grass[randomTree]);
            newGrass.transform.position = newPos;
        }
    }
}
