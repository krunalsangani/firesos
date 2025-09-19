$(document).ready(function(){
    //masonry init
    

})

const swiper = new Swiper('.visual-swiper', {
    // Optional parameters
    direction: 'horizontal',
    loop: false,
    spaceBetween: 30,
    slidesPerView: 3,
    mousewheel: true,
    breakpoints: {
        375: {
          slidesPerView: 1,
          spaceBetween: 20,
        },
        600: {
          slidesPerView: 1.5,
          spaceBetween: 40,
        },
        1024: {
          slidesPerView: 2.5,
          spaceBetween: 50,
        },
      },
  });