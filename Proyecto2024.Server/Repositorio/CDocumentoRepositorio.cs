using Proyecto2024.BD.Data;
using Proyecto2024.BD.Data.Entity;

namespace Proyecto2024.Server.Repositorio
{
    public class CDocumentoRepositorio : Repositorio<CDocumento>, ICDocumentoRepositorio
    {
        private readonly Context context;

        public CDocumentoRepositorio(Context context): base(context)
        {
            this.context = context;
        }
    }
}
