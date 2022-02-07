using IHunger.Domain.Interfaces;
using IHunger.Domain.Interfaces.Repository;
using IHunger.Infra.Data.Context;
using IHunger.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Infra.Data
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DataIdentityDbContext _dbContext;

        public RepositoryFactory(DataIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IAddressRestaurantRepository _addressRestaurantRepository;
        public IAddressRestaurantRepository AddressRestaurantRepository
        {
            get => _addressRestaurantRepository ?? (_addressRestaurantRepository = new AddressRestaurantRepository(_dbContext));
        }

        private IAddressUserRepository _addressUserRepository;
        public IAddressUserRepository AddressUserRepository
        {
            get => _addressUserRepository ?? (_addressUserRepository = new AddressUserRepository(_dbContext));
        }

        private ICategoryProductRepository _categoryProductRepository;
        public ICategoryProductRepository CategoryProductRepository
        {
            get => _categoryProductRepository ?? (_categoryProductRepository = new CategoryProductRepository(_dbContext));
        }

        private ICategoryRestaurantRepository _categoryRestaurantRepository;
        public ICategoryRestaurantRepository CategoryRestaurantRepository
        {
            get => _categoryRestaurantRepository ?? (_categoryRestaurantRepository = new CategoryRestaurantRepository(_dbContext));
        }

        private ICommentRepository _commentRepository;
        public ICommentRepository CommentRepository
        {
            get => _commentRepository ?? (_commentRepository = new CommentRepository(_dbContext));
        }

        private ICouponRepository _couponRepository;
        public ICouponRepository CouponRepository
        {
            get => _couponRepository ?? (_couponRepository = new CouponRepository(_dbContext));
        }

        private IItemRepository _itemRepository;
        public IItemRepository ItemRepository
        {
            get => _itemRepository ?? (_itemRepository = new ItemRepository(_dbContext));
        }

        private IOrderRepository _orderRepository;
        public IOrderRepository OrderRepository
        {
            get => _orderRepository ?? (_orderRepository = new OrderRepository(_dbContext));
        }

        private IOrderStatusRepository _orderStatusRepository;
        public IOrderStatusRepository OrderStatusRepository
        {
            get => _orderStatusRepository ?? (_orderStatusRepository = new OrderStatusRepository(_dbContext));
        }

        private IProductRepository _productRepository;
        public IProductRepository ProductRepository
        {
            get => _productRepository ?? (_productRepository = new ProductRepository(_dbContext));
        }

        private IRestaurantRepository _restaurantRepository;
        public IRestaurantRepository RestaurantRepository
        {
            get => _restaurantRepository ?? (_restaurantRepository = new RestaurantRepository(_dbContext));
        }

        private IProfileUserRepository _profileUserRepository;
        public IProfileUserRepository ProfileUserRepository
        {
            get => _profileUserRepository ?? (_profileUserRepository = new ProfileUserRepository(_dbContext));
        }
    }
}
