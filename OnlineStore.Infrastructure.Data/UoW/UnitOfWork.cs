using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Infrastructure.Data.Context;

namespace OnlineStore.Infrastructure.Data
{
    public class UnitOfWork : IDisposable
    {
        private ItmContext db = new ItmContext();

        private ProductRepository productRep;

        private ProductDescriptionRepository productDescRep;

        public ProductRepository Products
        {
            get
            {
                if (productRep == null)
                    productRep = new ProductRepository(db);
                return productRep;
            }
        }

        public ProductDescriptionRepository ProductDescription
        {
            get
            {
                if (productDescRep == null)
                    productDescRep = new ProductDescriptionRepository(db);
                return productDescRep;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
