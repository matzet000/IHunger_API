using IHunger.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Interfaces
{
    public interface IRepositoryFactory
    {
        IAddressRestaurantRepository AddressRestaurantRepository { get; }
        IAddressUserRepository AddressUserRepository { get; }
        ICategoryProductRepository CategoryProductRepository { get; }
        ICategoryRestaurantRepository CategoryRestaurantRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICouponRepository CouponRepository { get; }
        IItemRepository ItemRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderStatusRepository OrderStatusRepository { get; }
        IProductRepository ProductRepository { get; }
        IRestaurantRepository RestaurantRepository { get; }
        IProfileUserRepository ProfileUserRepository { get; }

    }
}
