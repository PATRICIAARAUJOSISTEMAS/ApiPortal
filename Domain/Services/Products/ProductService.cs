using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities.Products;
using Domain.Interfaces;
using Domain.Requests;
using Domain.Resources;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private IMapper _mapper;
        private ResponseBase _responseBase;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _responseBase = new ResponseBase();
        }

        public async Task<ResponseBase> DeleteAsync(string id)
        {
            var user = await _productRepository.GetByIdAsync(id);
            if (user == null)
            {
                _responseBase.AddMessage(string.Format(Message.X0_X1_NAO_ENCONTRADO, Message.O, Message.Produto));
                return _responseBase;
            }
            user.SetDeleted(true);
            _productRepository.Put(user);

            return _responseBase;
        }

        public async Task<IEnumerable<ProductResponse>> GetProductByAsync(ProductRequest productRequest)
        {
            var users = await _productRepository.GetAsync(
                p => p.Name == productRequest.Name ||
                p.Price == productRequest.Price ||
                p.Description == productRequest.Description);

            return _mapper.Map<IEnumerable<ProductResponse>>(users);
        }

        public async Task<ProductResponse> GetProductByIdAsync(string id)
        {
            var user = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductResponse>(user);
        }

        public async Task<ResponseBase> PostAsync(ProductRequest productRequest)
        {
            if (productRequest == null)
            {
                _responseBase.AddMessage(Message.REQUEST_NAO_PODE_SER_VAZIO);
                return _responseBase;
            }
            productRequest.Id = Guid.NewGuid().ToString();
            var product = CreateProduct(productRequest);

            if (product.IsFailure)
            {
                _responseBase.AddMessages(product.Errors);
                return _responseBase;
            }

            await _productRepository.PostAsync(product);
            return _responseBase;
        }

        public ResponseBase Put(ProductRequest productRequest)
        {
            if (productRequest == null)
            {
                _responseBase.AddMessage(Message.REQUEST_NAO_PODE_SER_VAZIO);
                return _responseBase;
            }

            var product = CreateProduct(productRequest);
            if (product.IsFailure)
            {
                _responseBase.AddMessages(product.Errors);
                return _responseBase;
            }

            _productRepository.Put(product);
            return _responseBase;
        }

        private static Product CreateProduct(ProductRequest productRequest)
        {
            var product = new Product(productRequest.Name, productRequest.Price, productRequest.Id);

            if (product.IsFailure)
                return product;

            product.SetDescription(productRequest?.Description);

            return product;
        }
    }
}