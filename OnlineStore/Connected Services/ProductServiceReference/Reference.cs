﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineStore.ProductServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProductServiceReference.IProductSVC")]
    public interface IProductSVC {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductSVC/GetProduct", ReplyAction="http://tempuri.org/IProductSVC/GetProductResponse")]
        OnlineStore.Infrastructure.Business.DTO.ProductDTO GetProduct(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductSVC/GetProduct", ReplyAction="http://tempuri.org/IProductSVC/GetProductResponse")]
        System.Threading.Tasks.Task<OnlineStore.Infrastructure.Business.DTO.ProductDTO> GetProductAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductSVC/GetProducts", ReplyAction="http://tempuri.org/IProductSVC/GetProductsResponse")]
        OnlineStore.Infrastructure.Business.DTO.ProductDTO[] GetProducts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductSVC/GetProducts", ReplyAction="http://tempuri.org/IProductSVC/GetProductsResponse")]
        System.Threading.Tasks.Task<OnlineStore.Infrastructure.Business.DTO.ProductDTO[]> GetProductsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductSVC/Image", ReplyAction="http://tempuri.org/IProductSVC/ImageResponse")]
        OnlineStore.Infrastructure.Business.DTO.PhotoDTO Image(string filename);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductSVC/Image", ReplyAction="http://tempuri.org/IProductSVC/ImageResponse")]
        System.Threading.Tasks.Task<OnlineStore.Infrastructure.Business.DTO.PhotoDTO> ImageAsync(string filename);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductSVC/Dispose", ReplyAction="http://tempuri.org/IProductSVC/DisposeResponse")]
        void Dispose();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductSVC/Dispose", ReplyAction="http://tempuri.org/IProductSVC/DisposeResponse")]
        System.Threading.Tasks.Task DisposeAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProductSVCChannel : OnlineStore.ProductServiceReference.IProductSVC, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProductSVCClient : System.ServiceModel.ClientBase<OnlineStore.ProductServiceReference.IProductSVC>, OnlineStore.ProductServiceReference.IProductSVC {
        
        public ProductSVCClient() {
        }
        
        public ProductSVCClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProductSVCClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductSVCClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductSVCClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public OnlineStore.Infrastructure.Business.DTO.ProductDTO GetProduct(int id) {
            return base.Channel.GetProduct(id);
        }
        
        public System.Threading.Tasks.Task<OnlineStore.Infrastructure.Business.DTO.ProductDTO> GetProductAsync(int id) {
            return base.Channel.GetProductAsync(id);
        }
        
        public OnlineStore.Infrastructure.Business.DTO.ProductDTO[] GetProducts() {
            return base.Channel.GetProducts();
        }
        
        public System.Threading.Tasks.Task<OnlineStore.Infrastructure.Business.DTO.ProductDTO[]> GetProductsAsync() {
            return base.Channel.GetProductsAsync();
        }
        
        public OnlineStore.Infrastructure.Business.DTO.PhotoDTO Image(string filename) {
            return base.Channel.Image(filename);
        }
        
        public System.Threading.Tasks.Task<OnlineStore.Infrastructure.Business.DTO.PhotoDTO> ImageAsync(string filename) {
            return base.Channel.ImageAsync(filename);
        }
        
        public void Dispose() {
            base.Channel.Dispose();
        }
        
        public System.Threading.Tasks.Task DisposeAsync() {
            return base.Channel.DisposeAsync();
        }
    }
}
