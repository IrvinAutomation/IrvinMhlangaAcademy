using System;
using LAWTrust_API_Automation.RequestAndResponseHelpers;
using LAWTrust_API_Automation.DataTransferModels;
using LAWTrust_API_Automation.HandleContent;
using RestSharp;

namespace LAWTrust_API_Automation.MethodOperators
{
    public class GetProductsOperator
    {
        public Helpers _helpers;
        public ProductHelper _productHelper;

        public GetProductsOperator()
        {
            _helpers = new Helpers();
            _productHelper = new ProductHelper();
        }

        public ProductsResponse GetProductsPerClient()
        {
            var client = _helpers.SetUrl("EnrolmentAppService/api/Product/GetProducts");
            var request = _productHelper.GetProduct();
            request.RequestFormat = DataFormat.Json;
            var response = _helpers.GetResponse(client, request);
            var products = HandleContents.GetContent<ProductsResponse>(response);
            return products;
        }
    }
}
