using _01_Framework.Application;
using _02_ElectronShopQuery.Contracts.Comment;
using _02_ElectronShopQuery.Contracts.Product;
using _02_ElectronShopQuery.Contracts.ProductPicture;
using AccountManagement.Infrastructure.EFCore;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ShopCart;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _02_ElectronShopQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;
        private readonly CommentContext _commentContext;
        private readonly DiscountContext _discountContext;
        private readonly InventoryContext _inventoryContext;

        public ProductQuery(ShopContext context, DiscountContext discountContext, InventoryContext inventoryContext, CommentContext commentContext, AccountContext accountContext)
        {
            _context = context;
            _discountContext = discountContext;
            _inventoryContext = inventoryContext;
            _commentContext = commentContext;
            _accountContext = accountContext;
        }

        public async Task<List<ProductQueryModel>> GetProducts()
        {
            var inventory = await _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _discountContext.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var products = await _context.Products
                 .Where(x => !x.IsRemoved)
                 .Select(x => new ProductQueryModel()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Slug = x.Slug,
                     Picture = x.Picture,
                     PictureAlt = x.PictureAlt,
                     PictureTitle = x.PictureTitle
                 }).OrderBy(x => Guid.NewGuid()).Take(20).AsNoTracking().ToListAsync();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    QueryHelper.CalculatePrice(productInventory.Price, product);
                    var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productDiscount != null)
                    {
                        QueryHelper.CalculateDiscount(productDiscount.DiscountRate, productInventory.Price, product);
                    }
                }
            }

            return products;
        }

        public async Task<List<ProductQueryModel>> GetProductsHaveDiscount()
        {
            var inventory = await _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _discountContext.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var products = await _context.Products
                 .Where(x => !x.IsRemoved)
                 .Select(x => new ProductQueryModel()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Slug = x.Slug,
                     Picture = x.Picture,
                     PictureAlt = x.PictureAlt,
                     PictureTitle = x.PictureTitle
                 }).AsNoTracking().ToListAsync();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    QueryHelper.CalculatePrice(productInventory.Price, product);
                    var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productDiscount != null)
                    {
                        QueryHelper.CalculateDiscount(productDiscount.DiscountRate, productInventory.Price, product);
                    }
                }
            }

            return products.Where(x => x.HasDiscount).OrderByDescending(x => x.Id).ToList();
        }

        public async Task<List<ProductQueryModel>> GetAmazings(int position)
        {
            var inventory = await _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _discountContext.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var products = await _context.Products
                .Where(x => !x.IsRemoved && x.Amazings
                .Any(x => x.Position == position && x.StartDate < DateTime.Now && x.EndDate > DateTime.Now))
                .Select(x => new ProductQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    QueryHelper.CalculatePrice(productInventory.Price, product);
                    var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productDiscount != null)
                    {
                        QueryHelper.CalculateDiscount(productDiscount.DiscountRate, productInventory.Price, product);
                    }
                }
            }

            return products;
        }

        public async Task<List<ProductQueryModel>> Search(string search)
        {
            var inventory = await _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _discountContext.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var searchQuery = _context.Products
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                });

            if (!string.IsNullOrWhiteSpace(search))
                searchQuery = searchQuery.Where(x => x.Name.Contains(search) || x.Keywords.Contains(search));

            var products = await searchQuery.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    QueryHelper.CalculatePrice(productInventory.Price, product);
                    var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productDiscount != null)
                    {
                        QueryHelper.CalculateDiscount(productDiscount.DiscountRate, productInventory.Price, product);
                    }
                }
            }

            return products;
        }

        public async Task<ProductQueryModel> GetProductDetails(string slug)
        {
            var inventory = await _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _discountContext.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var product = await _context.Products
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Code = x.Code,
                    Picture = x.Picture,
                    Property = x.Property,
                    Keywords = x.Keywords,
                    PictureAlt = x.PictureAlt,
                    Description = x.Description,
                    Information = x.Information,
                    PictureTitle = x.PictureTitle,
                    Category = x.ProductCategory.Name,
                    SubCategory = x.ProductSubCategory.Name,
                    MetaDescription = x.MetaDescription,
                    Pictures = MapProductPictures(x.ProductPictures)
                }).AsNoTracking().FirstOrDefaultAsync(x => x.Slug == slug);

            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                QueryHelper.CalculatePrice(productInventory.Price, product);
                var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);
                if (productDiscount != null)
                {
                    QueryHelper.CalculateDiscount(productDiscount.DiscountRate, productInventory.Price, product);
                }
            }

            var comments = await _commentContext.Comments
                .Where(x => x.Type == CommentTypes.Products && x.OwnerRecordId == product.Id && x.IsConfirmed)
                .Select(x => new CommentQueryModel()
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();

            foreach (var comment in comments)
            {
                var account = await _accountContext.Accounts.FirstOrDefaultAsync(x => x.Id == comment.AccountId);
                comment.Name = account.FullName;
            }

            product.Comments = comments;
            return product;
        }

        public async Task<List<CartItem>> CheckInventoryStatus(List<CartItem> cartItems)
        {
            var inventory = await _inventoryContext.Inventory.AsNoTracking().ToListAsync();

            foreach (var cartItem in cartItems.Where(x => inventory.Any(y => y.ProductId == x.Id && y.InStock)))
            {
                var itemInventory = inventory.Find(x => x.ProductId == cartItem.Id);
                cartItem.InStock = itemInventory.CalculateCurrentCount() >= cartItem.Count;
            }

            return cartItems;
        }

        private static List<ProductPictureQueryModel> MapProductPictures(IEnumerable<ProductPicture> productPictures)
        {
            return productPictures
               .Where(x => !x.IsRemoved)
               .Select(x => new ProductPictureQueryModel()
               {
                   Picture = x.Picture,
                   PictureAlt = x.PictureAlt,
                   PictureTitle = x.PictureTitle
               }).ToList();
        }
    }
}