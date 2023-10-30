using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MazeGenerator : MonoBehaviour
{
    const bool Fal = true;

    const bool Ut = false;

    public GameObject _Fal_Prefab;

    public int Szelesseg;

    public int Magassag;

    private bool[,] Grid;

    private GameObject[,] Strukura;

    private Vector2 bejarat;
    private Vector2 kijarat;

    
    void Start()
    {  
        GenerateNewRandomMaze();
    }

    private void GenerateBejarat()
    {
        int bejaratOldal = Random.Range(0,4);
        int kijaratOldal = (bejaratOldal + Random.Range(1,4)) % 4;

        switch(bejaratOldal)
        {
            case 0:     //bal
                bejarat = new Vector2(0,Random.Range(1 , Magassag - 1 ));
                break;
            case 1:     //jobb
                bejarat = new Vector2(Szelesseg - 1 , Random.Range(1 , Magassag - 1));
                break;
            case 2:     //felső
                bejarat = new Vector2(Random.Range(1 , Szelesseg - 1) , Magassag - 1);
                break;
            case 3:     //also
                bejarat = new Vector2(Random.Range(1 , Szelesseg - 1) , 0);
                break;        
        }

        switch(kijaratOldal)
        {
            case 0:     //bal
                kijarat = new Vector2(0 , Random.Range(1 , Magassag - 1));
                break;
            case 1:     //jobb
                kijarat = new Vector2(Szelesseg - 1 , Random.Range(1 ,Magassag - 1));
                break;
            case 2:     //felső
                kijarat = new Vector2(Random.Range(1 , Szelesseg - 1 ) , Magassag - 1);
                break;
            case 3:     //alsó
                kijarat = new Vector2(Random.Range(1 , Szelesseg - 1) , 0);
                break;
        }

        Grid[(int)bejarat.x , (int) bejarat.y] = Ut;
        Grid[(int)kijarat.x , (int) kijarat.y] = Ut;
    }

    private void GenerateNewRandomMaze()
    {
        Grid = new bool[Szelesseg, Magassag];
        Strukura = new GameObject[Szelesseg, Magassag];

        GenerateBejarat();

       for (int x = 0; x < Szelesseg; x++)
        {
            for (int y = 0; y < Magassag; y++)
            {
                Grid[x, y] = Fal;
                Strukura[x, y] = Instantiate(_Fal_Prefab, new Vector3(x * 5f, 0f, y * 5f), Quaternion.identity, GetComponent<Transform>());
            }
        }

        int kezdoCellaX = Random.Range(3, Szelesseg - 3);
        int kezdoCellaY = Random.Range(3, Magassag - 3);
        Grid[kezdoCellaX, kezdoCellaY] = Ut;

        HashSet<(int, int)> fCellak = GetNeighborCells(kezdoCellaX, kezdoCellaY, true);

        while (fCellak.Any())
        {
            int randomIndex = Random.Range(0, fCellak.Count);
            (int, int) randomFcella = fCellak.ElementAt(randomIndex);
            int randomFcellaX = randomFcella.Item1;
            int randomFcellaY = randomFcella.Item2;
            Grid[randomFcellaX, randomFcellaY] = Ut;

            HashSet<(int, int)> nevezoCella = GetNeighborCells(randomFcellaX, randomFcellaY, false);
            if (nevezoCella.Any()) 
            {
                int randomNevezoIndex = Random.Range(0, nevezoCella.Count);
                (int, int) randomCellaKapcsolat = nevezoCella.ElementAt(randomNevezoIndex);
                int randomCellaKapcsolatX = randomCellaKapcsolat.Item1;
                int randomCellaKapcsolatY = randomCellaKapcsolat.Item2;

                (int, int) koztesCella;
                if (randomFcellaX < randomCellaKapcsolatX)
                    koztesCella = (randomFcellaX + 1, randomFcellaY);
                else if (randomFcellaX > randomCellaKapcsolatX)
                    koztesCella = (randomFcellaX - 1, randomFcellaY);
                else
                {
                    if (randomFcellaY < randomCellaKapcsolatY)
                        koztesCella = (randomFcellaX, randomFcellaY + 1);
                    else
                        koztesCella = (randomFcellaX, randomFcellaY - 1);
                }

                Grid[koztesCella.Item1, koztesCella.Item2] = Ut;
            }

            fCellak.Remove(randomFcella);

            fCellak.UnionWith(GetNeighborCells(randomFcellaX, randomFcellaY, true));
        }

        for (int x = 0; x < Szelesseg; x++)
        {
            for (int y = 0; y < Magassag; y++)
            {
                if (Grid[x, y] == Ut)
                    Strukura[x, y].SetActive(false);
            }
        }

        SaveSystem.instance.OnLoad();
    }
  
    private HashSet<(int, int)> GetNeighborCells(int x, int y, bool CheckF)
    {
        HashSet<(int, int)> szomszedCellak = new HashSet<(int, int)>();

        if (x > 2)
        {
            (int, int) Aktcella = (x - 2, y);

            if (CheckF ? Grid[Aktcella.Item1, Aktcella.Item2] == Fal : Grid[Aktcella.Item1, Aktcella.Item2] == Ut)
            {
                szomszedCellak.Add(Aktcella);
            }
        }
        if (x < Szelesseg - 3)
        {
            (int, int) Aktcella = (x + 2, y);
            if (CheckF ? Grid[Aktcella.Item1, Aktcella.Item2] == Fal : Grid[Aktcella.Item1, Aktcella.Item2] == Ut)
            {
                szomszedCellak.Add(Aktcella);
            }
        }

        if (y > 2)
        {
            (int, int) Aktcella = (x, y - 2);
            if (CheckF ? Grid[Aktcella.Item1, Aktcella.Item2] == Fal : Grid[Aktcella.Item1, Aktcella.Item2] == Ut)
            {
                szomszedCellak.Add(Aktcella);
            }
        }
        if (y < Magassag - 3)
        {
            (int, int) Aktcella = (x, y + 2);
            if (CheckF ? Grid[Aktcella.Item1, Aktcella.Item2] == Fal : Grid[Aktcella.Item1, Aktcella.Item2] == Ut)
            {
                szomszedCellak.Add(Aktcella);
            }
        }

        return szomszedCellak;

    }
}