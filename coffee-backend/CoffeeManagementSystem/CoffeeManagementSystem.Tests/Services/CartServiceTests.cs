using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManagementSystem.Application.DTOs.Cart;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Application.Services;
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;
using CoffeeManagementSystem.Infrastructure.Repositories;
using FakeItEasy;
using FluentAssertions;

namespace CoffeeManagementSystem.Tests.Services
{
        public class CartServiceTests
        {
            private readonly ICartRepo _fakeCartRepo;
            private readonly ICoffeeItemRepo _fakeCoffeeRepo;
            private readonly ICartService _cartService;

            public CartServiceTests()
            {
                _fakeCartRepo = A.Fake<ICartRepo>();
                _fakeCoffeeRepo = A.Fake<ICoffeeItemRepo>();
                _cartService = new CartService(_fakeCartRepo, _fakeCoffeeRepo);
            }

            [Fact]
            public async Task AddCartAsync_ShouldReturnCartDto_WhenSuccessful()
            {
                // Arrange
                var addCartDto = new AddCartDto
                {
                    CustomerName = "John",
                    CartItems = new List<AddCartItemRequest>
                {
                    new() { CoffeeItemId = 1, Quantity = 2 }
                }
                };

                var coffeeItem = new CoffeeItem
                {
                    Id = 1,
                    Name = "Latte",
                    Price = 50,
                    Description = "Very good",
                   
                    Stock = 30,
                    IsAvailable = true,
                    ImageUrl = "https/image.com",
                   
                    CategoryId = 3
                };

                // Setup fake coffee repo to return a valid coffee item
                A.CallTo(() => _fakeCoffeeRepo.GetCoffeeItemByIdAsync(1))
                 .Returns(coffeeItem);

                // Setup fake cart repo to return a cart that matches expected result
                var savedCart = new Cart
                {
                    Id = 101,
                    CustomerName = "John",
                    TotalPrice = 100,
                    CartItems = new List<CartItem>
                {
                    new()
                    {
                        Id = 11,
                        CoffeeItemId = 1,
                        Quantity = 2,
                        Total = 100,
                        CoffeeItem = coffeeItem
                    }
                }
                };

                A.CallTo(() => _fakeCartRepo.AddCartAsync(A<Cart>._))
                 .Returns(savedCart);

                // Act
                var result = await _cartService.AddCartAsync(addCartDto);

                // Assert
                result.Should().NotBeNull();
                result!.Id.Should().Be(101);
                result.CustomerName.Should().Be("John");
                result.TotalPrice.Should().Be(100);
                result.CartItems.Should().HaveCount(1);

            }

            [Fact]
            public async Task AddCartAsync_ShouldThrow_WhenCoffeeItemMissing()
            {
                // Arrange
                var addCartDto = new AddCartDto
                {
                    CustomerName = "Alice",
                    CartItems = new List<AddCartItemRequest>
                {
                    new() { CoffeeItemId = 999, Quantity = 1 }
                }
                };

                // Return null to simulate missing coffee item
                A.CallTo(() => _fakeCoffeeRepo.GetCoffeeItemByIdAsync(999)).Returns((CoffeeItem?)null);

                // Act & Assert
                await Assert.ThrowsAsync<Exception>(() => _cartService.AddCartAsync(addCartDto));
            }

            [Fact]
            public async Task AddCartAsync_ShouldReturnNull_WhenRepoReturnsNull()
            {
            

                var coffeeItem = new CoffeeItem
                {
                    Id = 1,
                    Name = "Mocha",
                    Price = 30,
                    Description = "Very Good"
                };

                A.CallTo(() => _fakeCoffeeRepo.GetCoffeeItemByIdAsync(1)).Returns(coffeeItem);

                // Repo returns null to simulate persistence failure
                A.CallTo(() => _fakeCartRepo.AddCartAsync(A<Cart>._)).Returns((Cart?)null);

             

                // Assert
            
            }
        }
    }




