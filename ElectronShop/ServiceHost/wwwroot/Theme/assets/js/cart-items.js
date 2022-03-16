const cookieName = "ElectronShop-Cart-Items";
function addToCart(id, name, price, picture) {
    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }

    const count = $("#productCount").val();
    const currentProduct = products.find(x => x.id === id);
    if (currentProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            price,
            picture,
            count
        }

        products.push(product);
    }
    $.cookie(cookieName, JSON.stringify(products),
        {
            expires: 5, path: "/"
        });
    updateCart();
}

function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#cart_items_count").text(products.length);
    $("#mini_cart_items_count").text(products.length);
    const cartItemsWrapper = $("#cart_items_wrapper");
    cartItemsWrapper.html('');
    const miniCartItemsWrapper = $("#mini_cart_items_wrapper");
    miniCartItemsWrapper.html('');
    products.forEach(x => {
        const product =
            `<li class="js-mini-cart-item">
                                <a class="header-basket-list-item">
                                    <div class="header-basket-list-item-image">
                                        <img src="/Pictures/${x.picture}">
                                    </div>
                                    <div class="header-basket-list-item-content">
                                        <h1 class="header-basket-list-item-title">
                                            ${x.name}
                                        </h1>
                                        <div class="header-basket-list-item-footer">
                                            <div class="header-basket-list-item-props">
                                                <span class="header-basket-list-item-props-item">
                                                    ${x.count}
                                                </span>
                                                <span class="header-basket-list-item-remove" onclick="removeFromCart('${x.id}')">
                                                    <i class="mdi mdi-delete"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </a>
            </li>`;

        cartItemsWrapper.append(product);
        miniCartItemsWrapper.append(product);
    });
}

function removeFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    let itemToRemove = products.findIndex(x => x.id === id);
    products.splice(itemToRemove, 1);
    $.cookie(cookieName, JSON.stringify(products), { expires: 5, path: "/" });
    updateCart();
}

function changeCartItemCount(id, totalId, count) {
    var products = $.cookie(cookieName);
    products = JSON.parse(products);
    const productIndex = products.findIndex(x => x.id === id);
    products[productIndex].count = count;
    const product = products[productIndex];
    const newPrice = parseInt(product.price) * parseInt(count);
    $(`#${totalId}`).text(newPrice);
    $.cookie(cookieName, JSON.stringify(products), { expires: 5, path: "/" });
    updateCart();
}