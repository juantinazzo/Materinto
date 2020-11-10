using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDealer
{
    class Matematico
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

        public IDictionary<int, bool> Dic = new Dictionary<int, bool>();

        public void LoadDic()
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
        }

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
}
