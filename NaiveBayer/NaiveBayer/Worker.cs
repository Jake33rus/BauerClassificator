using System;
using System.Collections.Generic;
using System.Linq;

namespace NaiveBayer
{
    public class Worker
    {
        string path;
        List<string> kinds = new List<string>();
        List<string> categores = new List<string>();
        List<string> types = new List<string>();
        List<string> descriptions = new List<string>();
        public Worker(string path)
        {
            this.path = path;
        }

        public void GetClassification()
        {
            string line;
            int counter = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                if (counter == 0) { counter++; continue; }
                string[] word = line.Split(';');
                categores.Add(word[0]);
                kinds.Add(word[1]);
                types.Add(word[2]);
                descriptions.Add(word[3]);
            }
            categores.Select(x => x.ToLower()).Distinct().ToList();
            kinds.Select(x => x.ToLower()).Distinct().ToList();
            types.Select(x => x.ToLower()).Distinct().ToList();
            descriptions.Select(x => x.ToLower()).Distinct().ToList();
        }

        public List<Document> CreateList()
        {
            List<Document> docs = new List<Document>();
            for (int i = 0; i < kinds.Count; i++)
            {
                docs.Add(new Document(kinds[i], descriptions[i]));
                docs.Add(new Document(categores[i], descriptions[i]));
                docs.Add(new Document(types[i], descriptions[i]));
            }
            return docs;
        }

        public string ResultRequestName(string text)
        {
            GetClassification();
            var c = new Classifier(CreateList());
            var myListKinds = new List<KeyValuePair<string, double>>();
            var myListCategores = new List<KeyValuePair<string, double>>();
            var myListTypes = new List<KeyValuePair<string, double>>();
            foreach (string keyWord in kinds)
                myListKinds.Add(new KeyValuePair<string, double>(keyWord, c.IsInClassProbability(keyWord, text)));
            foreach (string keyWord in categores)
                myListCategores.Add(new KeyValuePair<string, double>(keyWord, c.IsInClassProbability(keyWord, text)));
            foreach (string keyWord in types)
                myListTypes.Add(new KeyValuePair<string, double>(keyWord, c.IsInClassProbability(keyWord, text)));

            myListKinds.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            myListCategores.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            myListTypes.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            Dictionary<string, double> result = new Dictionary<string, double>();
            string tempStr;
            double tempProbability;
            for (int i = 0; i < myListKinds.Count; i++)
            {
                tempStr = $"{myListKinds[i].Key} -> " +
                    $"{myListCategores[i].Key} " +
                    $"-> {myListTypes[i].Key}";
                tempProbability = myListKinds[i].Value +
                    myListCategores[i].Value +
                    myListTypes[i].Value;
                result.Add(tempStr, tempProbability);
            }

            var myListResult = result.ToList();
            myListResult.Sort((x, y) => x.Value.CompareTo(y.Value));
            return myListResult.Last().Key;
        }

    }
}
