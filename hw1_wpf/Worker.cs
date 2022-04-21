using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1_wpf
{
    public enum Knowelege
    {
        CPlusPlus,
        English,
        CSharp,
        Sql
    }

    [Serializable]
    public class Worker
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }

        public List<Knowelege> Abilities;
        public string Knoweleges { get; set; }

        public Worker(string name, int age, string email, string adress)
        {
            Name = name;
            Age = age;
            Email = email;
            Adress = adress;

            Abilities = new List<Knowelege>();
            Knoweleges = "";
        }
        public void Convert()
        {
            foreach(Knowelege k in Abilities)
            {
                this.Knoweleges += k.ToString() + " \n";
            }
        }
        public void AddAbilities(bool? Ability, Knowelege knowelege)
        {
            if ((bool)Ability)
            {
                Abilities.Add(knowelege);
            }
        }
        public override string ToString()
        {
            return Name + " " + Email;
        }
    }
}
