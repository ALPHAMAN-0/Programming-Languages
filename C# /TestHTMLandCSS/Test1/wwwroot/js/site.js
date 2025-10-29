// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function updateQuantity(change) {
    const quantityInput = document.getElementById('quantity');
    let newQuantity = parseInt(quantityInput.value) + change;
    
    if (newQuantity >= 1 && newQuantity <= 10) {
        quantityInput.value = newQuantity;
    }
}

function addToCart(productId) {
    fetch('/Men/AddToCart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-CSRF-TOKEN': document.querySelector('input[name="__RequestVerificationToken"]').value
        },
        body: JSON.stringify({ productId: productId })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            const cartCount = document.getElementById('cart-count');
            if (cartCount) {
                cartCount.textContent = data.cartCount;
            }
            alert(data.message || 'Product added to cart!');
        } else {
            alert(data.message || 'Failed to add product to cart');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        alert('Failed to add product to cart');
    });
}

document.addEventListener('DOMContentLoaded', function() {
    // Initialize all modals
    var modals = document.querySelectorAll('.modal');
    modals.forEach(function(modal) {
        new bootstrap.Modal(modal);
    });

    // Size button selection
    const sizeButtons = document.querySelectorAll('.size-btn');
    sizeButtons.forEach(button => {
        button.addEventListener('click', function() {
            sizeButtons.forEach(btn => btn.classList.remove('active'));
            this.classList.add('active');
        });
    });
});

$(document).ready(function () {
    // Initialize modal
    $('#logoModal').modal({
        keyboard: true,
        backdrop: 'static'
    });

    // Handle logo click
    $('.logo-link').on('click', function (e) {
        e.preventDefault();
        $('#logoModal').modal('show');
    });

    // Handle close button
    $('.modal .close').on('click', function () {
        $('#logoModal').modal('hide');
    });
});
