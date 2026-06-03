// Hover effect for poster upload zone
const mediaZones = document.querySelectorAll('.aspect-video, .aspect-\\[2\\/3\\]');
mediaZones.forEach(zone => {
    zone.addEventListener('mouseenter', () => {
        zone.classList.add('scale-[1.01]');
        zone.style.transition = 'all 0.3s ease';
    });
    zone.addEventListener('mouseleave', () => {
        zone.classList.remove('scale-[1.01]');
    });
});