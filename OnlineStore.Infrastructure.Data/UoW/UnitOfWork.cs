﻿using System;
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

        private ProductInventoryRepository productInventRep;

        private ProductPhotoRepository productPhotoRep;

        private ProductProductPhotoRepository productProductPhotoRep;

        private ProductModelRepository productModelRep;

        private ProductDescriptionCultureRepository productDescCultRep;


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

        public ProductInventoryRepository ProductInventory
        {
            get
            {
                if (productInventRep == null)
                    productInventRep = new ProductInventoryRepository(db);
                return productInventRep;
            }
        }

        public ProductPhotoRepository ProductPhoto
        {
            get
            {
                if (productPhotoRep == null)
                    productPhotoRep = new ProductPhotoRepository(db);
                return productPhotoRep;
            }
        }

        public ProductProductPhotoRepository ProductProductPhoto
        {
            get
            {
                if (productProductPhotoRep == null)
                    productProductPhotoRep = new ProductProductPhotoRepository(db);
                return productProductPhotoRep;
            }
        }

        public ProductModelRepository ProductModel
        {
            get
            {
                if (productModelRep == null)
                    productModelRep = new ProductModelRepository(db);
                return productModelRep;
            }
        }

        public ProductDescriptionCultureRepository ProductDescriptionCulture
        {
            get
            {
                if (productDescCultRep == null)
                    productDescCultRep = new ProductDescriptionCultureRepository(db);
                return productDescCultRep;
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
