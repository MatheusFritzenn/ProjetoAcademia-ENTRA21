using DataAccessLayer;
using Entities;
using Entities.Interfaces;
using Entities.ViewModel;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class PlanoBLL : BaseValidator<Plano>, IPlano
    {
        private PlanoDAL planoDAL = new PlanoDAL();

        public Response Delete(int id)
        {
            return planoDAL.Delete(id);
        }

        public Response Insert(Plano p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            return planoDAL.Insert(p);
        }

        public DataResponse<Plano> Select()
        {
            return planoDAL.Select();
        }

        public Response Update(Plano p)
        {
            Response response = this.Validate(p);
            if (!response.Success)
            {
                return response;
            }

            return planoDAL.Update(p);
        }
        public DataResponse<PlanoQueryViewModel> GetPlanos()
        {
            return planoDAL.GetPlanos();
        }
       

        public SingleResponse<Plano> GetPlanoById(int id)
        {
            return planoDAL.GetPlanoById(id);

        }
    }
    
}
