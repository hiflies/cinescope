// Micro-interactions for horizontal scroll
const scrollContainers = document.querySelectorAll('.overflow-x-auto');
scrollContainers.forEach(container => {
    container.addEventListener('wheel', (evt) => {
        evt.preventDefault();
        container.scrollLeft += evt.deltaY;
    }, { passive: false });
});

// Add active states to sidebar
const navLinks = document.querySelectorAll('aside nav a');
navLinks.forEach(link => {
    link.addEventListener('click', (e) => {
        navLinks.forEach(l => {
            l.classList.remove('bg-primary-container/10', 'text-primary-container');
            l.classList.add('text-on-surface-variant');
        });
        link.classList.remove('text-on-surface-variant');
        link.classList.add('bg-primary-container/10', 'text-primary-container');
    });
});