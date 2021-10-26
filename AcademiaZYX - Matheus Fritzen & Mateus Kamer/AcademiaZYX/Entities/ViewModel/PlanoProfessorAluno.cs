using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class PlanoProfessorAlunoQueryViewModel
    {
        public int ID { get; set; }
        public string Plano { get; set; }
        public string Professor { get; set; }
        public string Aluno { get; set; }
    }
}
