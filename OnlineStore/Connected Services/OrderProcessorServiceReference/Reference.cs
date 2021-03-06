//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineStore.OrderProcessorServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OrderProcessorServiceReference.IOrderProcessorSVC")]
    public interface IOrderProcessorSVC {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderProcessorSVC/ProcessOrder", ReplyAction="http://tempuri.org/IOrderProcessorSVC/ProcessOrderResponse")]
        void ProcessOrder(OnlineStore.Infrastructure.Business.DTO.CartLineDTO[] cart, OnlineStore.Infrastructure.Business.DTO.ShippingDetailsDTO shippingDetails);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderProcessorSVC/ProcessOrder", ReplyAction="http://tempuri.org/IOrderProcessorSVC/ProcessOrderResponse")]
        System.Threading.Tasks.Task ProcessOrderAsync(OnlineStore.Infrastructure.Business.DTO.CartLineDTO[] cart, OnlineStore.Infrastructure.Business.DTO.ShippingDetailsDTO shippingDetails);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderProcessorSVC/ProccesOrderAuthenticated", ReplyAction="http://tempuri.org/IOrderProcessorSVC/ProccesOrderAuthenticatedResponse")]
        void ProccesOrderAuthenticated(OnlineStore.Infrastructure.Business.DTO.CartLineDTO[] cart, OnlineStore.Infrastructure.Business.DTO.ShippingDetailsDTO shippingDetails, string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderProcessorSVC/ProccesOrderAuthenticated", ReplyAction="http://tempuri.org/IOrderProcessorSVC/ProccesOrderAuthenticatedResponse")]
        System.Threading.Tasks.Task ProccesOrderAuthenticatedAsync(OnlineStore.Infrastructure.Business.DTO.CartLineDTO[] cart, OnlineStore.Infrastructure.Business.DTO.ShippingDetailsDTO shippingDetails, string userId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderProcessorSVCChannel : OnlineStore.OrderProcessorServiceReference.IOrderProcessorSVC, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderProcessorSVCClient : System.ServiceModel.ClientBase<OnlineStore.OrderProcessorServiceReference.IOrderProcessorSVC>, OnlineStore.OrderProcessorServiceReference.IOrderProcessorSVC {
        
        public OrderProcessorSVCClient() {
        }
        
        public OrderProcessorSVCClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderProcessorSVCClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderProcessorSVCClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderProcessorSVCClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void ProcessOrder(OnlineStore.Infrastructure.Business.DTO.CartLineDTO[] cart, OnlineStore.Infrastructure.Business.DTO.ShippingDetailsDTO shippingDetails) {
            base.Channel.ProcessOrder(cart, shippingDetails);
        }
        
        public System.Threading.Tasks.Task ProcessOrderAsync(OnlineStore.Infrastructure.Business.DTO.CartLineDTO[] cart, OnlineStore.Infrastructure.Business.DTO.ShippingDetailsDTO shippingDetails) {
            return base.Channel.ProcessOrderAsync(cart, shippingDetails);
        }
        
        public void ProccesOrderAuthenticated(OnlineStore.Infrastructure.Business.DTO.CartLineDTO[] cart, OnlineStore.Infrastructure.Business.DTO.ShippingDetailsDTO shippingDetails, string userId) {
            base.Channel.ProccesOrderAuthenticated(cart, shippingDetails, userId);
        }
        
        public System.Threading.Tasks.Task ProccesOrderAuthenticatedAsync(OnlineStore.Infrastructure.Business.DTO.CartLineDTO[] cart, OnlineStore.Infrastructure.Business.DTO.ShippingDetailsDTO shippingDetails, string userId) {
            return base.Channel.ProccesOrderAuthenticatedAsync(cart, shippingDetails, userId);
        }
    }
}
