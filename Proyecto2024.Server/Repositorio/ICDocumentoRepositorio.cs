using Proyecto2024.BD.Data;
using Proyecto2024.BD.Data.Entity;

namespace Proyecto2024.Server.Repositorio
{
    public interface ICDocumentoRepositorio : IRepositorio<CDocumento>, ICDocumentoRepositorio
    {
        private readonly Context context;

        public CDocumentoRegistro(Context context) : base(context)

        {
            this.context = context;
        }
    }
}
