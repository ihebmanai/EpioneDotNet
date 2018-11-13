using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epione.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private EpioneContext dataContext;
        public EpioneContext DataContext { get { return dataContext; } }

        public DatabaseFactory()
        {
            dataContext = new EpioneContext();
        }

        protected override void DisposeCore()
        {
            // libérer espace mémoire du context
            if(DataContext!=null)
            DataContext.Dispose();
        }
    }

}
