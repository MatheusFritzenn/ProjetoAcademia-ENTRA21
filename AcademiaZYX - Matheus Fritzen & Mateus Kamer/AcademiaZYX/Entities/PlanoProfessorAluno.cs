using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PlanoProfessorAluno
    {
        public int ID { get; set; }
        public Plano Plano { get; set; }
        public int IDPlano { get; set; }
        public Aluno Aluno { get; set; }
        public int IDAluno { get; set; }
        public Professor Professor { get; set; }
        public int IDProfessor { get; set; }
    }
}
