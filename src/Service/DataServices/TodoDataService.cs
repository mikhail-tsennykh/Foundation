using System;
using System.Linq;
using AutoMapper;
using Data.Infrastructure;
using Model;
using Repositories;

namespace Service {
  public interface ITodoDataService {
    IQueryable<Todo> GetAll();
    Todo Get(int id);
    void Create(Todo todo, string user_name);
    void Edit(Todo todo, string user_name);
    bool Delete(int id);
    bool Delete(Todo todo);
  }
  public class TodoDataService : ITodoDataService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITodoRepository _todoRepository;
    public TodoDataService
      (IUnitOfWork unitOfWork, ITodoRepository todoRepository) {
      _unitOfWork = unitOfWork;
      _todoRepository = todoRepository;
    }

    public IQueryable<Todo> GetAll() {
      return _todoRepository.GetAll();
    }
    public Todo Get(int id) {
      return _todoRepository.Get(id);
    }
    public void Create(Todo todo, string user_name) {
      todo.CreatedDate = DateTime.Now;
      todo.CreatedBy = user_name;
      todo.ModifiedDate = todo.CreatedDate;
      todo.ModifiedBy = todo.CreatedBy;

      _todoRepository.Add(todo);
      _unitOfWork.Commit();
    }
    public void Edit(Todo todo, string user_name) {
      var todo_old = _todoRepository.Get(todo.Id);
      Mapper.Map(todo, todo_old);

      todo_old.ModifiedBy = user_name;
      todo_old.ModifiedDate = DateTime.Now;
      _unitOfWork.Commit();
    }
    public bool Delete(int id) {
      var todo = _todoRepository.Get(id);
      _todoRepository.Delete(todo);
      _unitOfWork.Commit();
      return true;
    }
    public bool Delete(Todo todo) {
      return Delete(todo.Id);
    }

  }
}