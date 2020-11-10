using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using System.Linq;
using UnityEngine.Windows;

public class XMLManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static XMLManager ins;
    public TileDatabase tileDB;
    public MateDB mateDB;
    public Sprite[] sprites;
    private Vector3[] textPos;
    public int[] Cuadrantes;
    private static Random rng = new Random();
    Text masterFont;
    void Awake()
    {
        ins = this;
    }

    void Start()
    {
        ReadMatematicos();
        textPos = new Vector3[25];
        Cuadrantes = new int[25];
        for (int i = 0; i < 25; i++)
        {
            GameObject go = GameObject.Find("Tile" + (i + 1).ToString());
            Text texto = go.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>();
            textPos[i] = texto.transform.position;
            Cuadrantes[i] = 1;
        }
        LoadTiles("tiles_data.xml"); 
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SaveTiles(string name, bool mix)
    {
        tileDB.list.Clear();
        for (int i = 0; i < 25; i++)
        {
            GameObject go = GameObject.Find("Tile" + (i + 1).ToString());
            TileEntry tl = new TileEntry();
            tl.TileEntryLoad(i + 1, (TileType)Enum.Parse(typeof(TileType), go.tag), (int)go.transform.rotation.eulerAngles.z, Cuadrantes[i], go.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text);
            tileDB.list.Add(tl);
        }

        if (mix)
        {
            for (int i = 25; i > 1; i--)
            {
                i--;
                int k = rng.Next(i + 1);
                int prevNum = tileDB.list[k].TileNumber;
                tileDB.list[k].TileNumber = tileDB.list[i].TileNumber;
                tileDB.list[i].TileNumber = prevNum;

            }
        }
        XmlSerializer serializer = new XmlSerializer(typeof(TileDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/ConfigFiles/" + name, FileMode.Create);
        serializer.Serialize(stream, tileDB);
        stream.Close();
    }

    public void Shuffle()
    {
        SaveTiles("tiles_shuffle.xml", true);
        LoadTiles("tiles_shuffle.xml");
    }

    public void LoadTiles(string name)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(TileDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/ConfigFiles/" + name, FileMode.Open);
        tileDB = serializer.Deserialize(stream) as TileDatabase;
        stream.Close();
        for (int i = 0; i < 25; i++)
        {
            GameObject go = GameObject.Find("Tile" + tileDB.list[i].TileNumber.ToString());
            TileEntry tl = tileDB.list[tileDB.list.FindIndex(item => (item.TileNumber - 1 == i))];
            go.transform.eulerAngles = (new Vector3(0, 0, tl.initialRotation));
            go.tag = tl.tileType.ToString();
            go.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[(int)tl.tileType];
            Text texto = go.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>();
            if (i == 0) masterFont = texto;
            //texto.text = tl.texto;
            texto.text = mateDB.list[i].Surname;
            //  Vector3 positionVec = textPos[i];
            Vector3 positionVec = textPos[tileDB.list[i].TileNumber-1];
            Cuadrantes[tileDB.list[i].TileNumber-1] = tl.cuadrante;
            switch (tl.cuadrante)
            {
                case 1:
                    positionVec.y += 0.3f;
                    //positionVec.x += 0.25f;
                    break;
                case 2:
                    //positionVec.y += 0.25f;
                    positionVec.x -= 0.3f;
                    break;
                case 3:
                    positionVec.y -= 0.3f;
                    //positionVec.x -= 0.25f;
                    break;
                case 4:
                    //positionVec.y -= 0.25f;
                    positionVec.x += 0.3f;
                    break;

            }
            texto.transform.position = positionVec;
            texto.color = Color.black;
            texto.fontSize = 8;
            texto.font = masterFont.font;
            //texto.fontStyle = FontStyle.Bold;
            texto.transform.eulerAngles = new Vector3(0f, 0f, 45f);
        }
        tileDB.list.Clear();
    } 
    public void LoadTilesSaved()
    {
        LoadTiles("tiles_data.xml");
    }
    public void SaveTilesBoard()
    {
        SaveTiles("tiles_data.xml", false);
    }

    void ReadMatematicos()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(MateDB));
        FileStream stream = new FileStream(Application.dataPath + "/ConfigFiles/Matematicos.xml", FileMode.Open);
        mateDB = serializer.Deserialize(stream) as MateDB;
        stream.Close();
    }


}


[System.Serializable]
public class TileEntry
{
    public int TileNumber;
    public TileType tileType;
    public int initialRotation;
    public string texto=" ";
    public int cuadrante;

    public void TileEntryLoad(int tn,TileType tt, int ir,int cuad,string text)
    {
        TileNumber = tn;
        tileType = tt;
        initialRotation = ir;
        cuadrante = cuad;
        texto = text;
    }
  

}

[System.Serializable]
public class TileDatabase
{
    public List<TileEntry> list = new List<TileEntry>();
}


public enum TileType
{
    Straight,
    T,
    Bridge,
}

[System.Serializable]
public class Matematico
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Origin { get; set; }
    public string Residence { get; set; }
    public string Meeting { get; set; }
    public string MeetingPlace { get; set; }
    public int Birth { get; set; }
    public int Death { get; set; }
    public int MeetingYear { get; set; }
    public bool Paper { get; set; }
    public bool Index { get; set; }
    public bool Algebra { get; set; }
    public bool Aritmetica { get; set; }
    public bool Analisis { get; set; }
    public bool Geometria { get; set; }
    public bool Logica { get; set; }
    public bool Conjuntos { get; set; }
    public bool Fundamentos { get; set; }
    public bool Latin { get; set; }
    public bool Aleman { get; set; }
    public bool Frances { get; set; }
    public bool Ingles { get; set; }

    //IDictionary<int, bool> Dic = new Dictionary<int, bool>();

    /*void LoadDic()
    {
        Dic.Add(1, Paper);
        Dic.Add(2, Index);
        Dic.Add(3, Algebra);
        Dic.Add(4, Aritmetica);
        Dic.Add(5, Analisis);
        Dic.Add(6, Geometria);
        Dic.Add(7, Logica);
        Dic.Add(8, Conjuntos);
        Dic.Add(9, Fundamentos);
        Dic.Add(10, Latin);
        Dic.Add(11, Aleman);
        Dic.Add(12, Frances);
        Dic.Add(13, Ingles);
    }*/

    public List<string> DicList = new List<string>
        {
            "Paper",
            "Index",
            "Algebra",
            "Aritmetica",
            "Analisis",
            "Geometria",
            "Logica",
            "Conjuntos",
            "Fundamentos",
            "Latin",
            "Aleman",
            "Frances",
            "Ingles"
        };
}

[System.Serializable]
public class MateDB
{
    public List<Matematico> list = new List<Matematico>();
}
