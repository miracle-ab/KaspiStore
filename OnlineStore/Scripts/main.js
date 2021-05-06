// BURGER MENU

let burgerMenu = document.querySelector('.b-menu')

burgerMenu.addEventListener('click', function () {
    let burgerContain = document.querySelector('.b-container')

    if (burgerContain.classList.contains('open')) {
        burgerContain.classList.remove('open')

        let mobileHeaderContent = document.querySelector('.mobile__header__content')
        mobileHeaderContent.classList.remove('active__header')
        mobileHeaderContent.classList.add('non__active__header')

    } else {
        burgerContain.classList.add('open')

        let mobileHeaderContent = document.querySelector('.mobile__header__content')

        mobileHeaderContent.classList.remove('non__active__header')
        mobileHeaderContent.classList.add('active__header')


    }
})


// MODAL

let body = document.querySelector('body')

let allButtonRequestHeader = document.querySelectorAll('#contact__request__btn')

for (let i = 0; i < allButtonRequestHeader.length; i++) {
    allButtonRequestHeader[i].addEventListener("click", function () {
        document.querySelector('.bg-modal').style.display = "flex";
        body.style.overflow = 'hidden';
    });
}

document.querySelector('.close').addEventListener("click", function () {
    document.querySelector('.bg-modal').style.display = "none";
    body.style.overflow = 'auto';
});


// ADD LINK TO EACH PRODUCT BLOCK


let allProductBlocks = document.querySelectorAll('.product__content__block')

for (let i = 0; i < allProductBlocks.length; i++) {
    allProductBlocks[i].addEventListener('click', function () {
        let link = allProductBlocks[i].querySelector('.product__items__block__order')

        document.location.href = link;
    })
}
