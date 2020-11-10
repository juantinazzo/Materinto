using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;

namespace CardDealer
{
    class MathDecks
    {
        public Matematico[] All;
        List<Matematico> Area = new List<Matematico>();
        List<List<Matematico>> listAreas = new List<List<Matematico>>();
        private Random rnd = new Random();
        List<int>[] remainingCards = new List<int>[7];
        List<int> remainingAreas = new List<int>();
        List<string> remainingLang = new List<string>();
        List<Matematico> mateEnJuego = new List<Matematico>();
        List<int> remainingMateEnJuego = new List<int>();
        public MathDecks()
        {   

        }
        public string ArmarPack(PaperDecks paperDeck)
        {
            int area = DrawArea();
            string resp = "1)\tAntes de poner manos a la obra tenés que estudiar la bibliografía existente.";
            if (remainingCards[area].Count < 5) ShuffleMate();
            Matematico matematico = DrawMate(area);
            if (matematico.Paper)
            {
                Paper paper = paperDeck.GetPaperFrom(matematico.Surname);
                resp += " En particular, te interesa mucho el paper _" + paper.Title + "_ de *" + matematico.Name + " " + matematico.Surname+ "*, publicado en " +paper.Year + (paper.With == "" ? "" : (", junto a " + paper.With))+ ", por lo que lo vas a visitar. Tene en cuenta que el paper se encuentra en "+paper.Languaje+".";
            }
            else
            {
                resp += " En particular te interesa mucho el trabajo de " + matematico.Name + " " + matematico.Surname + ",  por lo que lo vas a visitar.";
            }
            resp += "\n2)\tDespués vas a tener que escuchar qué dice un especialista, para enterarte de los resultados más recientes sobre "+matematico.DicList[area+2]+". Para eso buscá a *";
            matematico = DrawMate(area);
            resp += matematico.Name + " " + matematico.Surname +"* en "+matematico.Meeting+ ".\n3)\tUna vez que hagas esto, será un momento adecuado para visitar a un colega e investigar conjuntamente. Viajá a ";
            matematico = DrawMate(area);
            resp += matematico.Residence + " para visitar a *" + matematico.Name + " " + matematico.Surname+"* y profundizar tu conocimiento.\n4)\tComo siempre surgen dudas, y tu caso no será la excepción, vas a tener que visitar a *";
            matematico = DrawMate(area);
            resp += matematico.Name + " " + matematico.Surname + "* para que te ayude. \n5)\t¡A esta altura, tu teorema está casi listo! Visitá a *";
            matematico = DrawMate(area);
            resp += matematico.Name + " " + matematico.Surname + "* para que te ayude a darle una revisión final.";
            return resp;
        } 
        public void GenerateDeck()
        {
            for (int i = 0; i < All.Length; i++) All[i].LoadDic();
            for (int i = 0; i < 7; i++) LoadArea(i);
            ShuffleMate();
        }
        public string DrawLang()
        {
            if (remainingLang.Count == 0) ShuffleLang();
            int rand = rnd.Next(0, remainingLang.Count);
            string lang = remainingLang[rand];
            remainingLang.RemoveAt(rand);
            return lang;
        }

        public void ShuffleLang()
        {
            remainingLang = new List<string>();
            remainingLang.Add("latín e italiano");
            remainingLang.Add("alemán");
            remainingLang.Add("francés");
            remainingLang.Add("inglés");
        }

        public void ShuffleMate()
        {
            for (int i = 0; i < 7; i++)
            {
                List<int> lista = new List<int>();
                for (int j = 0; j < listAreas[i].Count; j++) lista.Add(j);
                remainingCards[i]=lista;
            }
            
        }
        public void ShuffleArea()
        {
            remainingAreas = new List<int>();
            for (int i = 0; i < 7; i++) remainingAreas.Add(i);
        }
        public void ShuffleMateEnJuego()
        {
            remainingMateEnJuego = new List<int>();
            for (int i = 0; i < mateEnJuego.Count; i++) remainingMateEnJuego.Add(i);
        }

        public int DrawArea()
        {
            if (remainingAreas.Count == 0) ShuffleArea();
            int rand = rnd.Next(0, remainingAreas.Count);
            int area = remainingAreas[rand];
            remainingAreas.RemoveAt(rand);
            return area;
        }

        public Matematico DrawMateEnJuego()
        {
            if (remainingMateEnJuego.Count == 0) ShuffleMateEnJuego();
            int rand = rnd.Next(0, remainingMateEnJuego.Count);
            int id = remainingMateEnJuego[rand];
            remainingMateEnJuego.RemoveAt(rand);
            return All[id];
        }

        public Matematico DrawMate(int area)
        {
            if (remainingCards[area].Count == 0) ShuffleMate();
            int rand=rnd.Next(0, remainingCards[area].Count);
            Matematico mate = listAreas[area][remainingCards[area][rand]];
            remainingCards[area].RemoveAt(rand);
            return mate;
        }

        public void LoadArea(int area)
        {
            Area = new List<Matematico>();
            for (int i = 0; i < All.Length; i++) 
                if(All[i].Index && All[i].Dic[area+3])
                    Area.Add(All[i]);
            listAreas.Add(Area);
        }

        public void LoadMateEnJuego()
        {
            mateEnJuego = new List<Matematico>();
            for (int i = 0; i < All.Length; i++) if (All[i].Index) mateEnJuego.Add(All[i]);
        }
    }
}
