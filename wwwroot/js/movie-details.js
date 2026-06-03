// Simple micro-interaction for rating stars
const stars = document.querySelectorAll('.flex.gap-xs.text-primary-container .material-symbols-outlined');
stars.forEach((star, index) => {
    star.addEventListener('mouseover', () => {
        for(let i=0; i<=index; i++) {
            stars[i].style.fontVariationSettings = "'FILL' 1";
        }
    });
    star.addEventListener('mouseout', () => {
        // Return to original state logic could go here if needed
    });
});