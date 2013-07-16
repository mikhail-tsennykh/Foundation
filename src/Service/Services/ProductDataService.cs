using System;
using System.Linq;
using AutoMapper;
using Data.Data.Infrastructure;
using Data.Data.Repositories;
using Data.Model;

namespace Service {
  public interface IProductDataService {
    IQueryable<Product> GetAll();
    Product Get(int id);
    Product Get(string name);
    void Create(Product product, string user_name);
    void Edit(Product product, string user_name);
    bool Delete(int id);
    bool Delete(Product product);
  }
  public class ProductDataService : IProductDataService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    public ProductDataService
      (IUnitOfWork unitOfWork, IProductRepository productRepository) {
      _unitOfWork = unitOfWork;
      _productRepository = productRepository;
    }

    public IQueryable<Product> GetAll() {
      return _productRepository.GetAll();
    }
    public Product Get(int id) {
      return _productRepository.Get(id);
    }
    public Product Get(string name) {
      return _productRepository.Get(name);
    }
    public void Create(Product product, string user_name) {
      product.CreatedDate = DateTime.Now;
      product.CreatedBy = user_name;
      product.ModifiedDate = product.CreatedDate;
      product.ModifiedBy = product.CreatedBy;

      _productRepository.Add(product);
      _unitOfWork.Commit();
    }
    public void Edit(Product product, string user_name) {
      var class_old = _productRepository.Get(product.Id);
      Mapper.Map(product, class_old);

      class_old.ModifiedBy = user_name;
      class_old.ModifiedDate = DateTime.Now;
      _unitOfWork.Commit();
    }
    public bool Delete(int id) {
      var product = _productRepository.Get(id);
      _productRepository.Delete(product);
      _unitOfWork.Commit();
      return true;
    }
    public bool Delete(Product product) {
      return Delete(product.Id);
    }

  }
}