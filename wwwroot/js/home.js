// Hero Auto-Rotating Logic
let currentSlide = 0;
const slides = document.querySelectorAll('.hero-slide');
const totalSlides = slides.length;

function nextSlide() {
    slides[currentSlide].classList.remove('active');
    currentSlide = (currentSlide + 1) % totalSlides;
    slides[currentSlide].classList.add('active');
}

setInterval(nextSlide, 6000);

// Heart Pop Animation
function heartPop(el) {
    el.classList.add('heart-pop');
    el.style.fontVariationSettings = "'FILL' 1";
    el.classList.add('text-error');
    setTimeout(() => {
        el.classList.remove('heart-pop');
    }, 300);
}