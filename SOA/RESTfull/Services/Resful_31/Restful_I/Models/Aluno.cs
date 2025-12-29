
/*
 * Model
 * lufer
 * */
namespace Restful_1.Models
{
    public class Aluno
    {
        int num;


        public Aluno() { }
        public Aluno(int x)
        {
            num = x;
        }
        public int Num
        {
            get { return num; }
            set { num = value; }
        }
    }
}
